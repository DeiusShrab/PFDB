using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
    public partial class ContinentWeather
    {
        public int Cwid { get; set; }
        public int ContinentId { get; set; }
        public int WeatherId { get; set; }
        public int SeasonId { get; set; }
        public int ExtraWeight { get; set; }
    }
}
