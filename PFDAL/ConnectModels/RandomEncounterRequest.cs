using System;
using System.Collections.Generic;
using System.Text;

namespace PFDAL.ConnectModels
{
    public class RandomEncounterRequest
    {
        public int[] Crs { get; set; }
        public bool Group { get; set; }
        public bool Npc { get; set; }
        public int ContinentId { get; set; }
        public int TerrainId { get; set; }
        public int TimeId { get; set; }
    }
}
