using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public partial class TrackedEvent
  {
    public int TrackedEventId { get; set; }
    public string DateCreated { get; set; }
    public string DateOccurring { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
  }
}
