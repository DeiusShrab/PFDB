﻿using DBConnect.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.ConnectModels
{
  public class UpdateMonsterSpawnRequest
  {
    public int BestiaryId { get; set; }
    public int ContinentId { get; set; }
    public int SeasonId { get; set; }
    public List<MonsterSpawn> Spawns { get; set; }
  }
}
