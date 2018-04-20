using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
    public partial class SpellSubSchool
    {
        public int SpellSubSchoolId { get; set; }
        public int SpellSchoolId { get; set; }
        public string Name { get; set; }
    }
}
