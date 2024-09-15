using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Salehistory
{
    public int Saleid { get; set; }

    public DateOnly Saledate { get; set; }

    public int Agentid { get; set; }

    public virtual Agent Agent { get; set; } = null!;

    public int CountOfProduct => Products.Count;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
