using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Partner
{
    public int IdPartner { get; set; }

    public string? NameCompany { get; set; }

    public string? AddressCompany { get; set; }

    public string? NamePartner { get; set; }

    public string? Position { get; set; }

    public string? PhoneNumber { get; set; }

    public string? BankNumber { get; set; }

    public string? BankName { get; set; }

    public string? QhnsNumber { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
