using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class Environment
    {
        public int EnvironmentId { get; set; }
        public string Name { get; set; }
        public string Temperature { get; set; }
        public string Notes { get; set; }
        public int TravelSpeed { get; set; }
    }
}
