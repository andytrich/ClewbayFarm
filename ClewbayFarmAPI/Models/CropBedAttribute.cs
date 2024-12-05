using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class CropBedAttribute
{
    public int CropId { get; set; }

    public int TimeToMaturity { get; set; }

    public decimal RowSpacing { get; set; }

    public decimal PlantSpacing { get; set; }

    public string? Notes { get; set; }

    public virtual Crop Crop { get; set; } = null!;
}
