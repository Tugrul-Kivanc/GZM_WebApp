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
        public int Stock { get; set; }
        public long TotalSales { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;

        public int? PerfumeId { get; set; } = null;
        public Perfume? Perfume { get; set; } = null;

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
