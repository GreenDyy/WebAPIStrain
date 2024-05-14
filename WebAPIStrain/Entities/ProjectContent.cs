using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class ProjectContent
{
    public int IdProjectContent { get; set; }

    public string? IdProject { get; set; }

    public string? NameContent { get; set; }

    public string? Results { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? ContractNo { get; set; }

    public string? Status { get; set; }

    public string? Priority { get; set; }

    public virtual ICollection<ContentWork> ContentWorks { get; set; } = new List<ContentWork>();

    public virtual Project? IdProjectNavigation { get; set; }
}
