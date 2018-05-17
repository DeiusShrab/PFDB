using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Environment
  {
    public int EnvironmentId { get; set; }
    public string Name { get; set; }
    public string Temperature { get; set; }
    public string Notes { get; set; }
    public int TravelSpeed { get; set; }

    public virtual ICollection<BestiaryEnvironment> BestiaryEnvironments { get; } = new List<BestiaryEnvironment>();
    public virtual ICollection<Location> Locations { get; } = new List<Location>();
  }
}
