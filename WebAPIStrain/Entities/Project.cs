using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Project
{
    public string IdProject { get; set; } = null!;

    public string? IdEmployee { get; set; }

    public int? IdPartner { get; set; }

    public string? ProjectName { get; set; }

    public string? Results { get; set; }

    public DateOnly? StartDateProject { get; set; }

    public DateOnly? EndDateProject { get; set; }

    public string? ContractNo { get; set; }

    public string? Description { get; set; }

    public byte[]? FileProject { get; set; }

    public string? Status { get; set; }

    public string? FileName { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual Partner? IdPartnerNavigation { get; set; }

    public virtual ICollection<ProjectContent> ProjectContents { get; set; } = new List<ProjectContent>();
}
