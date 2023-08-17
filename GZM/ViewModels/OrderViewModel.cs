using DatabaseModel.Models;

namespace GZM.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<string> ProductIds { get; set; }
        public List<Product>? Products { get; set; }
        public int Quantity { get; set; }
        public int Fee { get; set; }
        public string Payment { get; set; } = null!;
        public string? Description { get; set; }
    }
}
