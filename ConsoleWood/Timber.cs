using System;
using System.Collections.Generic;

namespace ConsoleWood;

public partial class Timber
{
    public int Id { get; set; }

    public double Length { get; set; }

    public int Diameter { get; set; }

    public double Volume { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
