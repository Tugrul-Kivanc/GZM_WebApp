using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class EqualiventBrand
    {
        public int EqualiventBrandId { get; set; }
        public string Name { get; set; } = null!;

        public ICollection<Equalivent> Equalivents { get; set; }
    }
}
