using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCProject.Models;

public partial class Aboutu
{
    public decimal AboutId { get; set; }

    public string? Aboutusimgbanner { get; set; }
    [DisplayName("Main Image")]
    public string? Aboutusmainimg { get; set; }
    [DisplayName("About Us Description")]
    public string? Aboutusdescription { get; set; }
    [DisplayName("Trusted info")]
    public string? Trustedinfo { get; set; }
    [DisplayName("Proffissonal info")]
    public string? Profinfo { get; set; }
    [DisplayName("Expert info")]
    public string? Expertinfo { get; set; }
    [NotMapped]
    public IFormFile ImageFile { get; set; }
}
