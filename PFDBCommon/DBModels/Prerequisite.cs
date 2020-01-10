using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class Prerequisite
  {
    public int PrerequisiteId { get; set; }
    public string Description { get; set; }
    public PrereqType PrereqTypeNeed { get; }
    public PrereqType PrereqTypeFor { get; }
    public int NeedId { get; set; }
    public int ForId { get; set; }
    public Stat Stat { get; set; }
    public int Value { get; set; }

    [NotMapped]
    public virtual object Need { get; set; }
    [NotMapped]
    public virtual object For { get; set; }

    public virtual ICollection<OverridePrerequisite> OverridePrerequisites { get; } = new List<OverridePrerequisite>();
  }
}
