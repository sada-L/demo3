using System;
using System.Collections.Generic;

namespace demo3.Models;

public partial class Type
{
    public string Name { get; set; } = null!;

    public int Typeid { get; set; }

    public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
}
