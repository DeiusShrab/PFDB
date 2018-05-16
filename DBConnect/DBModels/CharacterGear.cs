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
    public virtual ICollection<CharacterGearEnchantment> CharacterGearEnchantments { get; } = new List<CharacterGearEnchantment>();
  }
}
