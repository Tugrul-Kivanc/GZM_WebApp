using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Equalivent
{
    public int EqualiventId { get; set; }

    public int EqualiventBrandId { get; set; }

    public int PerfumeId { get; set; }

    public string Code { get; set; } = null!;

    public virtual EqualiventBrand EqualiventBrand { get; set; } = null!;

    public virtual Perfume Perfume { get; set; } = null!;
}
