using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class PerfumeProduct
    {
        public int PerfumeId { get; set; }
        public int ProductId { get; set; }
        public Perfume Perfume { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
