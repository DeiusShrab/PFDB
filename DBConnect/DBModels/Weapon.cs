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
    private string DmgTypeString { get; set; }

    public virtual List<DmgType> DamageTypes
    {
      get
      {
        var ret = new List<DmgType>();

        if (!string.IsNullOrWhiteSpace(DmgTypeString))
        {
          DmgType input = 0;
          foreach (var item in DmgTypeString.Split(','))
          {
            if (Enum.TryParse(item, out input))
              ret.Add(input);
          }
        }

        return ret;
      }

      set
      {
        if (value == null || value.Count == 0)
        {
          DmgTypeString = string.Empty;
        }
        else
        {
          var sb = new StringBuilder();
          foreach (var val in value)
          {
            sb.Append(val);
            sb.Append(",");
          }

          sb.Remove(sb.Length - 1, 1);

          DmgTypeString = sb.ToString();
        }
      }
    }
    public virtual ICollection<WeaponAttributeApplied> WeaponAttributesApplied { get; } = new List<WeaponAttributeApplied>();
  }
}
