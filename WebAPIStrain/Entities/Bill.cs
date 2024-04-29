using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Bill
{
    public string IdBill { get; set; } = null!;

    public int? IdCustomer { get; set; }

    public string? IdEmployee { get; set; }

    public DateOnly? BillDate { get; set; }

    public string? StatusOfBill { get; set; }

    public string? TypeOfBill { get; set; }

    public double? Total { get; set; }

    public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }
}
