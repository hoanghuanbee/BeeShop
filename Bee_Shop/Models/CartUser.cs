using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class CartUser
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public int Number { get; set; }
}
