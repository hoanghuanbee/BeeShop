using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Order
{
    public int Id { get; set; }

    public string Customer { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public double CartTotal { get; set; }

    public DateTime Createtime { get; set; }

    public string? Message { get; set; }

    public int Status { get; set; }

    public int UserId { get; set; }

    public DateTime? EditTime { get; set; }

    public int PaymentMethod { get; set; }
}
