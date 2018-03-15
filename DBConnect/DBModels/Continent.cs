using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
    public partial class Continent
    {
        public int ContinentId { get; set; }
        public string Name { get; set; }
        public bool? IsUnderground { get; set; }
        public bool? IsWater { get; set; }
        public string PrimaryLanguage { get; set; }
        public string MapPath { get; set; }
    }
}
