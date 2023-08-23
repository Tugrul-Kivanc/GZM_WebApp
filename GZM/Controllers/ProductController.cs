﻿using System;
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
    public class ProductController : ControllerBase
    {
        // GET: Product
        public async Task<IActionResult> Index()
        {
            var gzmdatabaseContext = _context.Products.Include(p => p.Category).Include(p => p.Perfume).OrderBy(a => a.CategoryId).OrderBy(b => b.ProductId);
            return View(await gzmdatabaseContext.ToListAsync());
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
