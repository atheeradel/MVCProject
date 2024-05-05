using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Profilephoto
{
    public decimal PhotoId { get; set; }

    public decimal? UserId { get; set; }

    public string? Photo { get; set; }

    public virtual Userinfo? User { get; set; }
}
