using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public int ProductTypeid { get; set; }

    public int? CategoryId { get; set; }

    public int? SubCategoryId { get; set; }

    public int? SupplyId { get; set; }

    public string? ProductDescription { get; set; }

    public int ProductPrice { get; set; }

    public string? ProductColor { get; set; }

    public string? ProductMaterial { get; set; }

    public string? ProductSize { get; set; }

    public string? ProductDetail { get; set; }

    public string? CreateBy { get; set; }

    public DateOnly? CreateDate { get; set; }

    public string? EditBy { get; set; }

    public DateTime? EditDate { get; set; }

    public int? TotalView { get; set; }

    public byte? Saleoff { get; set; }

    public int? Percentoff { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public string? Img3 { get; set; }

    public string? Img4 { get; set; }

    public string Slug { get; set; } = null!;
}
