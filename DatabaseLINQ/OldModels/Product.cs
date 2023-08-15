using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public int Stock { get; set; }

    public int Price { get; set; }

    public long TotalSales { get; set; }

    public virtual ICollection<PerfumeProduct> PerfumeProducts { get; set; } = new List<PerfumeProduct>();

    public virtual ICollection<Order> OrdersOrders { get; set; } = new List<Order>();
}
