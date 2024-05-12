using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVCProject.Models;

public partial class Userrecipe
{
    public decimal ReqId { get; set; }

    [DisplayName("Recipe Name")]
    public decimal? RecId { get; set; }
    [DisplayName("User Name")]
    public decimal? UserId { get; set; }
    [DisplayName("Purchase Date")]
    public DateTime? ReqDate { get; set; }

    public decimal? Status { get; set; }

    public virtual Recipe? Rec { get; set; }

    public virtual Userinfo? User { get; set; }
}
