using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class ContentWork
{
    public int IdContentWork { get; set; }

    public string? Content { get; set; }

    public int? IdProjectContent { get; set; }

    public string? IdEmployee { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ProjectContent? IdProjectContentNavigation { get; set; }
}
