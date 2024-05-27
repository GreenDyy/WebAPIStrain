using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Order
{
    public int IdOrder { get; set; }

    public string? IdCustomer { get; set; }

    public string? IdEmployee { get; set; }

    public DateOnly? DateOrder { get; set; }

    public double? TotalPrice { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public string? DeliveryAddress { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Customer? IdCustomerNavigation { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
