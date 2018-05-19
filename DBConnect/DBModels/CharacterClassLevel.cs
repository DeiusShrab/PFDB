using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class CharacterClassLevel
  {
    public int CharacterId { get; set; }
    public int ClassId { get; set; }
    public int Level { get; set; }

    public virtual Character Character { get; set; }
    public virtual Class Class { get; set; }
  }
}
