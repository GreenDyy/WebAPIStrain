using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Species
{
    public int IdSpecies { get; set; }

    public string? NameSpecies { get; set; }

    public int? IdGenus { get; set; }

    public virtual Genu? IdGenusNavigation { get; set; }

    public virtual ICollection<Strain> Strains { get; set; } = new List<Strain>();
}
