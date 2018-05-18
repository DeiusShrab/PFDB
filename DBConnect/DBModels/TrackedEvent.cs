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
    public int ReoccurFreq { get; set; } // Days between reoccurrence, or 0 for one-time events
    public int TrackedEventType { get; set; }
    public string TrackedEventData { get; set; }
    public int CampaignId { get; set; }

    public virtual Campaign Campaign { get; set; }
  }
}
