using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class ClassAbility
  {
    public int ClassAbilityId { get; set; }
    public int ClassId { get; set; }
    public int LevelRequirement { get; set; }
    public int ReplacesAbilityId { get; set; } // For abilities that increase in strength with levels
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual CharacterClass CharacterClass { get; set; }
    public virtual ICollection<CharacterClassAbility> CharacterClassAbilities { get; } = new List<CharacterClassAbility>();
  }
}