using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  public class LiveEvent
  {
    private int EventId;
    private string Name;
    private string Notes;
    private int ReoccurFreq;

    private FantasyDate DateCreated;
    private FantasyDate DateNextOccurring;
    private FantasyDate DateLastOccurred;

    public LiveEvent(TrackedEvent evt)
    {
      DateCreated = new FantasyDate(evt.DateCreated);
      DateNextOccurring = new FantasyDate(evt.DateOccurring);
      DateLastOccurred = string.IsNullOrWhiteSpace(evt.DateLastOccurred) ? null : new FantasyDate(evt.DateLastOccurred);

      Name = evt.Name;
      Notes = evt.Notes;
      ReoccurFreq = evt.ReoccurFreq;
      EventId = evt.TrackedEventId;
    }

    public TrackedEvent Export()
    {
      var evt = new TrackedEvent
      {
        DateCreated = DateCreated.ToNumDate(),
        DateOccurring = DateNextOccurring.ToNumDate(),
        Name = Name,
        Notes = Notes,
        ReoccurFreq = ReoccurFreq,
        TrackedEventId = EventId
      };

      if (DateLastOccurred != null)
        evt.DateLastOccurred = DateLastOccurred.ToNumDate();

      return evt;
    }

    public int DaysSince(FantasyDate evt)
    {
      return DateLastOccurred.DaysSince(evt);
    }

    public int DaysUntil(FantasyDate evt)
    {
      return DateNextOccurring.DaysSince(evt);
    }
  }
}
