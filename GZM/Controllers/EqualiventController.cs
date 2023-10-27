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
    public class EqualiventController : ControllerBase
    {
        public IActionResult Index()
        {
            //TODO implement better loading
            //TODO filtering
            var equalivents = _context.Equalivents.Include(a => a.EqualiventBrand).Include(b => b.Perfume).ToList();

            return View(equalivents);
        }

        // GET: Equalivent/Create
        public IActionResult Create()
        {
            CreatePerfumeViewData();
            CreateEqualiventBrandViewData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EqualiventBrandId,Code,PerfumeId")] Equalivent equalivent)
        {
            ModelState.Remove("EqualiventBrand");
            ModelState.Remove("Perfume");
            if (ModelState.IsValid)
            {
                _context.Add(equalivent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            CreatePerfumeViewData(equalivent.PerfumeId);
            CreateEqualiventBrandViewData(equalivent.EqualiventId);
            return View(equalivent);
        }

        private void CreatePerfumeViewData(int? perfumeId = null)
        {
            if (perfumeId == null)
            {
                ViewData["PerfumeId"] = new SelectList(_context.Perfumes, "PerfumeId", "Code");
            }
            else
            {
                ViewData["PerfumeId"] = new SelectList(_context.Perfumes, "PerfumeId", "Code", perfumeId);
            }
        }

        private void CreateEqualiventBrandViewData(int? equaliventeId = null)
        {
            if (equaliventeId == null)
            {
                ViewData["EqualiventBrandId"] = new SelectList(_context.EqualiventBrands, "EqualiventBrandId", "Name");
            }
            else
            {
                ViewData["EqualiventBrandId"] = new SelectList(_context.EqualiventBrands, "EqualiventBrandId", "Name", equaliventeId);
            }
        }
    }
}
