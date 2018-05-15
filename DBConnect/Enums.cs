using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect
{
  public enum GearType
  {
    Misc,
    Consumable,
    WeaponMelee,
    WeaponRanged,
    Shield,
    Head,
    Headband,
    Eyes,
    Shoulders,
    Neck,
    Chest,
    Body,
    Armor,
    Belt,
    Wrists,
    Hands,
    Ring,
    Feet,
  }

  public enum ArmorType
  {
    Light = 1,
    Medium = 2,
    Heavy = 3,
    Shield = 4,
  }

  public enum MagicType
  {
    Su = 1,
    Sq = 2,
    Ex = 3,
    Prep = 4,
    Spont = 5,
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

  public enum TrackedEventType
  {
    CalendarEvent = 1,
    UpdateCampaignNumber = 2 // Data Format: "Key(string);Operation(char);Amount(decimal)" - ex: "DaysUntilFestival;-;1", "GoblinBankAccount;*;1.0025"
  }

  public enum DmgType
  {
    B = 1,
    S = 2,
    P = 3,
  }

  public enum WpnType
  {
    MeleeL,
    Melee1H,
    Melee2H,
    RangedL,
    Ranged1H,
    Ranged2H,
  }
}
