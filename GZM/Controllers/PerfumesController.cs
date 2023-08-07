using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DatabaseModel.Models;

namespace GZM.Controllers
{
    public class PerfumesController : Controller
    {
        private readonly GZMWebAppDbContext _context;

        public PerfumesController(GZMWebAppDbContext context)
        {
            _context = context;
        }

        // GET: Perfumes
        public async Task<IActionResult> Index()
        {
              return _context.Perfumes != null ? 
                          View(await _context.Perfumes.ToListAsync()) :
                          Problem("Entity set 'GzmwebAppDbContext.Perfumes'  is null.");
        }

        // GET: Perfumes/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Perfumes/Create
        public IActionResult Create()
        {
            GenerateViewBags();

            return View();
        }

        // POST: Perfumes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerfumeId,Code,Brand,Type,Smell,Gender,Weather,Description,Link")] Perfume perfume)
        {
            GenerateViewBags();

            if (ModelState.IsValid && !IsDuplicateCode(perfume.Code))
            {
                _context.Add(perfume);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(perfume);
        }

        // GET: Perfumes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            GenerateViewBags();

            if (id == null || _context.Perfumes == null)
            {
                return NotFound();
            }

            var perfume = await _context.Perfumes.FindAsync(id);
            if (perfume == null)
            {
                return NotFound();
            }
            return View(perfume);
        }

        // POST: Perfumes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerfumeId,Code,Brand,Type,Smell,Gender,Weather,Description,Link")] Perfume perfume)
        {
            GenerateViewBags();

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
                return RedirectToAction(nameof(Index));
            }
            return View(perfume);
        }

        // GET: Perfumes/Delete/5
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

        // POST: Perfumes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Perfumes == null)
            {
                return Problem("Entity set 'GzmwebAppDbContext.Perfumes'  is null.");
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

        private void GenerateViewBags()
        {
            var genders = new List<string>() { "Unisex", "Erkek", "Kadın" };
            var weatherTypes = new List<string>() { "Genel", "Sıcak", "Soğuk" };

            List<SelectListItem> genderSelectList = new List<SelectListItem>();
            foreach (var item in genders)
            {
                genderSelectList.Add(new SelectListItem() { Text = item, Value = item });
            }

            List<SelectListItem> weatherSelectList = new List<SelectListItem>();
            foreach (var item in weatherTypes)
            {
                weatherSelectList.Add(new SelectListItem() { Text = item, Value = item });
            }

            ViewBag.Genders = genderSelectList;
            ViewBag.Weathers = weatherSelectList;
        }

        private bool IsDuplicateCode(string code)
        {
            var sameCodeCount = _context.Perfumes.Where(a => a.Code == code).Count();

            return sameCodeCount > 0 ? true : false;
        }
    }
}
