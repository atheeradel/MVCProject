using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MVCProject.Models;

public partial class Contactu
{
    public decimal ContId { get; set; }
    [DisplayName("Contact us Title")]
    public string? Contactusbannerimg { get; set; }

    public string? Contactinfo { get; set; }
}
