using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PFDBCommon.DBModels
{
  public partial class BestiarySkill
  {
    public int BestiarySkillId { get; set; }
    public int BestiaryId { get; set; }
    public int SkillId { get; set; }
    public int Bonus { get; set; }
    public string Notes { get; set; }

    public virtual Bestiary Bestiary { get; set; }
    public virtual Skill Skill { get; set; }
  }
}
