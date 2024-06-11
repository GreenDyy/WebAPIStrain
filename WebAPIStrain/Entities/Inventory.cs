using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class Inventory
{
    public int InventoryId { get; set; }

    public int? IdStrain { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? EntryDate { get; set; }

    public string? Histories { get; set; }

    public virtual Strain? IdStrainNavigation { get; set; }
}
