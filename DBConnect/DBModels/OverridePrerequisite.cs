using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class OverridePrerequisite
  {
    public int ClassAbilityId { get; set; }
    public int PrerequisiteId { get; set; }

    public virtual ClassAbility ClassAbility { get; set; }
    public virtual Prerequisite Prerequisite { get; set; }
  }
}
