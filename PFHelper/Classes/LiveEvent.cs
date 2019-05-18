using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  public class LiveEvent : INotifyPropertyChanged
  {
    private int _eventId;
    private string _name;
    private string _notes;
    private int _reoccurFreq;
    private string _location;
    private int _campaignId;
    private string _data;
    private int _continentId;
    private bool _activeEvent;

    private TrackedEventType _eventType;
    private TrackedEventFrequency _eventFrequency;
    private FantasyDate _dateNextOccurring;
    private FantasyDate _dateLastOccurred;

    public int EventId
    {
      get { return _eventId; }
      set
      {
        if (_eventId != value)
        {
          _eventId = value;
          NotifyPropertyChanged();
        }
      }
    }
    public string Name
    {
      get { return _name; }
      set
      {
        if (_name != value)
        {
          _name = value;
          NotifyPropertyChanged();
        }
      }
    }
    public string Notes
    {
      get { return _notes; }
      set
      {
        if (_notes != value)
        {
          _notes = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int ReoccurFreq
    {
      get { return _reoccurFreq; }
      set
      {
        if (_reoccurFreq != value)
        {
          _reoccurFreq = value;
          NotifyPropertyChanged();
        }
      }
    }
    public string Location
    {
      get { return _location; }
      set
      {
        if (_location != value)
        {
          _location = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int CampaignId
    {
      get { return _campaignId; }
      set
      {
        if (_campaignId != value)
        {
          _campaignId = value;
          NotifyPropertyChanged();
        }
      }
    }
    public string Data
    {
      get { return _data; }
      set
      {
        if (_data != value)
        {
          _data = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int ContinentId
    {
      get { return _continentId; }
      set
      {
        if (_continentId != value)
        {
          _continentId = value;
          NotifyPropertyChanged();
        }
      }
    }
    public bool ActiveEvent
    {
      get { return _activeEvent; }
      set
      {
        if (_activeEvent != value)
        {
          _activeEvent = value;
          NotifyPropertyChanged();
        }
      }
    }

    public TrackedEventType EventType
    {
      get { return _eventType; }
      set
      {
        if (_eventType != value)
        {
          _eventType = value;
          NotifyPropertyChanged();
        }
      }
    }
    public TrackedEventFrequency EventFrequency
    {
      get { return _eventFrequency; }
      set
      {
        if (_eventFrequency != value)
        {
          _eventFrequency = value;
          NotifyPropertyChanged();
        }
      }
    }
    public FantasyDate DateNextOccurring
    {
      get { return _dateNextOccurring; }
      set
      {
        if (_dateNextOccurring != value)
        {
          _dateNextOccurring = value;
          NotifyPropertyChanged();
        }
      }
    }
    public FantasyDate DateLastOccurred
    {
      get { return _dateLastOccurred; }
      set
      {
        if (_dateLastOccurred != value)
        {
          _dateLastOccurred = value;
          NotifyPropertyChanged();
        }
      }
    }

    public string ShortDateNextOccurring => DateNextOccurring?.ShortDate;
    public string ShortDateLastOccurred => DateLastOccurred?.ShortDate;

    public LiveEvent()
    {
      
    }

    public LiveEvent(TrackedEvent evt)
    {
      _dateNextOccurring = new FantasyDate(evt.DateOccurring);
      _dateLastOccurred = string.IsNullOrWhiteSpace(evt.DateLastOccurred) ? null : new FantasyDate(evt.DateLastOccurred);

      _name = evt.Name;
      _notes = evt.Notes;
      _reoccurFreq = evt.ReoccurFreq;
      _eventId = evt.TrackedEventId;
      _location = evt.Location;
      _campaignId = evt.CampaignId;
      _eventType = (TrackedEventType)evt.TrackedEventType;
      _data = evt.TrackedEventData;
      _activeEvent = false;
      _continentId = evt.ContinentId ?? 0;
      _eventFrequency = (TrackedEventFrequency)evt.TrackedEventFreq;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public TrackedEvent Export()
    {
      var evt = new TrackedEvent
      {
        DateOccurring = _dateNextOccurring.ToNumDate(),
        Name = _name,
        Notes = _notes,
        ReoccurFreq = _reoccurFreq,
        TrackedEventId = _eventId,
        Location = _location,
        CampaignId = _campaignId,
        TrackedEventData = _data,
        TrackedEventType = (int)_eventType,
        ContinentId = _continentId > 0 ? (int?)_continentId : null,
        TrackedEventFreq = (int)_eventFrequency,
        DateLastOccurred = _dateLastOccurred?.ToNumDate()
      };

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

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
