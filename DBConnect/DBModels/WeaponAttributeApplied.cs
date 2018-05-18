using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
    public class WeaponAttributeApplied
    {
    public int GearId { get; set; }
    public int WeaponAttributeId { get; set; }
    public string Note { get; set; }

    public virtual Weapon Weapon { get; set; }
    public virtual WeaponAttribute Attribute { get; set; }
  }
}
