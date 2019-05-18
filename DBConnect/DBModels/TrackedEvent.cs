using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class TrackedEvent
  {
    public int TrackedEventId { get; set; }
    public string DateOccurring { get; set; }
    public string DateLastOccurred { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Notes { get; set; }
    public int ReoccurFreq { get; set; } // Number of (TrackedEventFreq) between occurrences
    public int TrackedEventType { get; set; }
    public int TrackedEventFreq { get; set; }
    public string TrackedEventData { get; set; }
    public int CampaignId { get; set; }
    public int? ContinentId { get; set; } // NULL for globally recognized events, otherwise the ID of the continent that observes the event

    public virtual Campaign Campaign { get; set; }
    public virtual Continent Continent { get; set; }
  }
}
