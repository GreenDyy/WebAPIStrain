using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class District
{
    public int IdDistricts { get; set; }

    public string? Name { get; set; }

    public int? IdProvinces { get; set; }

    public virtual Province? IdProvincesNavigation { get; set; }

    public virtual ICollection<Ward> Wards { get; set; } = new List<Ward>();
}
