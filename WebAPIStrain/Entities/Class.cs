using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Class
{
    public int IdClass { get; set; }

    public string? NameClass { get; set; }

    public int? IdPhylum { get; set; }

    public virtual ICollection<Genu> Genus { get; set; } = new List<Genu>();

    public virtual Phylum? IdPhylumNavigation { get; set; }
}
