using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class CropType
{
    public int CropTypeId { get; set; } // Primary key

    public string TypeName { get; set; } = null!; // Name of the crop type

    public string Family { get; set; } = null!; // Crop family for rotation

    // Navigation property linking to the Crops table
    public virtual ICollection<Crop> Crops { get; set; } = new List<Crop>();
}
