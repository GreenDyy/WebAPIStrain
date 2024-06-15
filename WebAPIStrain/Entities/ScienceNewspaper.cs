using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class ScienceNewspaper
{
    public int IdNewspaper { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public DateOnly? PostDate { get; set; }

    public byte[]? Image { get; set; }

    public string? IdEmployee { get; set; }

    public string? Content2 { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }
}
