using System;
using System.Collections.Generic;

namespace DatabaseLINQ.OldModels;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;
}
