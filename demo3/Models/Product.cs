using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Product
{
    public int Productid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Salehistory> Sales { get; set; } = new List<Salehistory>();
}
