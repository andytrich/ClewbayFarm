using System;
using System.Collections.Generic;

namespace ClewbayFarmAPI.Models;

public partial class PropagationArea
{
    public int AreaId { get; set; }

    public int TunnelId { get; set; }

    public string Name { get; set; } = null!;

    public int MaxTrays { get; set; }

    public virtual ICollection<ModuleTray> ModuleTrays { get; set; } = new List<ModuleTray>();

    public virtual PropagationTunnel Tunnel { get; set; } = null!;
}
