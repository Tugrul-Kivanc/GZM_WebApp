using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GZM.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult ListPerfumes()
        {
            return View();
        }
    }
}