﻿namespace WebAPIStrain.Models
{
    public class BillModel
    {
        public int? IdOrder { get; set; }
        public string? IdCustomer { get; set; }
        public string? IdEmployee { get; set; }
        public DateOnly? BillDate { get; set; }
        public string? StatusOfBill { get; set; }
        public string? TypeOfBill { get; set; }
        public double? Total { get; set; }
    }
}