using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Type
{
    public int Id { get; set; }

    public string? TypeName { get; set; }

    public string? TypeDescription { get; set; }

    public string Slug { get; set; } = null!;
}
