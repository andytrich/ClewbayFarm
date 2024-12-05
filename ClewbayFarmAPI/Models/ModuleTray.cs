using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class ModuleTray
{
    public int TrayId { get; set; }

    public int AreaId { get; set; }

    public int TrayTypeId { get; set; }

    public int? CropId { get; set; }

    public int SeedsPerModule { get; set; }

    public DateOnly? PlantingDate { get; set; }

    public int? PlantingWeek { get; set; }

    public DateOnly? RemovalDate { get; set; }

    public int? RemovalWeek { get; set; }

    public int? BedCropId { get; set; }

    public virtual PropagationArea Area { get; set; } = null!;

    public virtual BedCrop? BedCrop { get; set; }

    public virtual Crop? Crop { get; set; }

    public virtual ModuleTrayType TrayType { get; set; } = null!;

    //public static implicit operator ModuleTray(ModuleTray v)
    //{
    //    throw new NotImplementedException();
    //}
}
