namespace WebAPIStrain.Models
{
    public class OrderDetailModel
    {
        public int IdOrderDetail { get; set; }
        public int? IdOrder { get; set; }
        public int? IdStrain { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}