using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class CharacterGear
  {
    public int CharacterGearId { get; set; }
    public int GearId { get; set; }
    public int CharacterId { get; set; }
    public int Quantity { get; set; }
    public string Notes { get; set; }

    public virtual Character Character { get; set; }
    public virtual Gear Gear { get; set; }
    public virtual ICollection<CharacterGearEnchantment> CharacterGearEnchantments { get; } = new List<CharacterGearEnchantment>();
  }
}
