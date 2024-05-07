using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class CartVM
    {
        public int IdCart { get; set; }

        public string? IdCustomer { get; set; }

        public int? TotalProduct { get; set; }

        //public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

        //public virtual Customer? IdCustomerNavigation { get; set; }
    }
}
