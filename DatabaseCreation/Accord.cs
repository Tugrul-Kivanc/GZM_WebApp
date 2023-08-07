using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class Accord
    {
        public int AccordId { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
