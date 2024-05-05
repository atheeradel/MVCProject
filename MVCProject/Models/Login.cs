using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Login
{
    public decimal LogId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public decimal? UserId { get; set; }

    public virtual Userinfo? User { get; set; }
}
