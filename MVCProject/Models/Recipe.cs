using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Recipe
{
    public decimal RecId { get; set; }

    public decimal? UserId { get; set; }

    public decimal? CatId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Ingrediants { get; set; }

    public string? Instruction { get; set; }

    public DateTime? Dateadd { get; set; }

    public string? Image { get; set; }

    public decimal? Status { get; set; }

    public virtual Category? Cat { get; set; }

    public virtual Userinfo? User { get; set; }

    public virtual ICollection<Userrecipe> Userrecipes { get; set; } = new List<Userrecipe>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
}
