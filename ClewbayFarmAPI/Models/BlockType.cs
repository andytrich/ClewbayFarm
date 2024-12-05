using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class BlockType
{
    public int BlockTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Block> Blocks { get; set; } = new List<Block>();
}
