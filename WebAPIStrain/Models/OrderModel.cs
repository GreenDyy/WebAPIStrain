namespace WebAPIStrain.Models
{
    public class OrderModel
    {
        public int IdOrder { get; set; }

        public string? IdCustomer { get; set; }

        public string? IdEmployee { get; set; }

        public DateOnly? DateOrder { get; set; }

        public double? TotalPrice { get; set; }

        public string? Status { get; set; }

        public string? Note { get; set; }
    }
}
