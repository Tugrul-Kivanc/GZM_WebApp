using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class PerfumeProduct
{
    public int PerfumeId { get; set; }

    public int ProductId { get; set; }

    public virtual Perfume Perfume { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
