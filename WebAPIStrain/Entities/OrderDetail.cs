using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class OrderDetail
{
    public int IdOrderDetail { get; set; }

    public int? IdOrder { get; set; }

    public int? IdStrain { get; set; }

    public int? Quantity { get; set; }

    public double? Price { get; set; }

    public virtual Order? IdOrderNavigation { get; set; }

    public virtual Strain? IdStrainNavigation { get; set; }
}
