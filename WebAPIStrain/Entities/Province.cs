using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Province
{
    public int IdProvinces { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
}
