﻿using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class Faction
    {
        public int FactionId { get; set; }
        public int? ContinentId { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public int? ParentFaction { get; set; }
        public string PrimaryRace { get; set; }
        public string Type { get; set; }
    }
}
