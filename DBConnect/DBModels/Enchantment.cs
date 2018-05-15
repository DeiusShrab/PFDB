using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Enchantment
  {
    public int EnchantmentId { get; set; }
    public string Name { get; set; }
    public string Effect { get; set; }
    public int EnhancementCost { get; set; }
    public int EnchantmentType { get; set; }
    public int BonusType { get; set; }
    public int Bonus { get; set; }

    public ICollection<CharacterGear> EnchantedGear { get; set; }
  }
}
