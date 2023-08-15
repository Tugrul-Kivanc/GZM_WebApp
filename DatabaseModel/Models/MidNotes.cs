using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class MidNotes
{
    public int PerfumeId { get; set; }
    public Perfume Perfume { get; set; } = null!;

    public int NoteId { get; set; }
    public Note Note { get; set; } = null!;
}
