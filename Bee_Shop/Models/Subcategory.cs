using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Subcategory
{
    public int Id { get; set; }

    public string? SubcategoryName { get; set; }

    public int? SupplyId { get; set; }

    public int? CategoryId { get; set; }

    public string Slug { get; set; } = null!;
}
