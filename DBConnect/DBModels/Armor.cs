using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DBConnect.DBModels
{
  public class Armor : Gear
  {
    public ArmorType ArmTyp { get; set; }
  }

  public enum ArmorType
  {
    Light = 1,
    Medium = 2,
    Heavy = 3,
    Shield = 4,
  }
}
