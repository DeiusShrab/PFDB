using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Weather
  {
    public int WeatherId { get; set; }
    public string Description { get; set; }
    public bool HeatDanger { get; set; }
    public bool HeatLethal { get; set; }
    public bool ColdDanger { get; set; }
    public bool ColdLethal { get; set; }
    public bool HighWind { get; set; }
    public bool VisionObscured { get; set; }
    public string Effects { get; set; }
    public string Name { get; set; }
    public bool Magical { get; set; }
    public bool Deadly { get; set; }
    public bool Flooding { get; set; }

    public virtual ICollection<ContinentWeather> ContinentWeathers { get; } = new List<ContinentWeather>();
  }
}
