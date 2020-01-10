using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
    public class CharacterGearEnchantment
    {
    public int CharacterGearId { get; set; }
    public int EnchantmentId { get; set; }
    public virtual CharacterGear CharacterGear { get; set; }
    public virtual Enchantment Enchantment { get; set; }
  }
}
