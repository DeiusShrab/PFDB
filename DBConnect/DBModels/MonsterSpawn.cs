using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class MonsterSpawn
  {
    public int SpawnId { get; set; }
    public int ContinentId { get; set; }
    public int SeasonId { get; set; }
    public int BestiaryId { get; set; }
    public int TimeId { get; set; }
    public int PlaneId { get; set; }

    public virtual Continent Continent { get; set; }
    public virtual Season Season { get; set; }
    public virtual Bestiary Bestiary { get; set; }
    public virtual Time Time { get; set; }
    public virtual Plane Plane { get; set; }
  }
}
