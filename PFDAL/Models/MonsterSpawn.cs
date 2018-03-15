using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class MonsterSpawn
    {
        public int SpawnId { get; set; }
        public int? Continent { get; set; }
        public int? Season { get; set; }
        public int? BestiaryId { get; set; }
    }
}
