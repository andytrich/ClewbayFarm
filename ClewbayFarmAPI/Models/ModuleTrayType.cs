using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class ModuleTrayType
{
    public int TrayTypeId { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfModules { get; set; }

    public virtual ICollection<ModuleTray> ModuleTrays { get; set; } = new List<ModuleTray>();
}
