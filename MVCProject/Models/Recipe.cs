using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models;

public partial class Recipe
{
    public decimal RecId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? CatId { get; set; }
    [DisplayName("Name")]
    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Ingrediants { get; set; }

    public string? Instruction { get; set; }
    [DisplayName("Date of recipe")]
    public DateTime? Dateadd { get; set; }

    public string? Image { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public decimal? Status { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual Userinfo? User { get; set; }

    public virtual ICollection<Userrecipe> Userrecipes { get; set; } = new List<Userrecipe>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
