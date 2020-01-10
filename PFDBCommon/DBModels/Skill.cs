using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Skill
  {
    public int SkillId { get; set; }
    public string Name { get; set; }
    public string Option { get; set; } // e.g. Knowledge (Local) or Knowledge (Dungeoneering)
    public string Description { get; set; }
    public bool TrainedOnly { get; set; }
    public Stat Stat { get; set; }

    public virtual ICollection<BestiarySkill> BestiarySkills { get; } = new List<BestiarySkill>();
    public virtual ICollection<CharacterSkill> CharacterSkills { get; } = new List<CharacterSkill>();
    public virtual ICollection<ClassSkill> ClassSkills { get; } = new List<ClassSkill>();
  }
}
