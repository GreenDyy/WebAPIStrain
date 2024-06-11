using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class InventoryVM
    {
        public int InventoryId { get; set; }

        public int? IdStrain { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public DateOnly? EntryDate { get; set; }

        public string? Histories { get; set; }

        public virtual Strain? IdStrainNavigation { get; set; }
    }
}
