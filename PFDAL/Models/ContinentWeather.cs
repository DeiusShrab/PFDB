using System;
using System.Collections.Generic;

namespace PFDAL.Models
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
