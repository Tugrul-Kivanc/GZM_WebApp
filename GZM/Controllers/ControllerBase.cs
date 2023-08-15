using DatabaseModel.Models;
using Microsoft.AspNetCore.Mvc;

namespace GZM.Controllers
{
    public class ControllerBase : Controller
    {
        protected readonly GzmdatabaseContext _context;

        public ControllerBase()
        {
            _context = new GzmdatabaseContext();
        }
    }
}
