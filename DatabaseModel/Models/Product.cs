using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    public long TotalSales { get; set; }

    public int CategoryId { get; set; }

    public int? PerfumeId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Perfume? Perfume { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
