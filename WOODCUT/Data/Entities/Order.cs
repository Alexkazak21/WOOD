using System;
using System.Collections.Generic;

namespace WOODCUT.Data.Entities;

public partial class Order
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int TimberId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Timber Timber { get; set; } = null!;
}
