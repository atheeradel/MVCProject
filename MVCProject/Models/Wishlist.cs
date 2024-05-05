using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Wishlist
{
    public decimal WishId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? RecId { get; set; }

    public virtual Recipe? Rec { get; set; }

    public virtual Userinfo? User { get; set; }
}
