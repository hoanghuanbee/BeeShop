using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class StockImport
{
    public int ImportId { get; set; }

    public string SupplierName { get; set; } = null!;

    public DateTime ImportDate { get; set; }

    public string InvoiceId { get; set; } = null!;

    public int TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }
}
