using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class EqualiventBrand
{
    public int EqualiventBrandId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Equalivent> Equalivents { get; set; } = new List<Equalivent>();
}
