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
    public class PerfumeController : Controller
    {
        private readonly GzmdatabaseContext _context;

        public PerfumeController(GzmdatabaseContext context)
        {
            _context = context;
        }

        // GET: Perfume
        public async Task<IActionResult> Index()
        {
              return _context.Perfumes != null ? 
                          View(await _context.Perfumes.ToListAsync()) :
                          Problem("Entity set 'GzmdatabaseContext.Perfumes'  is null.");
        }

        // GET: Perfume/Details/5
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

        // GET: Perfume/Create
        public IActionResult Create()
        {
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
    }
}
