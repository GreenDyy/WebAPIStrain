using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class StrainApprovalHistory
{
    public int Id { get; set; }

    public int? IdStrain { get; set; }

    public string? Status { get; set; }

    public DateOnly? DateApproval { get; set; }

    public string? Reason { get; set; }

    public virtual Strain? IdStrainNavigation { get; set; }
}
