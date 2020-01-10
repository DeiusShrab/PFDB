using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class SpellSchool
  {
    public int SpellSchoolId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Spell> Spells { get; } = new List<Spell>();
    public virtual ICollection<SpellSubSchool> SpellSubSchools { get; } = new List<SpellSubSchool>();
  }
}
