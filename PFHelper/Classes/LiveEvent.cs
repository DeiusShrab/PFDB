using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  public class LiveEvent
  {
    public int EventId { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }
    public int ReoccurFreq { get; set; }
    public string Location { get; set; }
    public int CampaignId { get; set; }
    public string Data { get; set; }
    public int ContinentId { get; set; }

    public TrackedEventType EventType { get; set; }
    public TrackedEventFrequency EventFrequency { get; set; }
    public FantasyDate DateNextOccurring { get; set; }
    public FantasyDate DateLastOccurred { get; set; }

    public string ShortDateNextOccurring => DateNextOccurring?.ShortDate;
    public string ShortDateLastOccurred => DateLastOccurred?.ShortDate;

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
