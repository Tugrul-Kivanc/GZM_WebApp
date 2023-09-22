using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModel.Models;
using GZM.ViewModels;
using Microsoft.Data.SqlClient;

namespace GZM.Controllers
{
    public class ProductController : ControllerBase
    {
        // GET: Product
        public IActionResult Index(int? categoryId, string? sortBy)
        {
            CreateCategoryViewData();

            ViewData["IdSort"] = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewData["StockSort"] = sortBy == "stock" ? "stock_desc" : "stock";
            ViewData["SalesSort"] = sortBy == "sales" ? "sales_desc" : "sales";

            var products = _context.Products.Include(p => p.Category).Include(p => p.Perfume).ToList();
            // TODO fix tolist
            // TODO both filtering & sorting noto working together
            switch(sortBy)
            {
                case "stock":
                    products = products.OrderByDescending(a => a.Stock).ToList();
                    break;
                case "sales":
                    products = products.OrderByDescending(a => a.TotalSales).ToList();
                    break;
                case "id_desc":
                    products = products.OrderByDescending(a => a.CategoryId).ThenByDescending(b => b.ProductId).ToList();
                    break;
                case "stock_desc":
                    products = products.OrderBy(a => a.Stock).ToList();
                    break;
                case "sales_desc":
                    products = products.OrderBy(a => a.TotalSales).ToList();
                    break;
                default:
                    products = products.OrderBy(a => a.CategoryId).ThenBy(b => b.ProductId).ToList();
                    break;
            }

            if(categoryId != null)
            {
                products = products.Where(a => a.CategoryId == categoryId).ToList();
            }

            return View(products);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            CreateCategoryViewData();
            CreatePerfumeViewData();
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,Stock,TotalSales,CategoryId,PerfumeId")] Product product)
        {
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateCategoryViewData(product);
            CreatePerfumeViewData(product.PerfumeId);
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            CreateCategoryViewData(product);
            CreatePerfumeViewData(product.PerfumeId);

            var model = new ProductViewModel()
            {
                CategoryId = product.CategoryId,
                TotalSales = product.TotalSales,
                Stock = product.Stock,
                Name = product.Name,
                PerfumeId = product.PerfumeId,
                ProductId = product.ProductId
            };

            return View(model);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Stock,TotalSales,CategoryId,PerfumeId")] ProductViewModel model)
        {
            if (id != model.ProductId)
            {
                return NotFound();
            }

            var product = _context.Products.Find(model.ProductId);
            product.CategoryId = model.CategoryId;
            product.Name = model.Name;
            product.Stock = model.Stock;
            product.TotalSales = model.TotalSales;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            CreateCategoryViewData(product);
            CreatePerfumeViewData(product.PerfumeId);
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Perfume)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'GzmdatabaseContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        private void CreateCategoryViewData(Product? product = null)
        {
            if (product == null)
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            }
            else
            {
                ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name", product.CategoryId);
            }
        }

        private void CreatePerfumeViewData(int? perfumeId = null)
        {
            if (perfumeId == null)
            {
                ViewData["PerfumeId"] = new SelectList(_context.Perfumes, "PerfumeId", "Brand");
            }
            else
            {
                ViewData["PerfumeId"] = new SelectList(_context.Perfumes, "PerfumeId", "Brand", perfumeId);
            }
        }
    }
}
