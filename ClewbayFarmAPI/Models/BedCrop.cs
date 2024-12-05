using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class BedCrop
{
    public int BedCropId { get; set; }

    public int BedId { get; set; }

    public int CropId { get; set; }

    public DateOnly PlantingDate { get; set; }

    public DateOnly RemovalDate { get; set; }
    public int RemovalWeek { get; set; }
    public int PlantingWeek { get; set; }
    public virtual Bed Bed { get; set; } = null!;

    public virtual Crop Crop { get; set; } = null!;

    public virtual ICollection<ModuleTray> ModuleTrays { get; set; } = new List<ModuleTray>();
}
