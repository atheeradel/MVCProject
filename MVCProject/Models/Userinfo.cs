using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models;

public partial class Userinfo
{
    public decimal UserId { get; set; }
    [DisplayName(" First Name")]
    public string Firstname { get; set; } = null!;
    [DisplayName(" Last Name")]
    public string Lastname { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string? Address { get; set; }

    public decimal? Age { get; set; }
    [DisplayName("Phone Number")]
    public string? Phonenum { get; set; }

    public decimal? RoleId { get; set; }
    [DisplayName("Image")]
    public string? ImagePath { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Testimonal> Testimonals { get; set; } = new List<Testimonal>();

    public virtual ICollection<Usermsg> Usermsgs { get; set; } = new List<Usermsg>();

    public virtual ICollection<Userrecipe> Userrecipes { get; set; } = new List<Userrecipe>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
