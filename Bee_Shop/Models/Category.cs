using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public int? SupplyId { get; set; }

    public int? CategoryPosition { get; set; }

    public string Slug { get; set; } = null!;
}
