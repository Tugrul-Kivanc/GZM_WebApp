using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Perfume
    {
        public int PerfumeId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Code { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Smell { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public ICollection<TopNotes> TopNotes { get; set; }
        public ICollection<MidNotes> MidNotes { get; set; }
        public ICollection<BaseNotes> BaseNotes { get; set; }
        public int Sillage { get; set; }
        public string? Info { get; set; }
        public string? Weather { get; set; }
        public string? Link { get; set; }
        public ICollection<Equalivent> Equalivents { get; set; }
    }
}
