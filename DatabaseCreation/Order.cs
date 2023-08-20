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
        public int ProductCount { get; set; }
        public int Fee { get; set; }
        public string Payment { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
