using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class Bed
{
    public int BedId { get; set; }

    public int BlockId { get; set; }

    public int Position { get; set; }

    public virtual ICollection<BedCrop> BedCrops { get; set; } = new List<BedCrop>();

    public virtual Block Block { get; set; } = null!;
}

