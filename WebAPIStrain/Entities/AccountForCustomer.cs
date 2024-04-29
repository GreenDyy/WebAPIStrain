using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class AccountForCustomer
{
    public int IdAccountForCustomer { get; set; }

    public int? Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public virtual Customer? IdNavigation { get; set; }
}
