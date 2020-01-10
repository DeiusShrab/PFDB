using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Season
  {
    public int SeasonId { get; set; }
    public string Name { get; set; }
    public int SeasonOrder { get; set; }

    public virtual ICollection<MonsterSpawn> MonsterSpawns { get; } = new List<MonsterSpawn>();
    public virtual ICollection<Month> Months { get; } = new List<Month>();
    public virtual ICollection<ContinentWeather> ContinentWeathers { get; } = new List<ContinentWeather>();
  }
}
