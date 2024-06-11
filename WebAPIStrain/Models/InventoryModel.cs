using WebAPIStrain.Entities;

namespace WebAPIStrain.Models
{
    public class InventoryModel
    {
        public int? IdStrain { get; set; }

        public int? Quantity { get; set; }

        public decimal? Price { get; set; }

        public DateOnly? EntryDate { get; set; }

        public string? Histories { get; set; }
    }
}
