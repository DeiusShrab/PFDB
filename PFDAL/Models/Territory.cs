﻿using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class Territory
    {
        public int TerritoryId { get; set; }
        public int? ContinentId { get; set; }
        public int? Factionid { get; set; }
        public string Name { get; set; }
    }
}
