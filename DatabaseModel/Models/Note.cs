using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Note
{
    public int NoteId { get; set; }

    public string Name { get; set; } = null!;

    public int AccordId { get; set; }

    public virtual Accord Accord { get; set; } = null!;

    public virtual ICollection<Perfume> Perfumes { get; set; } = new List<Perfume>();

    public virtual ICollection<Perfume> Perfumes1 { get; set; } = new List<Perfume>();

    public virtual ICollection<Perfume> PerfumesNavigation { get; set; } = new List<Perfume>();
}
