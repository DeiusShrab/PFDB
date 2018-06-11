using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  class CharacterRaceAbility
  {
    public int CharacterId { get; set; }
    public int RaceAbilityId { get; set; }

    public virtual Character Character { get; set; }
    public virtual RaceAbility RaceAbility { get; set; }
  }
}
