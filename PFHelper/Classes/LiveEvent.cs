using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  public class LiveEvent
  {
    public int EventId;
    public string Name;
    public string Notes;
    public int ReoccurFreq;
    public string Location;
    public int CampaignId;
    public string Data;
    public int ContinentId;

    public TrackedEventType EventType;
    public FantasyDate DateNextOccurring;
    public FantasyDate DateLastOccurred;

    public LiveEvent()
    {
      
    }

    public LiveEvent(TrackedEvent evt)
    {
      DateNextOccurring = new FantasyDate(evt.DateOccurring);
      DateLastOccurred = string.IsNullOrWhiteSpace(evt.DateLastOccurred) ? null : new FantasyDate(evt.DateLastOccurred);

      Name = evt.Name;
      Notes = evt.Notes;
      ReoccurFreq = evt.ReoccurFreq;
      EventId = evt.TrackedEventId;
      Location = evt.Location;
      CampaignId = evt.CampaignId;
      EventType = (TrackedEventType)evt.TrackedEventType;
      Data = evt.TrackedEventData;
    }

    public TrackedEvent Export()
    {
      var evt = new TrackedEvent
      {
        DateOccurring = DateNextOccurring.ToNumDate(),
        Name = Name,
        Notes = Notes,
        ReoccurFreq = ReoccurFreq,
        TrackedEventId = EventId,
        Location = Location,
        CampaignId = CampaignId,
        TrackedEventData = Data,
        TrackedEventType = (int)EventType,
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
