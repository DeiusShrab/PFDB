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
    public DmgType DamageType { get; set; }

    public virtual ICollection<WeaponAttribute> WeaponAttributes { get; } = new List<WeaponAttribute>();
  }
}
