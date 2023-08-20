namespace GZM.ViewModels
{
    public class CreateOrderViewModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<int> ProductIds { get; set; }
        public int ProductCount { get; set; }
        public int Fee { get; set; }
        public string Payment { get; set; } = null!;
        public string? Description { get; set; }
    }
}
