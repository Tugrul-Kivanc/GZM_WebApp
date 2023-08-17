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
        protected string GetFullProductName(Product product)
        {
            string productName = product.Name;
            if (product.CategoryId == 3 || product.CategoryId == 4)
            {
                productName = _context.Categories.Where(a => a.CategoryId == product.CategoryId).First().Name + " " + _context.Perfumes.Where(a => a.ProductId == product.ProductId).First().Type;
            }
            return productName;
        }
    }
}
