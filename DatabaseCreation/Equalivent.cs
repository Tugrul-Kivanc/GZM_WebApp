using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Equalivent
    {
        public int EqualiventId { get; set; }
        public string Code { get; set; } = null!;

        public int PerfumeId { get; set; }
        public Perfume Perfume { get; set; } = null!;

        public int EqualiventBrandId { get; set; }
        public EqualiventBrand EqualiventBrand { get; set; } = null!;
    }
}
