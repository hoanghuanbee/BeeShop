using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public long? Phone { get; set; }

    public string Subject { get; set; } = null!;

    public DateTime? CreateTime { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Status { get; set; }

    public DateTime? EditTime { get; set; }
}
