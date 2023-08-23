using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModel.Models;
using GZM.ViewModels;

namespace GZM.Controllers
{
    public class OrderController : ControllerBase
    {
        // GET: Order
        public async Task<IActionResult> Index()
        {
            if(_context.Orders == null)
            {
                return Problem("Entity set 'GzmdatabaseContext.Orders'  is null.");
            }

            var orders = _context.Orders.Select(a => new ListOrderViewModel()
            {
                OrderId = a.OrderId,
                Description = a.Description,
                Fee = a.Fee,
                OrderDate = a.OrderDate,
                Payment = a.Payment,
                ProductNames = _context.ProductOrders.Where(b => b.OrderId == a.OrderId).Select(c => c.Product.Name).ToList(),
                ProductCount = a.ProductOrders.Select(b => b.Quantity).Sum()
            }).OrderByDescending(b => b.OrderDate).OrderByDescending(c => c.OrderId).ToListAsync();

            return View(await orders);
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
                    OrderDate = new DateTime(2023, 06, 17),
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
                _context.Products.Find(productOrder.ProductId).TotalSales += quantity;
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

            var model = new ListOrderViewModel()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Fee= order.Fee,
                Payment = order.Payment,
                Description = order.Description,
                //Products = order.Products.ToList(),
                //Quantity = order.Quantity
            };

            CreatePaymentViewData();
            CreateProductsViewData();
            return View(model);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Quantity,Fee,Payment,OrderDate,Description")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }

            CreatePaymentViewData();
            CreateProductsViewData();
            return View(order);
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

        private void CreateProductsViewData()
        {
            var products = _context.Products.ToList();

            List<SelectListItem> productSelectList = new List<SelectListItem>();
            foreach (var product in products)
            {
                productSelectList.Add(new SelectListItem() { Text = GetFullProductName(product), Value = product.ProductId.ToString() });
            }

            ViewData["Products"] = productSelectList;
        }

        private void CreateProductNamesViewBag(int orderId)
        {
            var productNames = _context.ProductOrders.Where(a => a.OrderId == orderId).Select(b => b.Product.Name).ToList();

            ViewData["ProductNames"] = productNames;
        }
    }
}
