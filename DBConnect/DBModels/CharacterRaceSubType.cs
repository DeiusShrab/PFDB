using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class CharacterRaceSubType
  {
    public int CharacterRaceId { get; set; }
    public int BestiaryTypeId { get; set; }

    public virtual CharacterRace CharacterRace { get; set; }
    public virtual BestiaryType BestiaryType { get; set; }
  }
}
