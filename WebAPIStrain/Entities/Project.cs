using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Project
{
    public string IdProject { get; set; } = null!;

    public string? IdEmployee { get; set; }

    public int? IdPartner { get; set; }

    public string? ProjectName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public byte[]? FileForProject { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Partner? IdPartnerNavigation { get; set; }

    public virtual ICollection<ProjectContent> ProjectContents { get; set; } = new List<ProjectContent>();
}
