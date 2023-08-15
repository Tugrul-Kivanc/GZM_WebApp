using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Accord
{
    public int AccordId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
