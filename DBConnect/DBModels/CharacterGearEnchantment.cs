using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
    public class CharacterGearEnchantment
    {
    public int CharacterGearId { get; set; }
    public CharacterGear CharacterGear { get; set; }
    public int EnchantmentId { get; set; }
    public Enchantment Enchantment { get; set; }
  }
}
