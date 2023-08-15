using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Perfume
{
    public int PerfumeId { get; set; }

    public string Code { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Smell { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string? Weather { get; set; }

    public string? Description { get; set; }

    public string? Link { get; set; }

    public int Sillage { get; set; }

    public virtual ICollection<Equalivent> Equalivents { get; set; } = new List<Equalivent>();

    public virtual PerfumeProduct? PerfumeProduct { get; set; }

    public virtual ICollection<Note> NotesNotes { get; set; } = new List<Note>();
}
