﻿namespace DBConnect.DBModels
{
  public partial class MonsterSpawn
    {
        public int SpawnId { get; set; }
        public int BestiaryId { get; set; }
        public int ContinentId { get; set; }
        public int SeasonId { get; set; }
        public int TimeId { get; set; }
    }
}
