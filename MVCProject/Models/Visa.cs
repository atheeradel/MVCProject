using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Visa
{
    public decimal VisaId { get; set; }

    public string? Cardname { get; set; }

    public string? Cardnum { get; set; }

    public string? Cvc { get; set; }

    public DateTime? Expiredate { get; set; }

    public decimal? Amountofmoney { get; set; }
}
