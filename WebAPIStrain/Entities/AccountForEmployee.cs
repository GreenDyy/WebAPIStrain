using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class AccountForEmployee
{
    public string IdEmployee { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;
}
