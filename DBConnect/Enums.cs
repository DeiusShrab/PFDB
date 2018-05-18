using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect
{
  public enum GearType
  {
    Misc = 1,
    Consumable = 2,
    WeaponMelee = 3,
    WeaponRanged = 4,
    Shield = 5,
    Head = 6,
    Headband = 7,
    Eyes = 8,
    Shoulders = 9,
    Neck = 10,
    Chest = 11,
    Body = 12,
    Armor = 13,
    Belt = 14,
    Wrists = 15,
    Hands = 16,
    Ring = 17,
    Feet = 18,
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
    MeleeL = 1,
    Melee1H = 2,
    Melee2H = 3,
    RangedL = 4,
    Ranged1H = 5,
    Ranged2H = 6,
  }

  public enum Stat
  {
    STR = 1,
    DEX = 2,
    CON = 3,
    INT = 4,
    WIS = 5,
    CHA = 6,
    Level = 7,
    BAB = 8,
  }

  public enum ClassAbilityType
  {
    BAB = 1,
    Fort = 2,
    Ref = 3,
    Will = 4,
    SpellsPerDay = 5,
    SpellsKnown = 6,
    Sq = 7,
    Su = 8,
    Ex = 9,
    Other = 10,
    Familiar = 11,
  }

  public enum PrereqType
  {
    Task = 1,
    Feat = 2,
    Skill = 3,
  }
}
