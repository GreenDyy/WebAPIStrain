using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class BillDetail
{
    public int IdBillDetail { get; set; }

    public string? IdBill { get; set; }

    public int? IdStrain { get; set; }

    public int? Quantity { get; set; }

    public virtual Bill? IdBillNavigation { get; set; }

    public virtual Strain? IdStrainNavigation { get; set; }
}
