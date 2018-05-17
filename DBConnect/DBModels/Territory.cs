using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Territory
  {
    public int TerritoryId { get; set; }
    public int ContinentId { get; set; }
    public int Factionid { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }

    public virtual Continent Continent { get; set; }
    public virtual Faction Faction { get; set; }
    public virtual ICollection<Location> Locations { get; } = new List<Location>();
  }
}
