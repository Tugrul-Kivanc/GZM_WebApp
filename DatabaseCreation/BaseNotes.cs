using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCreation
{
    public class BaseNotes
    {
        public int PerfumeId { get; set; }
        public Perfume Perfume { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
