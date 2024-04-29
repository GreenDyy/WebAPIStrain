using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Genu
{
    public int IdGenus { get; set; }

    public string? NameGenus { get; set; }

    public int? IdClass { get; set; }

    public virtual Class? IdClassNavigation { get; set; }

    public virtual ICollection<Species> Species { get; set; } = new List<Species>();
}
