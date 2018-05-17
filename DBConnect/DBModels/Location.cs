using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Location
  {
    public int LocationId { get; set; }
    public int ContinentId { get; set; }
    public string Name { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }
    public int FactionId { get; set; }
    public int TerritoryId { get; set; }
    public int TerrainId { get; set; }
    public int EnvironmentId { get; set; }

    public virtual Continent Continent { get; set; }
    public virtual Faction Faction { get; set; }
    public virtual Territory Territory { get; set; }
    public virtual Terrain Terrain { get; set; }
    public virtual Environment Environment { get; set; }
  }
}
