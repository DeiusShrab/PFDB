using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Gear
  {
    public int GearId { get; set; }
    public decimal Cost { get; set; } // Cost in GP, decimal is SP and CP
    public string Name { get; set; }
    public int Type { get; set; }
    public int Weight { get; set; }

    public virtual ICollection<CharacterGear> CharacterGear { get; } = new List<CharacterGear>();
  }
}
