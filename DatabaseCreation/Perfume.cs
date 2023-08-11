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
        public string Code { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Smell { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public int Sillage { get; set; } = 0;
        public string? Weather { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public PerfumeProduct Product { get; set; } = null!;
        public ICollection<Note> Notes { get; set; } = new List<Note>();
        public ICollection<Equalivent> Equalivents { get; set; } = new List<Equalivent>();

    }
}
