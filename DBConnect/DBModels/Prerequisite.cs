using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Prerequisite
  {
    public int PrerequisiteId { get; set; }
    public string Description { get; set; }
    public PrereqType PrereqTypeNeed { get; set; }
    public PrereqType PrereqTypeFor { get; set; }
    public int NeedId { get; set; }
    public int ForId { get; set; }
    public Stat Stat { get; set; }
    public int Value { get; set; }

    public virtual object Need { get; set; }
    public virtual object For { get; set; }

    public virtual ICollection<OverridePrerequisite> OverridePrerequisites { get; } = new List<OverridePrerequisite>();
  }
}
