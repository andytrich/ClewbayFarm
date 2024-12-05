using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class Crop
{
    public int CropId { get; set; }

    public string Type { get; set; } = null!;

    public string Variety { get; set; } = null!;

    public bool IsDirectSow { get; set; }

    public virtual ICollection<BedCrop> BedCrops { get; set; } = new List<BedCrop>();

    public virtual ICollection<Cover> Covers { get; set; } = new List<Cover>();

    public virtual CropBedAttribute? CropBedAttribute { get; set; }

    public virtual CropPropagationAttribute? CropPropagationAttribute { get; set; }

    public virtual ICollection<ModuleTray> ModuleTrays { get; set; } = new List<ModuleTray>();
}
