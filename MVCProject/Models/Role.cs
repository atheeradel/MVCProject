using System;
using System.Collections.Generic;

namespace MVCProject.Models;

public partial class Role
{
    public decimal RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Userinfo> Userinfos { get; set; } = new List<Userinfo>();
}
