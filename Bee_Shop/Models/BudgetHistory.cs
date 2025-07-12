using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class BudgetHistory
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public string Reason { get; set; } = null!;

    public int? OrderId { get; set; }

    public int? ImportId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}
