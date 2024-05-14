using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Ward
{
    public int IdWards { get; set; }

    public string? Name { get; set; }

    public int? IdDistricts { get; set; }

    public virtual District? IdDistrictsNavigation { get; set; }
}
