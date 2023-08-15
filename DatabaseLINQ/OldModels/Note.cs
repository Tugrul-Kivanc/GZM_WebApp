using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Note
{
    public int NoteId { get; set; }

    public string Name { get; set; } = null!;

    public int AccordId { get; set; }

    public virtual Accord Accord { get; set; } = null!;

    public virtual ICollection<Perfume> PerfumesPerfumes { get; set; } = new List<Perfume>();
}
