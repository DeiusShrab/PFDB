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
    public string Notes { get; set; }
    public int ReoccurFreq { get; set; }
  }
}
