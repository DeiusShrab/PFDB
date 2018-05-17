using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Terrain
  {
    public int TerrainId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal MovementModifier { get; set; }
    public bool IsWater { get; set; }
    public bool IsUnderground { get; set; }
    public bool IsRough { get; set; }
    public bool IsBroken { get; set; }

    public virtual ICollection<Location> Locations { get; } = new List<Location>();
  }
}
