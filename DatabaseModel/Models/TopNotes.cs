using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class TopNotes
{
    public int PerfumeId { get; set; }
    public Perfume Perfume { get; set; } = null!;

    public int NoteId { get; set; }
    public Note Note { get; set; } = null!;
}
