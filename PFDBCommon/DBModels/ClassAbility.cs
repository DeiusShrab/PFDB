using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class ClassAbility
  {
    public int ClassAbilityId { get; set; }
    public int ClassId { get; set; }
    public int LevelRequirement { get; set; }
    public string ReplacesAbilities { get; set; } // For abilities that increase in strength with levels and archetype abilities
    public string Name { get; set; }
    public string Description { get; set; }
    public ClassAbilityType AbilityType { get; set; }

    public virtual List<int> ReplacesAbilitiesList
    {
      get
      {
        var ret = new List<int>();
        if (!string.IsNullOrWhiteSpace(ReplacesAbilities))
        {
          int i = 0;
          foreach (var id in ReplacesAbilities.Split(','))
          {
            if (int.TryParse(id.Trim(), out i))
              ret.Add(i);
          }
        }

        return ret;
      }
    }

    public virtual Class Class { get; set; }
    public virtual ICollection<CharacterClassAbility> CharacterClassAbilities { get; } = new List<CharacterClassAbility>();
    public virtual ICollection<OverridePrerequisite> OverridePrerequisites { get; } = new List<OverridePrerequisite>();
  }
}