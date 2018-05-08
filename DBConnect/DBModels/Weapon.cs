using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DBConnect.DBModels
{
  public class Weapon : Gear
  {
    public int DmgDiceNum { get; set; }
    public int DmgDiceType { get; set; }
    public int CritMin { get; set; }
    public int CritMult { get; set; }
    public int Range { get; set; }
    public int Weight { get; set; }
    public WpnType WeaponType { get; set; }
    public virtual List<WeaponAttribute> Special { get; set; }
  }
  
  public enum WpnType
  {
    B = 1,
    S = 2,
    P = 3,
  }
}
