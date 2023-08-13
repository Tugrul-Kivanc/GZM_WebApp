using System;
using System.Collections.Generic;

namespace DatabaseModel.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
