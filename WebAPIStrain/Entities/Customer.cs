using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Customer
{
    public string IdCustomer { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public byte[]? Image { get; set; }

    public string? NameWard { get; set; }

    public string? NameDistrict { get; set; }

    public string? NameProvince { get; set; }

    public virtual AccountForCustomer? AccountForCustomer { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
