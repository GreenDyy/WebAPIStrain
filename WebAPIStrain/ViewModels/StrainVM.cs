using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class StrainVM
    {
        public int IdStrain { get; set; }

        public string? StrainNumber { get; set; }

        public int? IdSpecies { get; set; }

        public int? IdCondition { get; set; }

        public byte[]? ImageStrain { get; set; }

        public string? ScientificName { get; set; }

        public string? SynonymStrain { get; set; }

        public string? FormerName { get; set; }

        public string? CommonName { get; set; }

        public string? CellSize { get; set; }

        public string? Organization { get; set; }

        public string? Characteristics { get; set; }

        public string? CollectionSite { get; set; }

        public string? Continent { get; set; }

        public string? Country { get; set; }

        public string? IsolationSource { get; set; }

        public string? ToxinProducer { get; set; }

        public string? StateOfStrain { get; set; }

        public string? AgitationResistance { get; set; }

        public string? Remarks { get; set; }

        public string? GeneInformation { get; set; }

        public string? Publications { get; set; }

        public string? RecommendedForTeaching { get; set; }

        public DateOnly? DateAdd { get; set; }

        //public virtual ICollection<BillDetail> BillDetails { get; set; } = new List<BillDetail>();

        //public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

        //public virtual ConditionalStrain? IdConditionNavigation { get; set; }

        //public virtual Species? IdSpeciesNavigation { get; set; }

        //public virtual ICollection<IdentifyStrain> IdentifyStrains { get; set; } = new List<IdentifyStrain>();

        //public virtual ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

        //public virtual ICollection<IsolatorStrain> IsolatorStrains { get; set; } = new List<IsolatorStrain>();
        public int TotalPage {  get; set; }
    }
}
