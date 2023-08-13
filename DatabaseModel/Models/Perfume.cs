using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Perfume
{
    public int PerfumeId { get; set; }

    public int ProductId { get; set; }

    public string Code { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Smell { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Sillage { get; set; }

    public string? Info { get; set; }

    public string? Weather { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<Equalivent> Equalivents { get; set; } = new List<Equalivent>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();

    public virtual ICollection<Note> Notes1 { get; set; } = new List<Note>();

    public virtual ICollection<Note> NotesNavigation { get; set; } = new List<Note>();
}
