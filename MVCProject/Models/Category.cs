using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Category
{
    public decimal CatId { get; set; }

    public string? CatName { get; set; }

    public string? CatImg { get; set; }

    public string? CatDes { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
