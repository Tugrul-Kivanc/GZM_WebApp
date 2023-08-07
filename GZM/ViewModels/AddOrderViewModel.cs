using DatabaseModel.Models;

namespace GZM.ViewModels
{
    public class AddOrderViewModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; } = new List<Product>();

        public int Quantity { get; set; }

        public int Fee { get; set; }

        public string Payment { get; set; } = null!;

        public string? Description { get; set; }
    }
}
