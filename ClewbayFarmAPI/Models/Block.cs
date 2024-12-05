using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class Block
{
    public int BlockId { get; set; }

    public string Name { get; set; } = null!;

    public int BlockTypeId { get; set; }

    public double Length { get; set; }

    public double Width { get; set; }

    public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();

    public virtual BlockType BlockType { get; set; } = null!;
}
