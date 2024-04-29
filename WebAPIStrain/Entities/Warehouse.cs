using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Warehouse
{
    public int IdStrain { get; set; }

    public int? QuantityOfStrain { get; set; }

    public virtual Strain IdStrainNavigation { get; set; } = null!;
}
