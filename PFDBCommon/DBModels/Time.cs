using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Time
  {
    public int TimeId { get; set; }
    public string Name { get; set; }
    public int TimeOrder { get; set; }
    public bool IsNight { get; set; }

    public virtual ICollection<MonsterSpawn> MonsterSpawns { get; } = new List<MonsterSpawn>();
  }
}
