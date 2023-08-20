namespace GZM.ViewModels
{
    public class ListOrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public List<int> ProductIds { get; set; }
        public List<string> ProductNames { get; set; }
        public int ProductCount { get; set; }
        public int Fee { get; set; }
        public string Payment { get; set; } = null!;
        public string? Description { get; set; }
    }
}
