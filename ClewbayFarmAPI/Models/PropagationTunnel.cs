using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class PropagationTunnel
{
    public int TunnelId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PropagationArea> PropagationAreas { get; set; } = new List<PropagationArea>();
}
