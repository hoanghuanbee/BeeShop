using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Supply
{
    public int Id { get; set; }

    public int ImportId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int UnitCost { get; set; }
}
