using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public int Quantity { get; set; }
        public int Fee { get; set; }
        public string Payment { get; set; } = null!;
        public string? Description { get; set; }
    }
}
