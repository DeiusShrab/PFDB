using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Attack
  {
    public int AtkBonus { get; set; }
    public int CritMin { get; set; }
    public int CritMult { get; set; }
    public string Damage { get; set; }
    public string Notes { get; set; }
  }
}
