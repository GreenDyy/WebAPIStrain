﻿using System;
using System.Collections.Generic;

namespace WebAPIStrain.Entities;

public partial class IsolatorStrain
{
    public string IdEmployee { get; set; } = null!;

    public int IdStrain { get; set; }

    public int? YearOfIsolator { get; set; }

    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    public virtual Strain IdStrainNavigation { get; set; } = null!;
}
