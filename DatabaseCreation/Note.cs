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

        public ICollection<TopNotes> PerfumeTopNotes { get; set; }
        public ICollection<MidNotes> PerfumeMidNotes { get; set; }
        public ICollection<BaseNotes> PerfumeBaseNotes { get; set; }
    }
}
