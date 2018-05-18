using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class WeaponAttribute
  {
    public int WpnAttId { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }

    public virtual ICollection<WeaponAttributeApplied> WeaponAttributesApplied { get; } = new List<WeaponAttributeApplied>();
  }
}
