using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Cart
{
    public int IdCart { get; set; }

    public string? IdCustomer { get; set; }

    public int? ToatalProduct { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Customer? IdCustomerNavigation { get; set; }
}
