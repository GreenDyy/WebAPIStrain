using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class ScienceNewspaper
{
    public int IdNewspaper { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Url { get; set; }
}
