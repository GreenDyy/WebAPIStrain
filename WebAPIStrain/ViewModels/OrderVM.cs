using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class OrderVM
    {
        public int IdOrder { get; set; }

        public string? IdCustomer { get; set; }

        public string? IdEmployee { get; set; }

        public DateOnly? DateOrder { get; set; }

        public double? TotalPrice { get; set; }

        public string? Status { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? Note { get; set; }

        public string? PaymentMethod { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
