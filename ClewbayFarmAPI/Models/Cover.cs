using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class Cover
{
    public int CoverId { get; set; }

    public int CropId { get; set; }

    public string CoverType { get; set; } = null!;

    public int StartWeek { get; set; }

    public int EndWeek { get; set; }

    public string? Notes { get; set; }

    public virtual Crop Crop { get; set; } = null!;
}
