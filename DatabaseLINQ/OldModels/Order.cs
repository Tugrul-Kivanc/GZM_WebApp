using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public int Quantity { get; set; }

    public int Fee { get; set; }

    public string Payment { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Product> ProductsProducts { get; set; } = new List<Product>();
}
