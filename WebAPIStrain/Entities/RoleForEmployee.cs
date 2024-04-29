using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class RoleForEmployee
{
    public int IdRole { get; set; }

    public string? RoleName { get; set; }

    public string? RoleDescription { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
