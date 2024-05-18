using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models;

public partial class Homepage
{
    public decimal PageId { get; set; }

    public string? Phonenum { get; set; }
    [DisplayName("Image logo")]
    public string? Imglogo { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
    [DisplayName(" First Banner")]
    public string? Imgbannerone { get; set; }
    //[NotMapped]
    //public IFormFile ImageFile1 { get; set; }
    [DisplayName(" Second Banner")]
    public string? Imgbannertwo { get; set; }
    //[NotMapped]
    //public IFormFile ImageFile2 { get; set; }
    [DisplayName(" Third Banner")]
    public string? Imgbannerthree { get; set; }
    //[NotMapped]
    //public IFormFile ImageFile3 { get; set; }
    [DisplayName("  Banner Description")]
    public string? Bannerdescription { get; set; }
    [DisplayName(" Image Desgin")]
    public string? Imgonedesign { get; set; }
    //[NotMapped]
    //public IFormFile ImageFile4 { get; set; }
    [DisplayName(" Image Desgin two")]
    public string? Imgtwodesign { get; set; }
    //[NotMapped]
    //public IFormFile ImageFile5 { get; set; }
}
