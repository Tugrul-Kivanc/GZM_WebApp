using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Accord
{
    public int AccordId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}
