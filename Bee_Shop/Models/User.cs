using System;
using System.Collections.Generic;

namespace Bee_Shop.Models;

public partial class User
{
    public int Id { get; set; }

    public string? UserUsername { get; set; }

    public string? UserPassword { get; set; }

    public string? UserName { get; set; }

    public int RoleId { get; set; }

    public string? UserAvatar { get; set; }

    public string? UserEmail { get; set; }

    public string? UserPhone { get; set; }

    public string? UserAddress { get; set; }

    public DateTime? CreateDate { get; set; }

    public int Verified { get; set; }

    public string? VerificationCode { get; set; }

    public DateTime? EditTime { get; set; }
}
