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
    UpdateCampaignNumber = 2, // Data Format: "Key(string);Operation(char);Amount(decimal)" - ex: "DaysUntilFestival;-;1", "GoblinBankAccount;*;1.0025"
  }

  public enum TrackedEventFrequency
  {
    OneTime = 0,
    Days = 1,
    Weeks = 2,
    Months = 3,
    Years = 4,
  }

  public enum DmgType
  {
    Untyped = 0,
    B = 1,
    S = 2,
    P = 3,
    Fire = 4,
    Cold = 5,
    Acid = 6,
    Electricity = 7,
    Sonic = 8,
    Necrotic = 9, // Negative Energy
    Radiant = 10, // Positive Energy
    Force = 11,
    Ability = 12,
    Nonlethal = 13,
    Precision = 14, // Sneak Attack
    Poison = 15,
    Death = 16,
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
    None = 0,
    STR = 1,
    DEX = 2,
    CON = 3,
    INT = 4,
    WIS = 5,
    CHA = 6,
    Level = 7,
    BAB = 8,
    CMB = 9,
    CMD = 10,
  }

  public enum ClassAbilityType
  {
    SELECT = 0,
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
    Proficiency = 12,
  }

  public enum PrereqType
  {
    Other = 0,
    Task = 1,
    Feat = 2,
    Skill = 3,
    Class = 4,
    Stat = 5,
  }

  public enum CRSpecial
  {
    ALL = -999,
    Half = 0,
    Third = -1,
    Fourth = -2,
    Sixth = -3,
    Eighth = -4,
  }

  public enum SaveDataType
  {
    PFHelper = 1,
    PFEditor = 2,
    PFSite = 3,
    PFAPI = 4,
    PFHelperCombatGrid = 5,
  }

  public enum ClassType
  {
    Core = 1,
    Base = 2,
    Hybrid = 3,
    Occult = 4,
    Alternate = 5,
    Unchained = 6,
    Prestige = 7,
    NPC = 8,
    ThirdParty = 9,
    Custom = 10,
  }

  public enum PermissionDataType
  {
    Int = 1,
    String = 2,
  }

  public enum CampaignDataGenType
  {
    Formula = 1,
    PFHelper = 2,
  }
}
