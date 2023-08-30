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
    public class PerfumeController : ControllerBase
    {
        // GET: Perfume
        public IActionResult Index(string? gender, string? sortBy)
        {
            if(_context.Perfumes == null)
            {
                return NotFound();
            }

            ViewData["IdSort"] = String.IsNullOrEmpty(sortBy) ? "id_desc" : "";
            ViewData["StockSort"] = sortBy == "stock" ? "stock_desc" : "stock";
            ViewData["SalesSort"] = sortBy == "sales" ? "sales_desc" : "sales";

            var perfumes = _context.Perfumes.Include(a => a.Product).ToList();
            if (gender != null)
            {
                perfumes = perfumes.Where(a => a.Gender == gender || a.Gender == "Unisex").ToList();
            }

            CreateGenderViewData();

            var model = perfumes.Select(a => new PerfumeViewModel
            {
                Brand = a.Brand,
                Code = a.Code,
                Gender = a.Gender,
                Info = a.Info,
                Link = a.Link,
                PerfumeId = a.PerfumeId,
                ProductId = a.ProductId,
                Smell = a.Smell,
                Type = a.Type,
                Weather = a.Weather,
                Stock = a.Product.Stock,
                TotalSales = a.Product.TotalSales
            });

            switch (sortBy)
            {
                case "stock":
                    model = model.OrderByDescending(a => a.Stock).ToList();
                    break;
                case "sales":
                    model = model.OrderByDescending(a => a.TotalSales).ToList();
                    break;
                case "id_desc":
                    model = model.OrderByDescending(a => a.ProductId).ToList();
                    break;
                case "stock_desc":
                    model = model.OrderBy(a => a.Stock).ToList();
                    break;
                case "sales_desc":
                    model = model.OrderBy(a => a.TotalSales).ToList();
                    break;
                default:
                    model = model.OrderBy(b => b.ProductId).ToList();
                    break;
            }

            return View(model);
        }

        // GET: Perfume/Create
        public IActionResult Create()
        {
            CreateGenderViewData();
            CreateWeatherViewData();
            return View();
        }

        // POST: Perfume/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerfumeId,ProductId,Code,Brand,Type,Smell,Gender,Sillage,Info,Weather,Link")] Perfume perfume)
        {
            if (ModelState.IsValid)
            {
                _context.Add(perfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreateGenderViewData();
            CreateWeatherViewData();
            return View(perfume);
        }

        // GET: Perfume/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }
            CreateGenderViewData();
            CreateWeatherViewData();
            return View(perfume);
        }

        // POST: Perfume/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerfumeId,ProductId,Code,Brand,Type,Smell,Gender,Sillage,Info,Weather,Link")] Perfume perfume)
        {
            if (id != perfume.PerfumeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(perfume);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerfumeExists(perfume.PerfumeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                CreateGenderViewData();
                CreateWeatherViewData();
                return RedirectToAction(nameof(Index));
            }
            return View(perfume);
        }

        // GET: Perfume/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes
                .FirstOrDefaultAsync(m => m.PerfumeId == id);
            if (perfume == null)
            {
                return NotFound();
            }

            return View(perfume);
        }

        // POST: Perfume/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfumes == null)
            {
                return Problem("Entity set 'GzmdatabaseContext.Perfumes'  is null.");
            }
            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume != null)
            {
                _context.Perfumes.Remove(perfume);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerfumeExists(int id)
        {
          return (_context.Perfumes?.Any(e => e.PerfumeId == id)).GetValueOrDefault();
        }

        private void CreateWeatherViewData()
        {
            var weatherTypes = new List<string>() { "Genel", "Soğuk", "Sıcak" };

            List<SelectListItem> weatherSelectList = new List<SelectListItem>();
            foreach (var item in weatherTypes)
            {
                weatherSelectList.Add(new SelectListItem() { Text = item, Value = item });
            }

            ViewData["Weathers"] = weatherSelectList;
        }

        private void CreateGenderViewData()
        {
            var genderTypes = new List<string>() { "Unisex", "Erkek", "Kadın" };

            List<SelectListItem> genderSelectList = new List<SelectListItem>();
            foreach (var item in genderTypes)
            {
                genderSelectList.Add(new SelectListItem() { Text = item, Value = item });
            }

            ViewData["Genders"] = genderSelectList;
        }
    }
}
