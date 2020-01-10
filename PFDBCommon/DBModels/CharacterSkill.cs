using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class CharacterSkill
  {
    public int CharacterId { get; set; }
    public int SkillId { get; set; }
    public int Ranks { get; set; }

    public virtual Character Character { get; set; }
    public virtual Skill Skill { get; set; }
  }
}
