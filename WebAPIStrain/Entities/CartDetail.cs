using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class CartDetail
{
    public int IdCartDetail { get; set; }

    public int? IdCart { get; set; }

    public int? IdStrain { get; set; }

    public int? QuantityOfStrain { get; set; }

    public virtual Cart? IdCartNavigation { get; set; }

    public virtual Strain? IdStrainNavigation { get; set; }
}
