using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public int Fee { get; set; }

    public string Payment { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
