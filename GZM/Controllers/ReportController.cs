﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GZM.Controllers
{
    public class ReportController : ControllerBase
    {
        public IActionResult Index(DateTime? date)
        {
            int monthlyCash = 0;
            int monthlyCard = 0;
            int monthlyTransfer = 0;
            int monthlySum = 0;
            int monthlyTrendyol = 0;
            int monthlyHepsiburada = 0;

            if (date == null)
            {
                date = DateTime.Today;
            }

            monthlyCash = _context.Orders.Where(a => a.OrderDate.Month == date.Value.Month && a.OrderDate.Year == date.Value.Year && a.Payment == "Nakit").Select(b => b.Fee).Sum();
            monthlyCard = _context.Orders.Where(a => a.OrderDate.Month == date.Value.Month && a.OrderDate.Year == date.Value.Year && a.Payment == "Kart").Select(b => b.Fee).Sum();
            monthlyTransfer = _context.Orders.Where(a => a.OrderDate.Month == date.Value.Month && a.OrderDate.Year == date.Value.Year && a.Payment == "Havale").Select(b => b.Fee).Sum();
            monthlyTrendyol = _context.Orders.Where(a => a.OrderDate.Month == date.Value.Month && a.OrderDate.Year == date.Value.Year && a.Payment == "Trendyol").Select(b => b.Fee).Sum();
            monthlyHepsiburada = _context.Orders.Where(a => a.OrderDate.Month == date.Value.Month && a.OrderDate.Year == date.Value.Year && a.Payment == "Hepsibrd").Select(b => b.Fee).Sum();
            monthlySum = monthlyCash + monthlyCard + monthlyTransfer + monthlyTrendyol + monthlyHepsiburada;

            ViewData["SelectedDate"] = date;
            ViewData["MonthlyCash"] = monthlyCash;
            ViewData["MonthlyCard"] = monthlyCard;
            ViewData["MonthlyTransfer"] = monthlyTransfer;
            ViewData["MonthlyTrendyol"] = monthlyTrendyol;
            ViewData["MonthlyHepsiburada"] = monthlyHepsiburada;
            ViewData["MonthlySum"] = monthlySum;

            return View();
        }
    }
}
