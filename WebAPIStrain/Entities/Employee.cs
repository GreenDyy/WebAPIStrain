using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Employee
{
    public string IdEmployee { get; set; } = null!;

    public int? IdRole { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? FullName { get; set; }

    public string? IdCard { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Degree { get; set; }

    public string? Address { get; set; }

    public DateOnly? JoinDate { get; set; }

    public byte[]? ImageEmployee { get; set; }

    public virtual AccountForEmployee? AccountForEmployee { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<ContentWork> ContentWorks { get; set; } = new List<ContentWork>();

    public virtual RoleForEmployee? IdRoleNavigation { get; set; }

    public virtual ICollection<IdentifyStrain> IdentifyStrains { get; set; } = new List<IdentifyStrain>();

    public virtual ICollection<IsolatorStrain> IsolatorStrains { get; set; } = new List<IsolatorStrain>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
