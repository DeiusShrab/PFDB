using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class BestiarySubType
    {
        public int BestiarySubTypeId { get; set; }
        public int BestiaryId { get; set; }
        public int BestiaryTypeId { get; set; }
    }
}
