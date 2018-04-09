using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public partial class TrackedEvent
  {
    public int TrackedEventId { get; set; }
    public ulong DateCreated { get; set; }
    public ulong DateOccurring { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }

    public virtual ulong DaysLeft(ulong dateCurrent)
    {
      if (dateCurrent < DateOccurring)
        return DateOccurring - dateCurrent;

      return 0;
    }
  }
}
