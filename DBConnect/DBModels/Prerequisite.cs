using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Prerequisite
  {
    public int PrerequisiteId { get; set; }
    public string Description { get; set; }
    public PrereqType PrereqType { get; }
    public int? FeatId { get; set; }
    public int? SkillId { get; set; }
    public int? ClassId { get; set; }
    public Stat Stat { get; set; }
    public int Value { get; set; }

    public virtual Feat Feat { get; set; }
    public virtual Skill Skill { get; set; }
    public virtual Class Class { get; set; }

    public virtual ICollection<OverridePrerequisite> OverridePrerequisites { get; } = new List<OverridePrerequisite>();
  }
}
