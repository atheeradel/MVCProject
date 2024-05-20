using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVCProject.Models;

public partial class Profilephoto
{
    [Key]
    public decimal PhotoId { get; set; }
    [DisplayName("Name")]
    public decimal? UserId { get; set; }
    [DisplayName("Subscribed Email")]
    public string? Photo { get; set; }

    public virtual Userinfo? User { get; set; }
}
