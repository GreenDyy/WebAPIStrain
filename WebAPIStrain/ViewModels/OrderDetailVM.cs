using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class OrderDetailVM
    {
        public int IdOrderDetail { get; set; }
        public int? IdOrder { get; set; }
        public int? IdStrain { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}