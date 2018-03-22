using System;
using System.Collections.Generic;
using System.Text;

namespace PFDAL.Models
{
    public partial class BestiaryEnvironment
    {
        public int BestiaryId { get; set; }
        public int EnvironmentId { get; set; }
        public string Notes { get; set; }
    }
}
