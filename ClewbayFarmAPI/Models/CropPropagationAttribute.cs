using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class CropPropagationAttribute
{
    public int CropId { get; set; }

    public int PropagationTime { get; set; }

    public int GerminationTime { get; set; }

    public decimal? PreferredTemperature { get; set; }

    public string? Notes { get; set; }

    public virtual Crop Crop { get; set; } = null!;
}
