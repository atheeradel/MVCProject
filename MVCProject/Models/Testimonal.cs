using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Testimonal
{
    public decimal TestId { get; set; }

    public decimal? UserId { get; set; }

    public string? Msg { get; set; }

    public DateTime? DateAdd { get; set; }

    public decimal? Status { get; set; }

    public virtual Userinfo? User { get; set; }
}
