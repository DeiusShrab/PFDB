using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class Armor : Gear
  {
    public ArmorType ArmTyp { get; set; }
    public int MaxDex { get; set; }
    public int SpellFailure { get; set; }
    public int ArmorBonus { get; set; }
    public int CheckPenalty { get; set; }
  }
}
