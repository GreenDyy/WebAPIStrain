using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class SpeciesVM
    {
        public int IdSpecies { get; set; }

        public string? NameSpecies { get; set; }

        public int? IdGenus { get; set; }

        //public virtual Genu? IdGenusNavigation { get; set; }

        //public virtual ICollection<Strain> Strains { get; set; } = new List<Strain>();
    }
}
