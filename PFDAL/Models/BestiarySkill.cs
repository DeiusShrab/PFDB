using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
    public partial class BestiarySkill
    {
        public int BestiaryId { get; set; }
        public int SkillId { get; set; }
        public int Bonus { get; set; }
        public string Notes { get; set; }
    }
}
