using WebAPIStrain.Entities;

namespace WebAPIStrain.Models
{
    public class CartDetailModel
    {
        public int? IdCart { get; set; }

        public int? IdStrain { get; set; }

        public int? QuantityOfStrain { get; set; }
    }
}
