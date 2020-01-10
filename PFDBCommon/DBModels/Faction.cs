using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Faction
  {
    public int FactionId { get; set; }
    public string Name { get; set; }
    public int? ParentFactionId { get; set; }
    public string Type { get; set; }
    public int? PrimaryLanguageId { get; set; }
    public int? PrimaryRaceId { get; set; }

    public virtual Language PrimaryLanguage { get; set; }
    public virtual Race PrimaryRace { get; set; }
    public virtual ICollection<Location> Locations { get; } = new List<Location>();
    public virtual ICollection<Territory> Territories { get; } = new List<Territory>();
  }
}
