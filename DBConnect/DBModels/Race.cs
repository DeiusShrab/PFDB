using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Race
  {
    public int RaceId { get; set; }
    public int? BestiaryId { get; set; } // Generic bestiary entry for race
    public int? RaceTypeId { get; set; }
    public int RP { get; set; }
    public int ModSTR { get; set; }
    public int ModDEX { get; set; }
    public int ModCON { get; set; }
    public int ModINT { get; set; }
    public int ModWIS { get; set; }
    public int ModCHA { get; set; }

    public virtual Bestiary RaceBestiary { get; set; }    
    public virtual ICollection<Character> Characters { get; } = new List<Character>();
    public virtual ICollection<FavoredClass> FavoredClasses { get; } = new List<FavoredClass>();
    public virtual ICollection<RaceSubType> RaceSubTypes { get; } = new List<RaceSubType>();
  }
}
