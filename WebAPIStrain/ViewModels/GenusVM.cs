using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class GenusVM
    {
        public int IdGenus { get; set; }

        public string? NameGenus { get; set; }

        public int? IdClass { get; set; }

        //public virtual Class? IdClassNavigation { get; set; }

        //public virtual ICollection<Species> Species { get; set; } = new List<Species>();
    }
}
