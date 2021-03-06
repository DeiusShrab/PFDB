﻿using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class SpellSubSchool
  {
    public int SpellSubSchoolId { get; set; }
    public int SpellSchoolId { get; set; }
    public string Name { get; set; }

    public virtual SpellSchool SpellSchool { get; set; }
    public virtual ICollection<Spell> Spells { get; } = new List<Spell>();
  }
}
