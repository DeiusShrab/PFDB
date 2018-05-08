using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Gear
  {
    public int GearId { get; set; }
    public decimal Cost { get; set; } // Cost in GP
    public string Name { get; set; }
    public int Type { get; set; }
  }

  public enum GearType
  {
    Misc,
    Consum, // Consumables
    WpnMLgt,
    WpnM1H,
    WpnM2H,
    WpnRLgt,
    WpnR1H,
    WpnR2H,
    ArmLgt,
    ArmMed,
    ArmHvy,
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
}
