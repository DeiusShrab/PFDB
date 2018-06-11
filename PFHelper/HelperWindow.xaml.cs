using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using CommonUI;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using PFHelper.Classes;

namespace PFHelper
{
  /// <summary>
  /// Interaction logic for HelperWindow.xaml
  /// </summary>
  public partial class HelperWindow : Window
  {
    #region Interface Properties

    public bool EncounterGroup
    {
      get { return CbxEncounterGroup.IsChecked == true; }
      set { CbxEncounterGroup.IsChecked = value; }
    }

    public bool EncounterNPC
    {
      get { return CbxEncounterNPC.IsChecked == true; }
      set { CbxEncounterNPC.IsChecked = value; }
    }

    public bool EncounterTime
    {
      get { return CbxEncounterTime.IsChecked == true; }
      set { CbxEncounterTime.IsChecked = value; }
    }

    public bool EncounterZone
    {
      get { return CbxEncounterZone.IsChecked == true; }
      set { CbxEncounterZone.IsChecked = value; }
    }

    public bool RationsInfinite
    {
      get { return CbxRationsInfinite.IsChecked == true; }
      set { CbxRationsInfinite.IsChecked = value; }
    }

    public int ContinentId
    {
      get { return CurrentContinent?.ContinentId ?? 0; }
    }

    public int TimeId
    {
      get
      {
        if (LbxTime.SelectedItem != null)
          return (int)LbxTime.SelectedValue;
        return 0;
      }
      set
      {
        if (timeList.Select(x => x.Result).Contains(value))
          LbxTime.SelectedValue = value;
        else
          LbxTime.SelectedValue = null;
      }
    }

    public int PlaneId
    {
      get
      {
        if (LbxPlane.SelectedItem != null)
          return (int)LbxPlane.SelectedValue;
        return 0;
      }
      set
      {
        if (planeList.Select(x => x.Result).Contains(value))
          LbxPlane.SelectedValue = value;
        else
          LbxPlane.SelectedValue = null;
      }
    }

    public int EnvironmentId
    {
      get
      {
        if (LbxEnvironment.SelectedItem != null)
          return (int)LbxEnvironment.SelectedValue;
        return 0;
      }
      set
      {
        if (environmentList.Select(x => x.Result).Contains(value))
          LbxEnvironment.SelectedValue = value;
        else
          LbxEnvironment.SelectedValue = null;
      }
    }

    public int CombatRound
    {
      get { return _combatRound; }
      set { LblCombatRound.Content = (_combatRound = value).ToString("D2"); }
    }
    private int _combatRound;

    public int RationsLeft
    {
      get { return _rationsLeft; }
      set
      {
        _rationsLeft = value;
        LblRationsLeft.Content = _rationsLeft.ToString();
      }
    }
    private int _rationsLeft;

    private int CgiInit
    {
      get
      {
        int.TryParse(TxtCombatInit.Text, out int ret);
        return ret;
      }
      set { TxtCombatInit.Text = value.ToString(); }
    }

    private string CgiName
    {
      get { return TxtCombatName.Text; }
      set { TxtCombatName.Text = value; }
    }

    private bool CgiPC
    {
      get { return CbxCombatPC.IsChecked == true; }
      set { CbxCombatPC.IsChecked = value; }
    }

    private int CgiHP
    {
      get
      {
        int.TryParse(TxtCombatHP.Text, out int ret);
        return ret;
      }
      set { TxtCombatHP.Text = value.ToString(); }
    }

    private int CgiAC
    {
      get
      {
        int.TryParse(TxtCombatAC.Text, out int ret);
        return ret;
      }
      set { TxtCombatAC.Text = value.ToString(); }
    }

    private int CgiFlat
    {
      get
      {
        int.TryParse(TxtCombatFlat.Text, out int ret);
        return ret;
      }
      set { TxtCombatFlat.Text = value.ToString(); }
    }

    private int CgiTouch
    {
      get
      {
        int.TryParse(TxtCombatTouch.Text, out int ret);
        return ret;
      }
      set { TxtCombatTouch.Text = value.ToString(); }
    }

    private int CgiFort
    {
      get
      {
        int.TryParse(TxtCombatFort.Text, out int ret);
        return ret;
      }
      set { TxtCombatFort.Text = value.ToString(); }
    }

    private int CgiRef
    {
      get
      {
        int.TryParse(TxtCombatRef.Text, out int ret);
        return ret;
      }
      set { TxtCombatRef.Text = value.ToString(); }
    }

    private int CgiWill
    {
      get
      {
        int.TryParse(TxtCombatWill.Text, out int ret);
        return ret;
      }
      set { TxtCombatWill.Text = value.ToString(); }
    }

    private int CefRounds
    {
      get { return IntCombatEffectRounds.Value ?? 0; }
      set { IntCombatEffectRounds.Value = value; }
    }

    private string CefName
    {
      get { return TxtCombatEffect.Text; }
      set { TxtCombatEffect.Text = value; }
    }

    private int RationsAdjust
    {
      get
      {
        int ret = 1;
        int.TryParse(TxtRations.Text, out ret);
        return ret;
      }
    }

    private int DaysAdjust
    {
      get { return IntAddDays.Value ?? 0; }
    }

    private string MoonPhase
    {
      set { LblMoonPhase.Content = value; }
    }

    public ObservableCollection<CombatGridItem> CombatGridItems
    {
      get => combatGridItems;
    }

    public ObservableCollection<CombatEffectItem> CombatEffectItems
    {
      get => combatEffectItems;
    }

    public ObservableCollection<LiveEvent> LiveEvents
    {
      get => liveEventList;
    }

    public string EventName
    {
      get { return TxtEvtName.Text; }
      set { TxtEvtName.Text = value; }
    }

    public string EventDate
    {
      get { return TxtEvtDate.Text; }
      set { TxtEvtDate.Text = value; }
    }

    public string EventLastDate
    {
      get { return TxtEvtLastDate.Text; }
      set { TxtEvtLastDate.Text = value; }
    }

    public string EventLocation
    {
      get { return TxtEvtLocation.Text; }
      set { TxtEvtLocation.Text = value; }
    }

    public int EventFrequency
    {
      get { return IntEvtFreq.Value ?? 0; }
      set
      {
        if (value > 0)
          IntEvtFreq.Value = value;
        else
          IntEvtFreq.Value = null;
      }
    }

    public int EventTypeId
    {
      get
      {
        if (DrpEvtType.SelectedItem != null)
          return (int)DrpEvtType.SelectedValue;

        return 0;
      }
      set
      {
        DrpEvtType.SelectedValue = value;
      }
    }

    public string EventData
    {
      get { return TxtEvtData.Text; }
      set { TxtEvtData.Text = value; }
    }

    public string EventNotes
    {
      get { return TxtEvtNotes.Text; }
      set { TxtEvtNotes.Text = value; }
    }

    public int CampaignId
    {
      get { return Convert.ToInt32(LblCampaignId.Content.ToString()); }
      set { LblCampaignId.Content = value; }
    }

    public string CampaignName
    {
      get { return TxtCampaignName.Text; }
      set { TxtCampaignName.Text = value; }
    }

    public string CampaignNotes
    {
      get { return TxtCampaignNotes.Text; }
      set { TxtCampaignNotes.Text = value; }
    }

    public string CampaignDataName
    {
      get { return TxtCampaignDataName.Text; }
      set { TxtCampaignDataName.Text = value; }
    }

    public string CampaignDataValue
    {
      get { return TxtCampaignDataValue.Text; }
      set { TxtCampaignDataValue.Text = value; }
    }

    public string CampaignDataNew
    {
      get { return TxtCampaignDataNew.Text; }
      set { TxtCampaignDataNew.Text = value; }
    }

    public bool WeatherLock
    {
      get { return CbxWeatherLock.IsChecked ?? false; }
      set { CbxWeatherLock.IsChecked = value; }
    }

    public int EventContinentId
    {
      get
      {
        if (DrpEvtContinent.SelectedIndex >= 0)
          return (int)DrpEvtContinent.SelectedValue;

        return 0;
      }
      set { DrpEvtContinent.SelectedValue = value; }
    }

    #endregion

    #region Data Properties

    private ObservableCollection<CombatGridItem> combatGridItems;
    private ObservableCollection<CombatEffectItem> combatEffectItems;
    private ObservableCollection<LiveEvent> liveEventList;
    private ObservableCollection<DisplayResult> randomEncounterItems;
    private ObservableCollection<DisplayResult> continentList;
    private ObservableCollection<DisplayValues> encounterResults;
    private ObservableCollection<DisplayResult> creatureInfos;
    private ObservableCollection<DisplayResult> planeList;
    private ObservableCollection<DisplayResult> timeList;
    private ObservableCollection<DisplayResult> campaignList;
    private ObservableCollection<DisplayResult> environmentList;
    private ObservableCollection<string> campaignDataList;

    private List<TrackedEvent> trackedEvents;
    private List<Continent> continents;
    private List<Season> seasons;
    private List<Month> months;
    private List<Time> times;
    private List<Plane> planes;
    private List<Campaign> campaigns;
    private List<DBConnect.DBModels.Environment> environments;
    private Dictionary<string, string> CampaignData;

    private RandomWeatherResult WeatherResult;
    private Random random;
    private FantasyDate CurrentDate;
    private Month CurrentMonth;
    private Weather CurrentWeather;
    private LiveEvent CurrentEvent;
    private Continent CurrentContinent;
    private Plane CurrentPlane;
    private Time CurrentTime;
    private Terrain CurrentTerrain;
    private ContinentWeather CurrentWeatherGroup;
    private Campaign ActiveCampaign;
    private string saveDataPath = Path.Combine(System.Environment.CurrentDirectory, "pfdat.dat");
    private string selectedCombatMonsterHtml;
    private int selectedCombatMonsterId;
    private string[] DayNames = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

    #endregion

    #region Constructor

    public HelperWindow()
    {
      InitializeComponent();
      this.DataContext = this;

      random = new Random();
      CurrentDate = new FantasyDate();

      encounterResults = new ObservableCollection<DisplayValues>();
      randomEncounterItems = new ObservableCollection<DisplayResult>();
      combatEffectItems = new ObservableCollection<CombatEffectItem>();
      combatGridItems = new ObservableCollection<CombatGridItem>();
      continentList = new ObservableCollection<DisplayResult>();
      creatureInfos = new ObservableCollection<DisplayResult>();
      planeList = new ObservableCollection<DisplayResult>();
      timeList = new ObservableCollection<DisplayResult>();
      liveEventList = new ObservableCollection<LiveEvent>();
      campaignDataList = new ObservableCollection<string>();
      campaignList = new ObservableCollection<DisplayResult>();
      environmentList = new ObservableCollection<DisplayResult>();

      if (!LoadDBData())
      {
        MessageBox.Show("ERROR - Failed to connect to server. Is the config set correctly?");
      }

      LbxD20.DisplayMemberPath = LbxD4.DisplayMemberPath = LbxD6.DisplayMemberPath = LbxD8.DisplayMemberPath = LbxD10.DisplayMemberPath = LbxD12.DisplayMemberPath
        = LbxEncounterCreatures.DisplayMemberPath = LbxContinent.DisplayMemberPath = LbxCreatureInfo.DisplayMemberPath = LbxPlane.DisplayMemberPath
        = LbxTime.DisplayMemberPath = LbxEnvironment.DisplayMemberPath = DrpEvtType.DisplayMemberPath = DrpCampaignSelect.DisplayMemberPath
        = DrpEvtContinent.DisplayMemberPath = "Display";
      LbxD20.SelectedValuePath = LbxD4.SelectedValuePath = LbxD6.SelectedValuePath = LbxD8.SelectedValuePath = LbxD10.SelectedValuePath = LbxD12.SelectedValuePath
        = LbxEncounterCreatures.SelectedValuePath = LbxContinent.SelectedValuePath = LbxCreatureInfo.SelectedValuePath = LbxPlane.SelectedValuePath
        = LbxTime.SelectedValuePath = LbxEnvironment.SelectedValuePath = DrpEvtType.SelectedValuePath = DrpCampaignSelect.SelectedValuePath
        = DrpEvtContinent.SelectedValuePath = "Result";

      LbxEncounterCRs.DisplayMemberPath = "Display";
      LbxEncounterCRs.SelectedValuePath = "Values";

      LbxEncounterCreatures.ItemsSource = randomEncounterItems;
      LbxEncounterCRs.ItemsSource = encounterResults;
      LbxContinent.ItemsSource = continentList;
      LbxCreatureInfo.ItemsSource = creatureInfos;
      LbxPlane.ItemsSource = planeList;
      LbxTime.ItemsSource = timeList;
      LbxCampaignData.ItemsSource = campaignDataList;
      DrpCampaignSelect.ItemsSource = campaignList;
      DrpEvtContinent.ItemsSource = continentList;
      
      LoadSavedData();
      LoadContinentEnvironments();

      UpdateDisplays();

      IntNumD20.Value = IntNumD12.Value = IntNumD10.Value = IntNumD8.Value = IntNumD6.Value = IntNumD4.Value = 1;

      foreach (TrackedEventType item in Enum.GetValues(typeof(TrackedEventType)))
      {
        DrpEvtType.Items.Add(new DisplayResult() { Display = item.ToString(), Result = (int)item });
      }
    }

    #endregion

    #region Methods

    private bool LoadDBData()
    {
      var ret = false;

      continentList.Clear();
      timeList.Clear();
      planeList.Clear();

      continentList.Add(new DisplayResult { Display = "ALL", Result = -1 });
      timeList.Add(new DisplayResult { Display = "ALL", Result = -1 });
      planeList.Add(new DisplayResult { Display = "ALL", Result = -1 });

      continents = new List<Continent>();
      seasons = new List<Season>();
      months = new List<Month>();
      times = new List<Time>();
      planes = new List<Plane>();
      trackedEvents = new List<TrackedEvent>();
      campaigns = new List<Campaign>();
      environments = new List<DBConnect.DBModels.Environment>();
      CampaignData = new Dictionary<string, string>();

      if (PFConfig.ConfigExists())
      {
        try
        {
          DBClient.ConnectToApi();

          continents.AddRange(DBClient.GetContinents());
          seasons.AddRange(DBClient.GetSeasons());
          months.AddRange(DBClient.GetMonths());
          times.AddRange(DBClient.GetTimes());
          planes.AddRange(DBClient.GetPlanes());
          trackedEvents.AddRange(DBClient.GetTrackedEvents());
          campaigns.AddRange(DBClient.GetCampaigns());

          ActiveCampaign = DBClient.GetCampaign(int.Parse(PFConfig.CAMPAIGN_ID));
          CampaignData = DBClient.GetCampaignData();

          continentList.Clear();
          timeList.Clear();
          planeList.Clear();
          liveEventList.Clear();
          campaignList.Clear();
          campaignDataList.Clear();

          foreach (var item in continents.OrderBy(x => x.Name))
          {
            continentList.Add(new DisplayResult() { Display = item.Name, Result = item.ContinentId });
          }
          foreach (var item in times.OrderBy(x => x.Name))
          {
            timeList.Add(new DisplayResult() { Display = item.Name, Result = item.TimeId });
          }
          foreach (var item in planes.OrderBy(x => x.Name))
          {
            planeList.Add(new DisplayResult() { Display = item.Name, Result = item.PlaneId });
          }
          foreach (var item in trackedEvents)
          {
            liveEventList.Add(new LiveEvent(item));
          }
          foreach (var item in campaigns.OrderBy(x => x.CampaignName))
          {
            campaignList.Add(new DisplayResult() { Display = item.CampaignName, Result = item.CampaignId });
          }

          BtnEvtSortNext_Click(null, null);

          campaignDataList.AddRange(CampaignData.Keys);

          ret = true;
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }

      return ret;
    }

    private void LoadSavedData()
    {
      if (File.Exists(saveDataPath))
      {
        var saveObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SaveObject>(File.ReadAllText(saveDataPath));

        TxtAPL.Text = saveObject.Apl.ToString();

        EncounterGroup = saveObject.CbxGroup;
        EncounterNPC = saveObject.CbxNpc;
        EncounterTime = saveObject.CbxTime;
        EncounterZone = saveObject.CbxZone;
        RationsInfinite = saveObject.CbxInfRations;
        TimeId = saveObject.TimeId;
        PlaneId = saveObject.PlaneId;
        EnvironmentId = saveObject.EnvironmentId;
        CombatRound = saveObject.CombatRound;
        WeatherLock = saveObject.WeatherLock;

        RationsLeft = saveObject.Rations;
        CurrentDate = saveObject.Date;
        CurrentWeather = saveObject.Weather;
        WeatherResult = saveObject.WeatherResult;
        CurrentContinent = saveObject.Continent;
        CurrentPlane = saveObject.Plane;
        CurrentTime = saveObject.Time;
        CurrentTerrain = saveObject.Terrain;

        combatEffectItems.Clear();
        combatGridItems.Clear();
        combatEffectItems.AddRange(saveObject.CombatEffects);
        combatGridItems.AddRange(saveObject.CombatGridItems);
      }
      else
      {
        TxtAPL.Text = "1";
        EncounterGroup = true;
        EncounterNPC = false;
        EncounterTime = false;
        EncounterZone = true;
        RationsInfinite = false;

        RationsLeft = 60;
        CurrentDate = new FantasyDate() { Year = 10000, Month = 1, Day = 1 };
        CurrentWeather = new Weather();
        CurrentContinent = null;
        CurrentPlane = null;
        CurrentTime = null;
        CurrentTerrain = null;

        combatEffectItems.Clear();
        combatGridItems.Clear();
      }
    }

    private void SaveDataToDisk()
    {
      var saveObject = new SaveObject();

      if (int.TryParse(TxtAPL.Text, out int apl))
        saveObject.Apl = apl;

      saveObject.CbxGroup = EncounterGroup;
      saveObject.CbxNpc = EncounterNPC;
      saveObject.CbxTime = EncounterTime;
      saveObject.CbxZone = EncounterZone;
      saveObject.CbxInfRations = RationsInfinite;
      saveObject.EnvironmentId = EnvironmentId;
      saveObject.PlaneId = PlaneId;
      saveObject.TimeId = TimeId;
      saveObject.CombatRound = CombatRound;
      saveObject.WeatherLock = WeatherLock;

      saveObject.Rations = RationsLeft;
      saveObject.Date = CurrentDate;
      saveObject.Weather = CurrentWeather;
      saveObject.WeatherResult = WeatherResult;
      saveObject.Continent = CurrentContinent;
      saveObject.Plane = CurrentPlane;
      saveObject.Time = CurrentTime;
      saveObject.Terrain = CurrentTerrain;

      saveObject.CombatEffects = combatEffectItems.ToList();
      saveObject.CombatGridItems = combatGridItems.ToList();

      File.WriteAllText(saveDataPath, Newtonsoft.Json.JsonConvert.SerializeObject(saveObject));
    }

    private void SaveDataToDB()
    {
      try
      {
        foreach (var item in liveEventList)
        {
          if (item.EventId > 0)
            DBClient.UpdateTrackedEvent(item.Export());
          else
            DBClient.CreateTrackedEvent(item.Export());
        }

        trackedEvents = DBClient.GetTrackedEvents();
        liveEventList.Clear();
        foreach (var item in trackedEvents)
        {
          liveEventList.Add(new LiveEvent(item));
        }
      }
      catch (Exception ex)
      {
        File.WriteAllText(Path.Combine(Path.GetDirectoryName(saveDataPath), $"Events {DateTime.Now.ToString("yyyyMMdd-hhmmss")}.log"), Newtonsoft.Json.JsonConvert.SerializeObject(liveEventList));
        MessageBox.Show("Failed to save Events!\n" + ex.Message);
      }

      try
      {
        DBClient.UpdateCampaignData(CampaignData);
      }
      catch (Exception ex)
      {
        File.WriteAllText(Path.Combine(Path.GetDirectoryName(saveDataPath), $"CampaignData {DateTime.Now.ToString("yyyyMMdd-hhmmss")}.log"), Newtonsoft.Json.JsonConvert.SerializeObject(CampaignData));
        MessageBox.Show("Failed to save CampaignData!\n" + ex.Message);
      }
    }

    private void ClearCombatAdd()
    {
      TxtCombatName.Clear();
      TxtCombatAC.Clear();
      TxtCombatFlat.Clear();
      TxtCombatTouch.Clear();
      TxtCombatFort.Clear();
      TxtCombatRef.Clear();
      TxtCombatWill.Clear();
      TxtCombatHP.Clear();
      TxtCombatInit.Clear();
      CgiPC = false;
    }

    private void ClearCombatEffectAdd()
    {
      TxtCombatEffect.Clear();
      IntCombatEffectRounds.Value = null;
    }

    private List<DisplayResult> DiceRoll(int d, int num, int add, bool addPos)
    {
      var ret = new List<DisplayResult>();
      string op = addPos ? "+" : "-";

      for (int i = 0; i < num; i++)
      {
        var roll = random.Next(d) + 1;
        var result = addPos ? (roll + add) : (roll - add);
        var itm = new DisplayResult
        {
          Display = $"{roll} {op} {add} = {result}",
          Result = result
        };

        ret.Add(itm);
      }

      return ret;
    }

    private void GenEncounters()
    {
      var difficulty = new string[] { "[E] ", "[M] ", "[H] " };
      encounterResults.Clear();
      GenMysterious();
      LblChanceEncounter.Content = random.Next(100).ToString("D2");
      LblChanceNPC.Content = random.Next(100).ToString("D2");
      if (!int.TryParse(TxtAPL.Text, out int apl))
        apl = 1;

      apl--;

      for (int t = 0; t < 3; t++)
      {
        var CRs = new List<int[]>();
        for (int i = 0; i < 5; i++)
        {
          int r = random.Next(6);
          switch (r)
          {
            case 0:
              CRs.Add(new int[] { apl });
              break;
            case 1:
              CRs.Add(new int[] { apl - 2, apl - 2 });
              break;
            case 2:
              CRs.Add(new int[] { apl - 4, apl - 4, apl - 4, apl - 4 });
              break;
            case 3:
              CRs.Add(new int[] { apl - 2, apl - 3, apl - 3 });
              break;
            case 4:
              CRs.Add(new int[] { apl - 2, apl - 4, apl - 4, apl - 4 });
              break;
            case 5:
              CRs.Add(new int[] { apl - 5, apl - 5, apl - 5, apl - 5, apl - 5, apl - 5 });
              break;
          }
        }
        foreach (var cr in CRs)
        {
          var sb = new StringBuilder();
          foreach (var c in cr)
          {
            sb.Append(ParseCR(c));
            sb.Append(", ");
          }
          sb.Remove(sb.Length - 2, 2);
          encounterResults.Add(new DisplayValues() { Display = difficulty[t] + sb.ToString(), Values = cr });
        }
        apl++;
      }
    }

    private void GenMysterious()
    {
      if (random.Next(100) == 0)
      {
        var m = random.Next(100);

        if (m == 1)
        {
          LblChanceMysterious.Foreground = Brushes.Blue;
          LblChanceMysterious.Content = "VGD";
        }
        else if (m == 0)
        {
          LblChanceMysterious.Foreground = Brushes.Red;
          LblChanceMysterious.Content = "VBD";
        }
        else if (m <= 10)
        {
          LblChanceMysterious.Foreground = Brushes.Green;
          LblChanceMysterious.Content = "GUD";
        }
        else if (m >= 90)
        {
          LblChanceMysterious.Foreground = Brushes.Orange;
          LblChanceMysterious.Content = "BAD";
        }
        else
        {
          LblChanceMysterious.Foreground = Brushes.Black;
          LblChanceMysterious.Content = "N/A";
        }

      }
      else
      {
        LblChanceMysterious.Foreground = Brushes.Black;
        LblChanceMysterious.Content = "N/A";
      }
    }

    private void LoadMonsters(int[] crs)
    {
      var req = new RandomEncounterRequest
      {
        Crs = crs,
        Group = EncounterGroup,
        Npc = EncounterNPC,
        ContinentId = EncounterZone ? ContinentId : 0,
        EnvironmentId = EncounterZone ? EnvironmentId : 0,
        TimeId = EncounterTime ? TimeId : 0
      };

      var result = DBClient.GetEncounters(req);
      if (result.Success)
      {
        foreach (var mon in result.EncounterItems)
        {
          var entry = new DisplayResult
          {
            Result = mon.BestiaryId,
            Display = mon.Name + " [" + ParseCR(mon.Cr) + "]"
          };

          randomEncounterItems.Add(entry);
        }
      }
      else
        MessageBox.Show("WARNING - Unable to connect to DB for LoadMonsters");
    }

    private string ParseCR(int cr)
    {
      switch (cr)
      {
        case (int)CRSpecial.Half:
          return "1/2";
        case (int)CRSpecial.Third:
          return "1/3";
        case (int)CRSpecial.Fourth:
          return "1/4";
        case (int)CRSpecial.Sixth:
          return "1/6";
        case (int)CRSpecial.Eighth:
          return "1/8";
        default:
          return cr.ToString();
      }
    }

    private void AddCreatureToCombat()
    {
      if (LbxEncounterCreatures.SelectedItems != null)
      {
        foreach (DisplayResult item in LbxEncounterCreatures.SelectedItems)
        {
          var b = DBClient.GetBestiary(item.Result);
          combatGridItems.Add(new CombatGridItem(b));
        }

        AddCreatureToCreatureInfo();
      }
    }

    private void AddCreatureToCreatureInfo()
    {
      if (LbxEncounterCreatures.SelectedItems != null)
      {
        foreach (DisplayResult item in LbxEncounterCreatures.SelectedItems)
        {
          if (!creatureInfos.Select(x => x.Result).Contains(item.Result))
            creatureInfos.Add(item);

          selectedCombatMonsterId = item.Result;
        }

        LoadCreatureInfo();
      }
    }

    private void LoadCreatureInfo()
    {
      var desc = DBClient.GetBestiaryDetail(selectedCombatMonsterId);
      if (desc != null)
        selectedCombatMonsterHtml = desc.FullText;
      else
        selectedCombatMonsterHtml = "<html><body><h1>NOT FOUND</h1></body></html>";

      BrowserCreature.NavigateToString(selectedCombatMonsterHtml);
    }

    private void CombatNextRound()
    {
      CombatRound++;

      var removeItems = new List<CombatEffectItem>();

      foreach (var item in combatEffectItems)
      {
        if (item.Rounds <= 0)
          removeItems.Add(item);
        else
          item.Rounds--;
      }

      foreach (var item in removeItems)
      {
        combatEffectItems.Remove(item);
      }
    }

    private void AddDays(int i)
    {
      BtnEvtSortNext_Click(null, null);

      var oldDate = new FantasyDate(CurrentDate.ShortDate);
      CurrentDate.AddDays(i);
      NextWeather(i);
      UpdateDateDisplay();

      if (!RationsInfinite && i > 0)
        AddRations(i * -1);

      var activeEventIds = new List<int>();

      for (int j = 1; j <= i; j++)
      {
        foreach (var evt in liveEventList)
        {
          if (evt.DateNextOccurring == oldDate.AddDays(j))
          {
            RunLiveEvent(evt);

            if (evt.ReoccurFreq > 1)
              activeEventIds.Add(evt.EventId);
          }
        }
      }

      if (activeEventIds.Count > 0)
        HelperTabs.SelectedIndex = 3;

      foreach (LiveEvent evt in DgEvt.ItemsSource)
      {
        var row = DgEvt.ItemContainerGenerator.ContainerFromItem(evt) as DataGridRow;
        if (activeEventIds.Contains(evt.EventId))
          row.Background = Brushes.Green;
        else
          row.Background = Brushes.White;
      }

      UpdateCampaignData("CurrentDate", CurrentDate.ToNumDate());
    }

    private void RunLiveEvent(LiveEvent e)
    {
      e.DateLastOccurred = new FantasyDate(e.DateNextOccurring.ShortDate);
      e.DateNextOccurring.AddDays(e.ReoccurFreq);

      try
      {
        switch (e.EventType)
        {
          case TrackedEventType.UpdateCampaignNumber:
            UpdateCampaignNumber(e.Data);
            break;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Failed to run event '{e.Name}'\n{ex.Message}");
      }
    }

    private void UpdateCampaignNumber(string data)
    {
      var split = data.Split(';');
      var key = split[0];
      var op = split[1];
      var num = Convert.ToDecimal(split[2]);
      var value = Convert.ToInt32(CampaignData[key]);
      switch (op)
      {
        case "*":
          value = Convert.ToInt32(Math.Floor(d: value * num));
          break;
        case "+":
          value += Convert.ToInt32(num);
          break;
        case "-":
          value -= Convert.ToInt32(num);
          break;
      }
      UpdateCampaignData(key, value.ToString());
    }

    private void NextWeather(int d = 1)
    {
      if (WeatherLock)
        return;

      if (WeatherResult == null && CurrentWeather == null)// DEBUG
      {
        LblCurrentWeather.Content = random.Next(100);
        LblCurrentWeatherGroup.Content = "DEBUG";
        return;
      }

      if (WeatherResult == null || ContinentId != WeatherResult.ContinentId)
      {
        ReloadWeatherTable();
        CurrentWeatherGroup = GetRandomWeather();
      }
      else if (CurrentMonth.SeasonId != WeatherResult.SeasonId)
        ReloadWeatherTable();

      while (d > 0 && CurrentWeatherGroup.Duration > 0)
      {
        if (CurrentWeatherGroup.Duration >= d)
        {
          CurrentWeatherGroup.Duration -= d;
          d = -1;
        }
        else
        {
          d -= CurrentWeatherGroup.Duration;
          if (CurrentWeatherGroup.NextCWID > 0)
          {
            if (WeatherResult.WeatherList.Select(x => x.WeatherId).Contains(CurrentWeatherGroup.NextCWID.Value))
              CurrentWeatherGroup = WeatherResult.WeatherList.First(x => x.WeatherId == CurrentWeatherGroup.NextCWID.Value);
            else
              CurrentWeatherGroup = DBClient.GetContinentWeather(CurrentWeatherGroup.NextCWID.Value);
          }
          else
            CurrentWeatherGroup = GetRandomWeather();
        }
      }

      CurrentWeather = DBClient.GetWeather(CurrentWeatherGroup.WeatherId);
    }

    private ContinentWeather GetRandomWeather(bool onlyParentWeather = true)
    {
      ContinentWeather weather = null;
      var initialWeathers = WeatherResult.WeatherList.Where(x => x.ParentCWID == 0 || !onlyParentWeather);
      if (initialWeathers.Count() == 0)
      {
        weather = new ContinentWeather() { CWName = "ERR" };
      }
      else
      {
        var totalWeight = initialWeathers.Sum(x => x.Weight);
        var randW = random.Next(totalWeight);
        foreach (var w in initialWeathers)
        {
          if (randW < w.Weight)
          {
            weather = w;
            break;
          }
        }
        if (weather == null)
          weather = initialWeathers.ElementAt(random.Next(initialWeathers.Count()));

        if (!onlyParentWeather)
          weather.Duration = weather.Duration - random.Next(weather.Duration);
      }

      return weather;
    }

    private void AddRations(int r)
    {
      RationsLeft += r;
      if (RationsLeft <= 0)
      {
        RationsLeft = 0;
        MessageBox.Show("Out of rations!");
      }
    }

    private void UpdateDateDisplay()
    {
      if (CurrentDate == null || seasons.Count == 0 || months.Count == 0)
      {
        LblGrandDate.Content = "NO DATA";
        return;
      }

      CurrentMonth = months.FirstOrDefault(x => x.MonthOrder == CurrentDate.Month);
      LblGrandDate.Content = $"YEAR {CurrentDate.Year} AA, Season of {seasons.FirstOrDefault(x => x.SeasonId == CurrentMonth.SeasonId).Name}, Month of {CurrentMonth.Name}, Day {CurrentDate.Day}";

      LblNumericDate.Content = $"{CurrentDate.Year} / {CurrentMonth.MonthOrder} / {CurrentDate.Day}";

      LblTextDate.Content = $"{DayNames[(CurrentDate.Day - 1) % 7]}, {CurrentMonth.Name.Substring(0, 3)} {CurrentDate.Day}";

      if (CurrentDate.Day <= 3 && CurrentDate.Day > 0)
        MoonPhase = "FULL MOON";
      else if (CurrentDate.Day <= 7)
        MoonPhase = "Waning Gibbous";
      else if (CurrentDate.Day <= 10)
        MoonPhase = "Last Quarter";
      else if (CurrentDate.Day <= 14)
        MoonPhase = "Waning Crescent";
      else if (CurrentDate.Day <= 17)
        MoonPhase = "NEW MOON";
      else if (CurrentDate.Day <= 21)
        MoonPhase = "Waxing Crescent";
      else if (CurrentDate.Day <= 24)
        MoonPhase = "First Quarter";
      else if (CurrentDate.Day <= 28)
        MoonPhase = "Waxing Gibbous";
      else
        MoonPhase = "MOON MOON";
    }

    private void UpdateWeatherDisplay()
    {
      LblCurrentWeather.Content = CurrentWeather == null ? "NONE" : CurrentWeather.Name;
      LblCurrentWeatherGroup.Content = CurrentWeatherGroup == null ? "NONE" : CurrentWeatherGroup.CWName;
    }

    private void UpdateLocationDisplay()
    {
      LblCurrentContinent.Content = CurrentContinent?.Name ?? "NONE";
      LblCurrentPlane.Content = CurrentPlane?.Name ?? "NONE";
      LblCurrentTime.Content = CurrentTime?.Name ?? "NONE";
      LblCurrentTerrain.Content = CurrentTerrain?.Name ?? "NONE";
    }

    private void UpdateDisplays()
    {
      UpdateDateDisplay();
      UpdateWeatherDisplay();
      UpdateLocationDisplay();
    }

    private void ReloadWeatherTable()
    {
      var reqWeather = new RandomWeatherRequest()
      {
        ContinentId = ContinentId,
        SeasonId = CurrentMonth.SeasonId ?? 0
      };

      WeatherResult = DBClient.GetRandomWeatherList(reqWeather);
    }

    private void LoadTrackedEvent()
    {
      if (CurrentEvent != null)
      {
        EventDate = CurrentEvent.DateNextOccurring.ShortDate;
        EventLastDate = CurrentEvent.DateLastOccurred != null ? CurrentEvent.DateLastOccurred.ShortDate : string.Empty;
        EventLocation = CurrentEvent.Location;
        EventName = CurrentEvent.Name;
        EventNotes = CurrentEvent.Notes;
        EventFrequency = CurrentEvent.ReoccurFreq;
        EventContinentId = CurrentEvent.ContinentId;
      }
    }

    private void SaveTrackedEvent()
    {
      if (CurrentEvent == null)
        CurrentEvent = new LiveEvent();

      CurrentEvent.DateNextOccurring = new FantasyDate(EventDate);
      CurrentEvent.DateLastOccurred = string.IsNullOrWhiteSpace(EventLastDate) ? null : new FantasyDate(EventLastDate);
      CurrentEvent.Location = EventLocation;
      CurrentEvent.Name = EventName;
      CurrentEvent.Notes = EventNotes;
      CurrentEvent.ReoccurFreq = EventFrequency;
      CurrentEvent.ContinentId = EventContinentId;
    }

    private void LoadCampaign()
    {
      if (ActiveCampaign == null)
        ActiveCampaign = new Campaign();

      CampaignId = ActiveCampaign.CampaignId;
      CampaignName = ActiveCampaign.CampaignName;
      CampaignNotes = ActiveCampaign.CampaignNotes;
    }

    private void SaveCampaign()
    {
      if (ActiveCampaign == null)
        ActiveCampaign = new Campaign();

      ActiveCampaign.CampaignId = CampaignId;
      ActiveCampaign.CampaignName = CampaignName;
      ActiveCampaign.CampaignNotes = CampaignNotes;
    }

    private void SwitchContinents(int continentId)
    {
      CurrentContinent = DBClient.GetContinent(continentId);
      UpdateLocationDisplay();
    }

    private void LoadContinentEnvironments()
    {
      environmentList.Clear();

      if (ContinentId > 0)
      {
        environments.Clear();
        environments.AddRange(DBClient.GetEnvironmentsForContinent(ContinentId));

        environmentList.Add(new DisplayResult { Display = "ALL", Result = -1 });

        foreach (var item in environments.OrderBy(x => x.Name))
        {
          environmentList.Add(new DisplayResult { Display = item.Name, Result = item.EnvironmentId });
        }
      }
    }

    private void UpdateCampaignData(string key, string value)
    {
      CampaignData[key] = value;
      SaveDataToDB();
    }

    #endregion

    #region Events

    private void BtnRollD20_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD20.Value > 0)
      {
        var diceList = DiceRoll(20, IntNumD20.Value ?? 0, IntAddD20.Value ?? 0, RadPlusD20.IsChecked ?? false);
        LbxD20.ItemsSource = diceList;

        LblAvgD20.Content = diceList.Average(x => x.Result);
      }
    }

    private void BtnRollD4_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD4.Value > 0)
      {
        var diceList = DiceRoll(4, IntNumD4.Value ?? 0, IntAddD4.Value ?? 0, RadPlusD4.IsChecked ?? false);
        LbxD4.ItemsSource = diceList;

        LblTotalD4.Content = diceList.Sum(x => x.Result);
      }
    }

    private void BtnRollD6_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD6.Value > 0)
      {
        var diceList = DiceRoll(6, IntNumD6.Value ?? 0, IntAddD6.Value ?? 0, RadPlusD6.IsChecked ?? false);
        LbxD6.ItemsSource = diceList;

        LblTotalD6.Content = diceList.Sum(x => x.Result);
      }
    }

    private void BtnRollD8_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD8.Value > 0)
      {
        var diceList = DiceRoll(8, IntNumD8.Value ?? 0, IntAddD8.Value ?? 0, RadPlusD8.IsChecked ?? false);
        LbxD8.ItemsSource = diceList;

        LblTotalD8.Content = diceList.Sum(x => x.Result);
      }
    }

    private void BtnRollD10_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD10.Value > 0)
      {
        var diceList = DiceRoll(10, IntNumD10.Value ?? 0, IntAddD10.Value ?? 0, RadPlusD10.IsChecked ?? false);
        LbxD10.ItemsSource = diceList;

        LblTotalD10.Content = diceList.Sum(x => x.Result);
      }
    }

    private void BtnRollD12_Click(object sender, RoutedEventArgs e)
    {
      if (IntNumD12.Value > 0)
      {
        var diceList = DiceRoll(12, IntNumD12.Value ?? 0, IntAddD12.Value ?? 0, RadPlusD12.IsChecked ?? false);
        LbxD12.ItemsSource = diceList;

        LblTotalD12.Content = diceList.Sum(x => x.Result);
      }
    }

    private void BtnRollD100_Click(object sender, RoutedEventArgs e)
    {
      LblTotalD100.Content = random.Next(101);
    }

    private void BtnGenerateEncounters_Click(object sender, RoutedEventArgs e)
    {
      GenEncounters();
    }

    private void BtnClearEncounters_Click(object sender, RoutedEventArgs e)
    {
      randomEncounterItems.Clear();
    }

    private void BtnAddToCombat_Click(object sender, RoutedEventArgs e)
    {
      AddCreatureToCombat();
    }

    private void LbxEncounterCreatures_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      AddCreatureToCreatureInfo();
      HelperTabs.SelectedIndex = 2;
    }

    private void LbxEncounterCRs_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxEncounterCRs.SelectedItem != null)
      {
        LoadMonsters((int[])LbxEncounterCRs.SelectedValue);
      }
    }

    private void BtnCombatNextRound_Click(object sender, RoutedEventArgs e)
    {
      CombatNextRound();
    }

    private void BtnCombatEnd_Click(object sender, RoutedEventArgs e)
    {
      CombatRound = 0;
      combatEffectItems.Clear();

      var removeItems = new List<CombatGridItem>();
      foreach (var item in combatGridItems)
      {
        if (!item.PC)
          removeItems.Add(item);
      }
      foreach (var item in removeItems)
      {
        combatGridItems.Remove(item);
      }
    }

    private void BtnCombatAddNew_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(CgiName))
      {
        var cgi = new CombatGridItem()
        {
          Name = CgiName,
          AC = CgiAC,
          ACFlat = CgiFlat,
          ACTouch = CgiTouch,
          BestiaryId = 0,
          Fort = CgiFort,
          Ref = CgiRef,
          Will = CgiWill,
          HP = CgiHP,
          Init = CgiInit,
          PC = CgiPC
        };

        combatGridItems.Add(cgi);

        ClearCombatAdd();
      }
    }

    private void BtnCombatAddEffect_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(CefName))
      {
        var cef = new CombatEffectItem()
        {
          Rounds = CefRounds,
          Effect = CefName
        };

        combatEffectItems.Add(cef);

        ClearCombatEffectAdd();
      }
    }

    private void BtnCombatSort_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<CombatGridItem>(combatGridItems.OrderByDescending(x => x.Init));
      combatGridItems.Clear();
      combatGridItems.AddRange(temp);
    }

    private void BtnCombatDuplicate_Click(object sender, RoutedEventArgs e)
    {
      if (DgCombatGrid.SelectedItems != null)
      {
        foreach (CombatGridItem item in DgCombatGrid.SelectedItems)
        {
          combatGridItems.Add(new CombatGridItem(item));
        }
      }
    }

    private void BtnCombatDelete_Click(object sender, RoutedEventArgs e)
    {
      if (DgCombatGrid.SelectedItems != null)
      {
        var removeItems = new List<CombatGridItem>();
        foreach (CombatGridItem item in DgCombatGrid.SelectedItems)
        {
          removeItems.Add(item);
        }
        foreach (var item in removeItems)
        {
          combatGridItems.Remove(item);
        }
      }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      SaveDataToDisk();
    }

    private void CommandSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      SaveDataToDisk();
    }

    private void BtnCombatClearAll_Click(object sender, RoutedEventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to Clear All?") == MessageBoxResult.OK)
      {
        combatEffectItems.Clear();
        combatGridItems.Clear();
        ClearCombatAdd();
        ClearCombatEffectAdd();
        CombatRound = 1;
      }
    }

    private void LbxCreatureInfo_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxCreatureInfo.SelectedItem != null)
      {
        var selectedItem = (DisplayResult)LbxCreatureInfo.SelectedItem;
        selectedCombatMonsterId = selectedItem.Result;
        LoadCreatureInfo();
      }
    }

    private void BtnNextDay_Click(object sender, RoutedEventArgs e)
    {
      AddDays(1);
      NextWeather();
      GenEncounters();
    }

    private void BtnAddDays_Click(object sender, RoutedEventArgs e)
    {
      AddDays(DaysAdjust);
    }

    private void SpinRations_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
    {
      AddRations(RationsAdjust * (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase ? 1 : -1));
      TxtRations.Clear();
    }

    private void BtnNextWeather_Click(object sender, RoutedEventArgs e)
    {
      NextWeather();
    }

    private void MenuEditConfig_Click(object sender, RoutedEventArgs e)
    {
      var popup = new Controls.ConfigEditor();
      popup.ShowDialog();

      if (PFConfig.ConfigExists())
        DBClient.ReloadConfig(true);

      if (!LoadDBData())
      {
        MessageBox.Show("ERROR - Failed to connect to server. Is the config set correctly?");
      }
    }

    #endregion

    private void BtnManualWeather_Click(object sender, RoutedEventArgs e)
    {
      var weatherList = DBClient.GetList("Weather");
      var popup = new ListPopUp(weatherList, "Select the new Weather:");
      popup.ShowDialog();
      if (popup.DialogResult == true && popup.SelectedResult > 0)
      {
        CurrentWeather = DBClient.GetWeather(popup.SelectedResult);
        LblCurrentWeather.Content = CurrentWeather.Name;
        WeatherLock = true;
      }
    }

    private void BtnCombatAddFromBestiary_Click(object sender, RoutedEventArgs e)
    {
      var bList = DBClient.GetList("Bestiary");
      var popup = new ListPopUp(bList);
      popup.ShowDialog();

      if (popup.DialogResult == true && popup.SelectedResult > 0)
      {
        var bItem = bList.First(x => x.Id == popup.SelectedResult);

        var b = DBClient.GetBestiary(bItem.Id);
        combatGridItems.Add(new CombatGridItem(b));

        if (!creatureInfos.Select(x => x.Result).Contains(bItem.Id))
          creatureInfos.Add(new DisplayResult() { Display = bItem.Name, Result = bItem.Id });
      }
    }

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        e.Handled = true;
        if (!LoadDBData())
        {
          MessageBox.Show("ERROR - Failed to connect to server. Is the config set correctly?");
        }
      }
    }

    private void BtnEvtSave_Click(object sender, RoutedEventArgs e)
    {
      SaveTrackedEvent();

      if (CurrentEvent.EventId == 0)
        CurrentEvent = new LiveEvent(DBClient.CreateTrackedEvent(CurrentEvent.Export()));
      else
        DBClient.UpdateTrackedEvent(CurrentEvent.Export());
    }

    private void BtnEvtNew_Click(object sender, RoutedEventArgs e)
    {
      CurrentEvent = new LiveEvent();

      LoadTrackedEvent();
    }

    private void DgEvt_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (DgEvt.SelectedItem != null)
      {
        CurrentEvent = liveEventList.First(x => x.EventId == ((LiveEvent)DgEvt.SelectedItem).EventId);
        LoadTrackedEvent();
      }
    }

    private void BtnEvtSortName_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<LiveEvent>(liveEventList.OrderBy(x => x.Name));
      liveEventList.Clear();
      liveEventList.AddRange(temp);
    }

    private void BtnEvtSortNext_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<LiveEvent>(liveEventList.Where(x => x.DateNextOccurring >= CurrentDate).OrderBy(x => x.DateNextOccurring));
      temp.AddRange(liveEventList.Where(x => x.DateNextOccurring < CurrentDate).OrderBy(x => x.DateNextOccurring));

      liveEventList.Clear();
      liveEventList.AddRange(temp);
    }

    private void BtnSaveCampaignData_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(CampaignDataName))
      {
        UpdateCampaignData(CampaignDataName, CampaignDataValue);

        campaignDataList.Clear();
        campaignDataList.AddRange(CampaignData.Keys);
      }
    }

    private void BtnAddCampaignData_Click(object sender, RoutedEventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(CampaignDataNew) && !CampaignData.ContainsKey(CampaignDataNew))
      {
        UpdateCampaignData(CampaignDataNew, string.Empty);

        campaignDataList.Clear();
        campaignDataList.AddRange(CampaignData.Keys);
      }
    }

    private void BtnCampaignNew_Click(object sender, RoutedEventArgs e)
    {
      ActiveCampaign = new Campaign();
      CampaignId = 0;
      CampaignName = CampaignNotes = string.Empty;
    }

    private void BtnCampaignSave_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(CampaignName))
      {
        TxtCampaignName.BorderThickness = new Thickness(2);
        TxtCampaignName.BorderBrush = new SolidColorBrush(Colors.Red);
      }
      else
      {
        TxtCampaignName.BorderThickness = new Thickness(1);
        TxtCampaignName.BorderBrush = null;
      }

      SaveCampaign();

      if (ActiveCampaign.CampaignId == 0)
        ActiveCampaign = DBClient.CreateCampaign(ActiveCampaign);
      else
        DBClient.UpdateCampaign(ActiveCampaign);

      campaigns = DBClient.GetCampaigns();
      campaignList.Clear();
      foreach (var item in campaigns.OrderBy(x => x.CampaignName))
      {
        campaignList.Add(new DisplayResult() { Display = item.CampaignName, Result = item.CampaignId });
      }

      LoadCampaign();
    }

    private void LbxCampaign_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxCampaignData.SelectedItem != null)
      {
        var data = LbxCampaignData.SelectedItem as string;
        CampaignDataName = data;
        CampaignDataValue = CampaignData[data];
      }
    }

    private void DrpCampaignSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (DrpCampaignSelect.SelectedItem != null)
      {
        ActiveCampaign = DBClient.GetCampaign((int)DrpCampaignSelect.SelectedValue);
        LoadCampaign();
      }
    }

    private void BtnRandomWeather_Click(object sender, RoutedEventArgs e)
    {
      CurrentWeatherGroup = GetRandomWeather(false);
      CurrentWeather = DBClient.GetWeather(CurrentWeatherGroup.WeatherId);
    }

    private void LbxContinent_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxContinent.SelectedItem != null)
        SwitchContinents((int)LbxContinent.SelectedValue);
    }
  }
}
