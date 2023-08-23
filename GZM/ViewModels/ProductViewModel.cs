namespace GZM.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public int Stock { get; set; }

        public long TotalSales { get; set; }

        public int CategoryId { get; set; }

        public int? PerfumeId { get; set; }
    }
}
