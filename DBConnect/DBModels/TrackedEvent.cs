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

    public virtual int DaysLeft(string dateCurrent)
    {
      if (Convert.ToInt64(dateCurrent) < Convert.ToInt64(DateOccurring))
        return Convert.ToInt32(Convert.ToInt64(DateOccurring) - Convert.ToInt64(dateCurrent));

      return 0;
    }
  }
}
