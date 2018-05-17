using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Continent
  {
    public int ContinentId { get; set; }
    public string Name { get; set; }
    public bool IsUnderground { get; set; }
    public bool IsWater { get; set; }
    public int PrimaryLanguageId { get; set; }
    public string MapPath { get; set; }

    public virtual Language PrimaryLanguage { get; set; }
    public virtual ICollection<ContinentWeather> ContinentWeathers { get; } = new List<ContinentWeather>();
    public virtual ICollection<Location> Locations { get; } = new List<Location>();
    public virtual ICollection<MonsterSpawn> MonsterSpawns { get; } = new List<MonsterSpawn>();
    public virtual ICollection<Territory> Territories { get; } = new List<Territory>();
  }
}
