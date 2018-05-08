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
  }

  public enum EnchantType
  {
    WpnR = 1,
    WpnM = 2,
    WpnA = 3,
    Armor = 4,
    Cast = 5,
  }

  // Bonus types stack with each other, but not multiples of themselves (with the exception of Dodge bonuses)
  public enum BonusType
  {
    Other = 0,
    Alch = 1, // Alchemical
    Armor = 2,
    BAB = 3,
    Circ = 4, // Circumstance
    Comp = 5, // Competence
    Defl = 6, // Deflection
    Dodge = 7,
    Enh = 8, // Enhancement
    Inh = 9, // Inherent
    Insi = 10, // Insight
    Luck = 11,
    Morale = 12,
    NatArm = 13,
    Prof = 14, // Profane
    Racial = 15, 
    Resist = 16,
    Sacred = 17,
    Shield = 18,
    Size = 19,
    Trait = 20,
  }
}
