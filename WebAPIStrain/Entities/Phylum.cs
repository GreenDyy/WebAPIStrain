using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Phylum
{
    public int IdPhylum { get; set; }

    public string? NamePhylum { get; set; }

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
