using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class TrackedEvent
  {
    public int TrackedEventId { get; set; }
    public string DateCreated { get; set; }
    public string DateOccurring { get; set; }
    public string DateLastOccurred { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string Notes { get; set; }
    public int ReoccurFreq { get; set; }
    public int TrackedEventType { get; set; }
    public string TrackedEventData { get; set; }
  }

  public enum TrackedEventType
  {
    CalendarEvent = 1,
    UpdateCampaignNumber = 2 // Data Format: "Key(string);Operation(char);Amount(decimal)" - ex: "DaysUntilFestival;-;1", "GoblinBankAccount;*;1.0025"
  }
}
