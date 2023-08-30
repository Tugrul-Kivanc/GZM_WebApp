namespace GZM.ViewModels
{
    public class PerfumeViewModel
    {
        public int PerfumeId { get; set; }
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Smell { get; set; }
        public string Gender { get; set; }
        public int Sillage { get; set; }
        public string? Info { get; set; }
        public string? Weather { get; set; }
        public string? Link { get; set; }
        public int Stock { get; set; }
        public long TotalSales { get; set; }
    }
}
