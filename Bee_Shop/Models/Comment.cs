using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class Comment
{
    public int Id { get; set; }

    public int? ParentCommentId { get; set; }

    public int UserId { get; set; }

    public string? Content { get; set; }

    public DateTime? CreateDate { get; set; }

    public string? Author { get; set; }

    public string? Email { get; set; }

    public int Status { get; set; }

    public string LinkImage { get; set; } = null!;

    public DateTime? EditTime { get; set; }

    public int? ProductId { get; set; }

    public int? PostId { get; set; }

    public int? PageId { get; set; }
}
