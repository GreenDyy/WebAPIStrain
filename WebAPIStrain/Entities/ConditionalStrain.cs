using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class ConditionalStrain
{
    public int IdCondition { get; set; }

    public string? Medium { get; set; }

    public string? Temperature { get; set; }

    public string? LightIntensity { get; set; }

    public string? Duration { get; set; }

    public virtual ICollection<Strain> Strains { get; set; } = new List<Strain>();
}
