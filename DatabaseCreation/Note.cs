using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Name { get; set; } = null!;
        public int AccordId { get; set; }
        public Accord Accord { get; set; } = null!;
        public ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();
    }
}
