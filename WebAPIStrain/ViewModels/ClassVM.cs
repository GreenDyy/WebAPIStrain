﻿using WebAPIStrain.Entities;

namespace WebAPIStrain.ViewModels
{
    public class ClassVM
    {
        public int IdClass { get; set; }

        public string? NameClass { get; set; }

        public int? IdPhylum { get; set; }

        //public virtual ICollection<Genu> Genus { get; set; } = new List<Genu>();

        //public virtual Phylum? IdPhylumNavigation { get; set; }
    }
}
