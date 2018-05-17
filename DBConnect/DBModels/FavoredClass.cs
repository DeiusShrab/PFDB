using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class FavoredClass
  {
    public int FavoredClassId { get; set; }
    public int ClassId { get; set; }
    public int RaceId { get; set; }
    public string Description { get; set; }

    public virtual CharacterClass CharacterClass { get; set; }
    public virtual CharacterRace CharacterRace { get; set; }
  }
}
