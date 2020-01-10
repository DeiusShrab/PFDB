using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Plane
  {
    public int PlaneId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<MonsterSpawn> MonsterSpawns { get; } = new List<MonsterSpawn>();
  }
}
