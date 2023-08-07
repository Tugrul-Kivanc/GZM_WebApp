using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public int Stock { get; set; }
        public int Price { get; set; }
        public long TotalSales { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
