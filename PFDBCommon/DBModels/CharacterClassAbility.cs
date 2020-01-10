using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
    public class CharacterClassAbility
    {
    public int CharacterClassAbilityId { get; set; }
    public int CharacterId { get; set; }
    public int ClassAbilityId { get; set; }
    public string Note { get; set; } // ex: Favored Enemy (Goblinoids)

    public virtual Character Character { get; set; }
    public virtual ClassAbility ClassAbility { get; set; }
  }
}
