using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class AuthorNewspaper
{
    public int? IdNewspaper { get; set; }

    public string? IdEmployee { get; set; }

    public DateOnly? PostDate { get; set; }

    public string? RoleOfAuthor { get; set; }

    public virtual Employee? IdEmployeeNavigation { get; set; }

    public virtual ScienceNewspaper? IdNewspaperNavigation { get; set; }
}
