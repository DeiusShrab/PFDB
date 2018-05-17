using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Skill
  {
    public int SkillId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool TrainedOnly { get; set; }
    public Stat Stat { get; set; }

    public virtual ICollection<BestiarySkill> BestiarySkills { get; } = new List<BestiarySkill>();
    public virtual ICollection<CharacterSkill> CharacterSkills { get; } = new List<CharacterSkill>();
  }
}
