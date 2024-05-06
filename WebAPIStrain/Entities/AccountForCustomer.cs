using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class AccountForCustomer
{
    public string IdCustomer { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
