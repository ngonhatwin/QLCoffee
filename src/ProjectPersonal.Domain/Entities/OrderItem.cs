using ProjectPersonal.Domain.Common;
using System;
using System.Collections.Generic;

namespace ProjectPersonal;

public partial class OrderItem : EntityBase
{
    public Guid? OrderId { get; set; }

    public Guid? ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Product? Product { get; set; }
}
