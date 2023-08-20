namespace GZM.ViewModels
{
    public class ProductOrderViewModel
    {
        public int OrderId { get; set; }
        public Dictionary<int, int> ProductQuantities { get; set; }
    }
}
