using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModel.Models;
using GZM.ViewModels;
using X.PagedList;

namespace GZM.Controllers
{
    public class OrderController : ControllerBase
    {
        private int pageSize = 50;

        // GET: Order
        public async Task<IActionResult> Index(DateTime? date, int page = 1)
        {
            if(_context.Orders == null)
            {
                return Problem("Entity set 'GzmdatabaseContext.Orders'  is null.");
            }

            var dateSearch = DateTime.Today.Date;
            if(date != null)
            {
                dateSearch = date.Value.Date;
            }

            ViewData["SelectedDate"] = dateSearch;

            ViewData["Date"] = dateSearch.Date.ToShortDateString();
            ViewData["Nakit"] = _context.Orders.Where(a => a.OrderDate.Date == dateSearch && a.Payment == "Nakit").Sum(b => b.Fee);
            ViewData["Kart"] = _context.Orders.Where(a => a.OrderDate.Date == dateSearch && a.Payment == "Kart").Sum(b => b.Fee);
            ViewData["Havale"] = _context.Orders.Where(a => a.OrderDate.Date == dateSearch && a.Payment == "Havale").Sum(b => b.Fee);
            ViewData["Toplam"] = _context.Orders.Where(a => a.OrderDate.Date == dateSearch).Sum(b => b.Fee);

            var orders = _context.Orders.Include(a => a.ProductOrders).ThenInclude(b => b.Product).Select(c => new ListOrderViewModel()
            {
                OrderId = c.OrderId,
                Description = c.Description,
                Fee = c.Fee,
                OrderDate = c.OrderDate,
                Payment = c.Payment,
                ProductNames = c.ProductOrders.Select(d => d.Product.Name).ToList(),
                ProductCount = c.ProductOrders.Select(d => d.Quantity).Sum()
            });

            if(date != null)
            {
                orders = orders.Where(a => a.OrderDate.Date == dateSearch);
            }
            
            orders = orders.OrderByDescending(b => b.OrderDate).ThenByDescending(c => c.OrderId);

            return View(await orders.ToPagedListAsync(page, pageSize));
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            CreatePaymentViewData();
            CreateProductsViewData();
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDate,ProductIds,Products,ProductCount,Fee,Payment,Description")] CreateOrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                Order newOrder = new Order()
                {
                    Description = order.Description,
                    Fee = order.Fee,
                    //OrderDate = order.OrderDate,
                    OrderDate = DateTime.Now,
                    Payment = order.Payment
                };

                _context.Add(newOrder);
                await _context.SaveChangesAsync();

                foreach (var productId in order.ProductIds)
                {
                    var productOrder = new ProductOrder()
                    {
                        OrderId = newOrder.OrderId,
                        ProductId = productId,
                        Quantity = 1
                    };
                    _context.Add(productOrder);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(EditProductOrder), new { id = newOrder.OrderId });
            }
            CreatePaymentViewData();
            CreateProductsViewData();
            return View(order);
        }

        public async Task<IActionResult> EditProductOrder(int id)
        {
            if (_context.ProductOrders == null)
            {
                return NotFound();
            }

            var productOrders = await _context.ProductOrders.Where(a => a.OrderId == id).ToListAsync();

            var productQuantities = new Dictionary<int, int>();

            foreach (var item in productOrders)
            {
                productQuantities.Add(item.ProductId, item.Quantity);
            }

            var model = new ProductOrderViewModel()
            {
                OrderId= id,
                ProductQuantities = productQuantities
            };

            CreateProductNamesViewBag(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductOrder(ProductOrderViewModel model)
        {
            var productOrders = _context.ProductOrders.Where(a => a.OrderId == model.OrderId).ToList();

            foreach (var (productId, quantity) in model.ProductQuantities)
            {
                var productOrder = _context.ProductOrders.Where(a => a.OrderId == model.OrderId && a.ProductId == productId).Single();
                productOrder.Quantity = quantity;
            }
            await _context.SaveChangesAsync();

            foreach (var productId in model.ProductQuantities.Keys)
            {
                var product = _context.Products.Find(productId);
                UpdateProductSalesAndStock(product);
            }
            await _context.SaveChangesAsync();

            CreateProductNamesViewBag(model.OrderId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var model = new CreateOrderViewModel()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Fee= order.Fee,
                Payment = order.Payment,
                Description = order.Description,
            };

            CreatePaymentViewData();
            CreateProductsViewData(id);
            return View(model);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,ProductIds,Quantity,Fee,Payment,OrderDate,Description")] CreateOrderViewModel model)
        {
            if (id != model.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var order = _context.Orders.Find(id);
                try
                {
                    var initialProductOrders = _context.ProductOrders.Where(a => a.OrderId == id).ToList();
                    foreach (var initialProductOrder in initialProductOrders)
                    {
                        if(!model.ProductIds.Contains(initialProductOrder.ProductId)) // Remove old productorder if its not included in the edited order
                        {
                            _context.ProductOrders.Remove(initialProductOrder);
                            await _context.SaveChangesAsync();
                            UpdateProductSalesAndStock(_context.Products.Find(initialProductOrder.ProductId));
                        }
                        else
                        {
                            model.ProductIds.Remove(initialProductOrder.ProductId); // Remove the productid if its not changed
                        }
                    }

                    foreach (var productId in model.ProductIds) // Add new products
                    {
                        _context.Add(new ProductOrder()
                        {
                            OrderId = id,
                            ProductId = productId,
                            Quantity = 1
                        });
                    }

                    order.Payment = model.Payment;
                    order.OrderDate = model.OrderDate;
                    order.Fee = model.Fee;
                    order.Description = model.Description;

                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EditProductOrder), new { id = id });
            }

            CreatePaymentViewData();
            CreateProductsViewData(id);
            return View(model);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'GzmdatabaseContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        private void CreatePaymentViewData()
        {
            var paymentTypes = new List<string>() { "Nakit", "Kart", "Havale", "Hediye" };

            List<SelectListItem> paymentSelectList = new List<SelectListItem>();
            foreach (var item in paymentTypes)
            {
                paymentSelectList.Add(new SelectListItem() { Text = item, Value = item });
            }

            ViewData["PaymentTypes"] = paymentSelectList;
        }

        private void CreateProductsViewData(int? orderId = null)
        {
            var products = _context.Products.ToList();
            var selectedProducts = new List<Product>();
            if(orderId != null)
            {
                selectedProducts = _context.ProductOrders.Where(a => a.OrderId == orderId).Select(b => b.Product).ToList();
            }

            List<SelectListItem> productSelectList = new List<SelectListItem>();
            foreach (var product in products)
            {
                var isSelected = selectedProducts.Contains(product);

                productSelectList.Add(new SelectListItem() { Text = GetFullProductName(product), Value = product.ProductId.ToString(), Selected = isSelected });
            }

            ViewData["Products"] = productSelectList;
        }

        private void CreateProductNamesViewBag(int orderId)
        {
            var productNames = _context.ProductOrders.Where(a => a.OrderId == orderId).Select(b => b.Product.Name).ToList();

            ViewData["ProductNames"] = productNames;
        }

        private void UpdateProductSalesAndStock(Product product)
        {
            int totalSales = _context.ProductOrders.Where(a => a.ProductId == product.ProductId).Select(b => b.Quantity).Sum();
            product.Stock -= totalSales - (int)product.TotalSales;
            product.TotalSales = totalSales;
            _context.Update(product);
        }
    }
}
