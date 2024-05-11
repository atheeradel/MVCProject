using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models;

public partial class Category
{
    public decimal CatId { get; set; }
    [DisplayName("Category Name")]
    public string? CatName { get; set; }
    [DisplayName("Category Image")]
    public string? CatImg { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    [DisplayName("category Description")]
    public string? CatDes { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
