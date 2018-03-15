using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class SpellDetail
    {
        public int SpellId { get; set; }
        public string Description { get; set; }
        public string DescriptionFormatted { get; set; }
        public string FullText { get; set; }
    }
}
