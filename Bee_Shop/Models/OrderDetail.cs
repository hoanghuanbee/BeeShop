using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public int SupplyId { get; set; }
}
