using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
    public partial class BestiaryDetail
    {
        public int BestiaryId { get; set; }
        public string MonsterSource { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public string FullText { get; set; }
    }
}
