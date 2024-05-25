namespace WebAPIStrain.ViewModels
{
    public class BillVM
    {
        public string IdBill { get; set; }
        public string? IdCustomer { get; set; }
        public string? IdEmployee { get; set; }
        public DateOnly? BillDate { get; set; }
        public string? StatusOfBill { get; set; }
        public string? TypeOfBill { get; set; }
        public double? TotalPrice { get; set; }
    }
}