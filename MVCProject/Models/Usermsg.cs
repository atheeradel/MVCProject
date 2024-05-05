using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Usermsg
{
    public decimal MsgId { get; set; }

    public decimal? UserId { get; set; }

    public string? Email { get; set; }

    public string? Username { get; set; }

    public string? Subject { get; set; }

    public string? Msg { get; set; }

    public virtual Userinfo? User { get; set; }
}
