using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Userrecipe
{
    public decimal ReqId { get; set; }

    public decimal? RecId { get; set; }

    public decimal? UserId { get; set; }

    public DateTime? ReqDate { get; set; }

    public decimal? Status { get; set; }

    public virtual Recipe? Rec { get; set; }

    public virtual Userinfo? User { get; set; }
}
