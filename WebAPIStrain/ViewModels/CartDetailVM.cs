    using WebAPIStrain.Entities;

    namespace WebAPIStrain.ViewModels
    {
        public class CartDetailVM
        {
            public int IdCartDetail { get; set; }

            public int? IdCart { get; set; }

            public int? IdStrain { get; set; }

            public int? QuantityOfStrain { get; set; }
            public decimal? Price { get; set; }

            public virtual Cart? IdCartNavigation { get; set; }

            public virtual Strain? IdStrainNavigation { get; set; }
        }
    }
