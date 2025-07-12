using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Medium
{
    public int Id { get; set; }

    public string? MediaName { get; set; }

    public string? Slug { get; set; }

    public DateTime CreateDate { get; set; }
}
