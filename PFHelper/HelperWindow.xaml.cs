using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using CommonUI;
using CommonUI.Popups;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using Microsoft.Win32;
using PFHelper.Classes;

namespace PFHelper
{
  /// <summary>
  /// Interaction logic for HelperWindow.xaml
  /// </summary>
  public partial class HelperWindow : Window
  {
    #region Static Properties

    public static int TAB_CREATUREINFO = 2;

    #endregion

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

    public ObservableCollection<CombatGridItem> CombatGridItems { get; set; }

    public ObservableCollection<CombatEffectItem> CombatEffectItems { get; set; }

    public ObservableCollection<LiveEvent> LiveEvents { get; set; }

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

    public int EventFreqId
    {
      get
      {
        if (DrpEvtFreqSpan != null)
          return (int)DrpEvtFreqSpan.SelectedValue;

        return 0;
      }
      set
      {
        DrpEvtFreqSpan.SelectedValue = value;
      }
    }

    public bool EventLocalOnly
    {
      get { return CbxEvtShowLocal.IsChecked ?? false; }
      set { CbxEvtShowLocal.IsChecked = value; }
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

    public int CampaignDataGen
    {
      get
      {
        if (DrpCampaignDataGenerate.SelectedItem != null)
          return (int)DrpCampaignDataGenerate.SelectedValue;

        return 0;
      }
      set
      {
        DrpCampaignDataGenerate.SelectedValue = value;
      }
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
      set
      {
        if (value > 0 && continentList.Select(x => x.Result).Contains(value))
          DrpEvtContinent.SelectedValue = value;
        else
          DrpEvtContinent.SelectedIndex = -1;
      }
    }

    #endregion

    #region Data Properties

    private const string FILENAME_CBDATA = "cbdefault";
    private const string FILENAME_SAVEDATA = "pfdat";
    private const string EXT_CBDATA = ".cbdat";
    private const string EXT_SAVEDATA = ".dat";
    private readonly string APPLICATIONPATH;

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
    private string selectedCombatMonsterHtml;
    private int selectedCombatMonsterId;
    private string[] DayNames = { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat" };

    #endregion

    #region Constructor

    public HelperWindow()
    {
      InitializeComponent();
      this.DataContext = this;

      APPLICATIONPATH = System.Environment.CurrentDirectory;

      random = new Random();
      CurrentDate = new FantasyDate();

      encounterResults = new ObservableCollection<DisplayValues>();
      randomEncounterItems = new ObservableCollection<DisplayResult>();
      CombatEffectItems = new ObservableCollection<CombatEffectItem>();
      CombatGridItems = new ObservableCollection<CombatGridItem>();
      continentList = new ObservableCollection<DisplayResult>();
      creatureInfos = new ObservableCollection<DisplayResult>();
      planeList = new ObservableCollection<DisplayResult>();
      timeList = new ObservableCollection<DisplayResult>();
      LiveEvents = new ObservableCollection<LiveEvent>();
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
        = DrpEvtContinent.DisplayMemberPath = DrpEvtFreqSpan.DisplayMemberPath = DrpCampaignDataGenerate.DisplayMemberPath = "Display";
      LbxD20.SelectedValuePath = LbxD4.SelectedValuePath = LbxD6.SelectedValuePath = LbxD8.SelectedValuePath = LbxD10.SelectedValuePath = LbxD12.SelectedValuePath
        = LbxEncounterCreatures.SelectedValuePath = LbxContinent.SelectedValuePath = LbxCreatureInfo.SelectedValuePath = LbxPlane.SelectedValuePath
        = LbxTime.SelectedValuePath = LbxEnvironment.SelectedValuePath = DrpEvtType.SelectedValuePath = DrpCampaignSelect.SelectedValuePath
        = DrpEvtContinent.SelectedValuePath = DrpEvtFreqSpan.SelectedValuePath = DrpCampaignDataGenerate.SelectedValuePath = "Result";

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

      LoadDataFromDisk();
      LoadContinentEnvironments();

      UpdateDisplays();

      IntNumD20.Value = IntNumD12.Value = IntNumD10.Value = IntNumD8.Value = IntNumD6.Value = IntNumD4.Value = 1;

      foreach (TrackedEventType item in Enum.GetValues(typeof(TrackedEventType)))
      {
        DrpEvtType.Items.Add(new DisplayResult { Display = item.ToString(), Result = (int)item });
      }

      foreach (TrackedEventFrequency item in Enum.GetValues(typeof(TrackedEventFrequency)))
      {
        DrpEvtFreqSpan.Items.Add(new DisplayResult { Display = item.ToString(), Result = (int)item });
      }
      EventFreqId = (int)TrackedEventFrequency.Days;

      foreach (CampaignDataGenType item in Enum.GetValues(typeof(CampaignDataGenType)))
      {
        DrpCampaignDataGenerate.Items.Add(new DisplayResult { Display = item.ToString(), Result = (int)item });
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
          var campaignData = DBClient.GetCampaignData();
          if (campaignData != null)
            CampaignData.AddRange(campaignData);

          continentList.Clear();
          timeList.Clear();
          planeList.Clear();
          LiveEvents.Clear();
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
            LiveEvents.Add(new LiveEvent(item));
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

      if (CampaignData.ContainsKey(PFConfig.STR_SAVEDATA))
      {
        var saveObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PFHelperSaveObject>(CampaignData[PFConfig.STR_SAVEDATA]);
        LoadSavedData(saveObject);
      }

      return ret;
    }

    private bool LoadSavedData(PFHelperSaveObject saveObject)
    {
      var ret = false;

      if (saveObject != null && saveObject.Date != null)
      {
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
        EventLocalOnly = saveObject.CbxEvtLocal;

        RationsLeft = saveObject.Rations;
        CurrentDate = saveObject.Date;
        CurrentWeather = saveObject.Weather;
        WeatherResult = saveObject.WeatherResult;
        CurrentContinent = saveObject.Continent;
        CurrentPlane = saveObject.Plane;
        CurrentTime = saveObject.Time;
        CurrentTerrain = saveObject.Terrain;

        CombatEffectItems.Clear();
        CombatGridItems.Clear();
        CombatEffectItems.AddRange(saveObject.CombatEffects);
        CombatGridItems.AddRange(saveObject.CombatGridItems);

        ret = true;
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

        CombatEffectItems.Clear();
        CombatGridItems.Clear();
      }

      return ret;
    }

    private void SaveDataToDisk(string path = "")
    {
      if (string.IsNullOrWhiteSpace(path))
        path = Path.Combine(APPLICATIONPATH, FILENAME_SAVEDATA + EXT_SAVEDATA);

      var saveObject = new PFHelperSaveObject();

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
      saveObject.CbxEvtLocal = EventLocalOnly;

      saveObject.Rations = RationsLeft;
      saveObject.Date = CurrentDate;
      saveObject.Weather = CurrentWeather;
      saveObject.WeatherResult = WeatherResult;
      saveObject.Continent = CurrentContinent;
      saveObject.Plane = CurrentPlane;
      saveObject.Time = CurrentTime;
      saveObject.Terrain = CurrentTerrain;

      saveObject.CombatEffects = CombatEffectItems.ToList();
      saveObject.CombatGridItems = CombatGridItems.ToList();

      File.WriteAllText(path, Newtonsoft.Json.JsonConvert.SerializeObject(saveObject));
    }

    private bool LoadDataFromDisk(string path = "")
    {
      if (string.IsNullOrWhiteSpace(path))
        path = Path.Combine(APPLICATIONPATH, FILENAME_SAVEDATA + EXT_SAVEDATA);

      PFHelperSaveObject saveObject = null;

      if (File.Exists(path) && Path.GetExtension(path).Equals(EXT_SAVEDATA, StringComparison.InvariantCultureIgnoreCase))
        saveObject = Newtonsoft.Json.JsonConvert.DeserializeObject<PFHelperSaveObject>(File.ReadAllText(path));

      return LoadSavedData(saveObject);
    }

    private void SaveCampaignData()
    {
      var saveObject = new PFHelperSaveObject();

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

      saveObject.CombatEffects = CombatEffectItems.ToList();
      saveObject.CombatGridItems = CombatGridItems.ToList();

      if (!CampaignData.ContainsKey(PFConfig.STR_SAVEDATA))
        CampaignData.Add(PFConfig.STR_SAVEDATA, string.Empty);

      CampaignData[PFConfig.STR_SAVEDATA] = Newtonsoft.Json.JsonConvert.SerializeObject(saveObject);

      SaveDataToDisk();
      SaveDataToDB();
    }

    private void SaveDataToDB()
    {
      try
      {
        foreach (var item in LiveEvents)
        {
          if (item.EventId > 0)
            DBClient.UpdateTrackedEvent(item.Export());
          else
            DBClient.CreateTrackedEvent(item.Export());
        }

        trackedEvents = DBClient.GetTrackedEvents();
        LiveEvents.Clear();
        foreach (var item in trackedEvents)
        {
          LiveEvents.Add(new LiveEvent(item));
        }
      }
      catch (Exception ex)
      {
        File.WriteAllText(Path.Combine(APPLICATIONPATH, $"Events {DateTime.Now.ToString("yyyyMMdd-hhmmss")}.log"), Newtonsoft.Json.JsonConvert.SerializeObject(LiveEvents));
        MessageBox.Show("Failed to save Events!\n" + ex.Message);
      }

      try
      {
        DBClient.UpdateCampaignData(CampaignData);
      }
      catch (Exception ex)
      {
        File.WriteAllText(Path.Combine(APPLICATIONPATH, $"CampaignData {DateTime.Now.ToString("yyyyMMdd-hhmmss")}.log"), Newtonsoft.Json.JsonConvert.SerializeObject(CampaignData));
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
        apl = 0;

      for (int t = 0; t < 3; t++)
      {
        var CRs = new List<int[]>();
        CRs.Add(new int[] { apl });
        CRs.Add(new int[] { apl - 2, apl - 2 });
        CRs.Add(new int[] { apl - 4, apl - 4, apl - 4, apl - 4 });
        CRs.Add(new int[] { apl - 2, apl - 3, apl - 3 });
        CRs.Add(new int[] { apl - 2, apl - 4, apl - 4, apl - 4 });
        CRs.Add(new int[] { apl - 5, apl - 5, apl - 5, apl - 5, apl - 5, apl - 5 });

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

        apl += 1;
      }

      int rand = random.Next(30) - 4;
      encounterResults.Add(new DisplayValues() { Display = "[X] " + rand.ToString(), Values = new int[] { rand } });
    }

    private void GenMysterious()
    {
      if (random.Next(10000) == 0)
      {
        var m = random.Next(10);

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
        else if (m <= 5)
        {
          LblChanceMysterious.Foreground = Brushes.Green;
          LblChanceMysterious.Content = "GUD";
        }
        else
        {
          LblChanceMysterious.Foreground = Brushes.Orange;
          LblChanceMysterious.Content = "BAD";
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
          CombatGridItems.Add(new CombatGridItem(b));
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

      foreach (var item in CombatEffectItems)
      {
        if (item.Rounds <= 0)
          removeItems.Add(item);
        else
          item.Rounds--;
      }

      foreach (var item in removeItems)
      {
        CombatEffectItems.Remove(item);
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
      foreach (var evt in LiveEvents.Where(x => x.ActiveEvent))
      {
        evt.ActiveEvent = false;
      }

      for (int j = 1; j <= i; j++)
      {
        foreach (var evt in LiveEvents)
        {
          if (evt.DateNextOccurring == oldDate.AddDays(j))
          {
            RunLiveEvent(evt);
            evt.ActiveEvent = true;
          }
        }
      }

      if (activeEventIds.Count > 0)
        HelperTabs.SelectedIndex = 3;

      UpdateCampaignData("CurrentDate", CurrentDate.ToNumDate());
    }

    private void RunLiveEvent(LiveEvent e)
    {
      e.DateLastOccurred = new FantasyDate(e.DateNextOccurring.ShortDate);
      switch (e.EventFrequency)
      {
        case TrackedEventFrequency.Days:
          e.DateNextOccurring.AddDays(e.ReoccurFreq);
          break;
        case TrackedEventFrequency.Weeks:
          e.DateNextOccurring.AddDays(e.ReoccurFreq * 7);
          break;
        case TrackedEventFrequency.Months:
          e.DateNextOccurring.AddMonths(e.ReoccurFreq);
          break;
        case TrackedEventFrequency.Years:
          e.DateNextOccurring.AddYears(e.ReoccurFreq);
          break;
      }

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

      if (WeatherResult == null && CurrentWeather == null && CurrentWeatherGroup == null)// DEBUG
      {
        LblCurrentWeather.Content = random.Next(100);
        LblCurrentWeatherGroup.Content = "DEBUG";
        return;
      }

      if (WeatherResult == null || ContinentId != WeatherResult.ContinentId || CurrentWeatherGroup == null)
      {
        ReloadWeatherTable();
        CurrentWeatherGroup = GetRandomWeather();
      }
      else if (CurrentMonth.SeasonId != WeatherResult.SeasonId)
        ReloadWeatherTable();

      while (d > 0 && CurrentWeatherGroup.Duration > 0)
      {
        if (CurrentWeatherGroup.Duration > d)
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
            CurrentWeatherGroup = GetRandomWeather(true);
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
        EventDate = CurrentEvent.DateNextOccurring != null ? CurrentEvent.DateNextOccurring.ShortDate : string.Empty;
        EventLastDate = CurrentEvent.DateLastOccurred != null ? CurrentEvent.DateLastOccurred.ShortDate : string.Empty;
        EventLocation = CurrentEvent.Location;
        EventName = CurrentEvent.Name;
        EventNotes = CurrentEvent.Notes;
        EventFreqId = CurrentEvent.ReoccurFreq;
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
      CurrentEvent.ReoccurFreq = EventFreqId;
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

    private void ImportCombatGrid(string filename)
    {
      if (File.Exists(filename))
      {
        var newGrid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CombatGridItem>>(File.ReadAllText(filename));
        CombatGridItems.Clear();
        foreach (var item in newGrid)
        {
          CombatGridItems.Add(item);
        }
      }
    }

    private void ExportCombatGrid(string filename)
    {
      var saveGrid = new List<CombatGridItem>();
      foreach (var item in CombatGridItems)
      {
        saveGrid.Add(item);
      }
      File.WriteAllText(filename, Newtonsoft.Json.JsonConvert.SerializeObject(saveGrid));
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
      HelperTabs.SelectedIndex = TAB_CREATUREINFO;
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
      CombatEffectItems.Clear();

      var removeItems = new List<CombatGridItem>();
      foreach (var item in CombatGridItems)
      {
        if (!item.PC)
          removeItems.Add(item);
      }
      foreach (var item in removeItems)
      {
        CombatGridItems.Remove(item);
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
          MaxHP = CgiHP,
          Init = CgiInit,
          PC = CgiPC
        };

        CombatGridItems.Add(cgi);

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

        CombatEffectItems.Add(cef);

        ClearCombatEffectAdd();
      }
    }

    private void BtnCombatSort_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<CombatGridItem>(CombatGridItems.OrderByDescending(x => x.Init));
      CombatGridItems.Clear();
      CombatGridItems.AddRange(temp);
    }

    private void BtnCombatDuplicate_Click(object sender, RoutedEventArgs e)
    {
      if (DgCombatGrid.SelectedItems != null)
      {
        foreach (CombatGridItem item in DgCombatGrid.SelectedItems)
        {
          CombatGridItems.Add(new CombatGridItem(item));
        }
      }
    }

    private void BtnCombatDelete_Click(object sender, RoutedEventArgs e)
    {
      if (DgCombatGrid.SelectedItems != null)
      {
        var removeItems = new List<CombatGridItem>();
        bool removePCs = false;
        foreach (CombatGridItem item in DgCombatGrid.SelectedItems)
        {
          removeItems.Add(item);
        }

        if (removeItems.Any(x => x.PC))
        {
          var dialogResult = MessageBox.Show("One or more PCs selected, remove them?", string.Empty, MessageBoxButton.YesNoCancel);

          if (dialogResult == MessageBoxResult.Cancel)
            return;

          if (dialogResult == MessageBoxResult.Yes)
            removePCs = true;
        }

        foreach (var item in removeItems)
        {
          if (!item.PC || removePCs)
            CombatGridItems.Remove(item);
        }
      }
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      SaveDataToDB();
      var savePath = Path.Combine(APPLICATIONPATH, FILENAME_SAVEDATA + EXT_SAVEDATA);
      SaveDataToDisk(savePath);
    }

    private void CommandSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      SaveDataToDB();
      var savePath = Path.Combine(APPLICATIONPATH, FILENAME_SAVEDATA + EXT_SAVEDATA);
      SaveDataToDisk(savePath);
    }

    private void BtnCombatClearAll_Click(object sender, RoutedEventArgs e)
    {
      var dialogResult = MessageBox.Show("Are you sure you want to Clear All?", string.Empty, MessageBoxButton.YesNoCancel);

      if (dialogResult == MessageBoxResult.Cancel)
        return;

      if (dialogResult == MessageBoxResult.Yes)
      {
        CombatEffectItems.Clear();
        CombatGridItems.Clear();
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
      //NextWeather();
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
      var bList = DBClient.GetList("Bestiary").OrderBy(x => x.Name);
      var formattedList = new List<DisplayResult>();
      foreach (var item in bList)
      {
        formattedList.Add(new DisplayResult { Display = $"[{ParseCR(int.Parse(item.Notes))}] {item.Name}", Result = item.Id });
      }

      var popup = new ListPopUp(bList);
      popup.ShowDialog();

      if (popup.DialogResult == true && popup.SelectedResult > 0)
      {
        var bItem = bList.First(x => x.Id == popup.SelectedResult);

        var b = DBClient.GetBestiary(bItem.Id);
        CombatGridItems.Add(new CombatGridItem(b));

        if (!creatureInfos.Select(x => x.Result).Contains(bItem.Id))
          creatureInfos.Add(new DisplayResult() { Display = $"{bItem.Name} [{ParseCR(int.Parse(bItem.Notes))}]", Result = bItem.Id });
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
      {
        var createdEvent = DBClient.CreateTrackedEvent(CurrentEvent.Export());
        CurrentEvent = new LiveEvent(createdEvent ?? CurrentEvent.Export());
      }
      else
        DBClient.UpdateTrackedEvent(CurrentEvent.Export());

      if (CurrentEvent.EventId == 0)
        MessageBox.Show("WARNING - Event not saved to DB!");

      if (!EventLocalOnly || CurrentEvent.ContinentId == 0 || CurrentEvent.ContinentId == CurrentContinent.ContinentId)
        LiveEvents.Add(CurrentEvent);
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
        CurrentEvent = LiveEvents.First(x => x.EventId == ((LiveEvent)DgEvt.SelectedItem).EventId);
        LoadTrackedEvent();
      }
    }

    private void BtnEvtSortName_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<LiveEvent>(LiveEvents.OrderBy(x => x.Name));
      LiveEvents.Clear();
      LiveEvents.AddRange(temp);
    }

    private void BtnEvtSortNext_Click(object sender, RoutedEventArgs e)
    {
      var temp = new List<LiveEvent>(LiveEvents.Where(x => x.DateNextOccurring >= CurrentDate).OrderBy(x => x.DateNextOccurring));
      temp.AddRange(LiveEvents.Where(x => x.DateNextOccurring < CurrentDate).OrderBy(x => x.DateNextOccurring));

      LiveEvents.Clear();
      LiveEvents.AddRange(temp);
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

    private void BtnEncounterAddFromBestiary_Click(object sender, RoutedEventArgs e)
    {
      var bList = DBClient.GetList("Bestiary").OrderBy(x => x.Name);
      var formattedList = new List<DisplayResult>();
      foreach (var item in bList)
      {
        formattedList.Add(new DisplayResult { Display = $"[{ParseCR(int.Parse(item.Notes))}] {item.Name}", Result = item.Id });
      }

      var popup = new ListPopUp(bList);
      popup.ShowDialog();

      if (popup.DialogResult == true && popup.SelectedResult > 0)
      {
        var bItem = bList.First(x => x.Id == popup.SelectedResult);

        var b = DBClient.GetBestiary(bItem.Id);
        randomEncounterItems.Add(new DisplayResult() { Display = $"{bItem.Name} [{ParseCR(int.Parse(bItem.Notes))}]", Result = bItem.Id });

        if (!creatureInfos.Select(x => x.Result).Contains(bItem.Id))
          creatureInfos.Add(new DisplayResult() { Display = $"{bItem.Name} [{ParseCR(int.Parse(bItem.Notes))}]", Result = bItem.Id });
      }
    }

    private void CbxEvtShowLocal_Checked(object sender, RoutedEventArgs e)
    {
      LiveEvents.Clear();
      foreach (var item in trackedEvents)
      {
        if (!EventLocalOnly || item.ContinentId == 0 || item.ContinentId == CurrentContinent.ContinentId)
          LiveEvents.Add(new LiveEvent(item));
      }
    }

    private void BtnCombatCreatureInfo_Click(object sender, RoutedEventArgs e)
    {
      if (DgCombatGrid.SelectedItem != null)
      {
        var selectedItem = DgCombatGrid.SelectedItem as CombatGridItem;
        if (selectedItem.BestiaryId > 0)
        {
          selectedCombatMonsterId = selectedItem.BestiaryId;
          LoadCreatureInfo();
          HelperTabs.SelectedIndex = TAB_CREATUREINFO;
        }
      }
    }

    private void DgCombatGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (DgCombatGrid.SelectedItem != null)
      {
        var selectedItem = DgCombatGrid.SelectedItem as CombatGridItem;
        var popup = new CombatPopUp(selectedItem);
        popup.ShowDialog();
        if (popup.DialogResult == true)
        {
          selectedItem.BestiaryId = popup.CombatGridItem.BestiaryId;
          selectedItem.Init = popup.CombatGridItem.Init;
          selectedItem.Name = popup.CombatGridItem.Name;
          selectedItem.PC = popup.CombatGridItem.PC;
          selectedItem.HP = popup.CombatGridItem.HP;
          selectedItem.MaxHP = popup.CombatGridItem.MaxHP;
          selectedItem.AC = popup.CombatGridItem.AC;
          selectedItem.ACTouch = popup.CombatGridItem.ACTouch;
          selectedItem.ACFlat = popup.CombatGridItem.ACFlat;
          selectedItem.Fort = popup.CombatGridItem.Fort;
          selectedItem.Ref = popup.CombatGridItem.Ref;
          selectedItem.Will = popup.CombatGridItem.Will;
          selectedItem.Subd = popup.CombatGridItem.Subd;
          selectedItem.Note = popup.CombatGridItem.Note;
        }
      }
    }

    private void DgCombatGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
    {
      var oldVal = e.Row.Item as CombatGridItem;

      if (e.Column.Header.Equals(nameof(CombatGridItem.HP)) && e.EditingElement is TextBox)
      {
        var editText = e.EditingElement as TextBox;
        if (editText.Text.StartsWith("-") || editText.Text.StartsWith("+"))
        {
          if (int.TryParse(editText.Text, out int i))
            editText.Text = (oldVal.HP + i).ToString();
        }
        else if (editText.Text.StartsWith("M", StringComparison.InvariantCultureIgnoreCase))
        {
          editText.Text = oldVal.MaxHP.ToString();
        }
      }
    }

    private void BtnCombatImport_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new OpenFileDialog();
      dialog.DefaultExt = EXT_CBDATA;
      dialog.InitialDirectory = APPLICATIONPATH;
      if (dialog.ShowDialog() == true)
      {
        ImportCombatGrid(dialog.FileName);
      }
    }

    private void BtnCombatExport_Click(object sender, RoutedEventArgs e)
    {
      var dialog = new SaveFileDialog();
      dialog.DefaultExt = EXT_CBDATA;
      dialog.InitialDirectory = APPLICATIONPATH;
      dialog.AddExtension = true;
      if (dialog.ShowDialog() == true)
      {
        ExportCombatGrid(dialog.FileName);
      }
    }

    #endregion

    private void DgCombatGrid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.Delete)
      {
        e.Handled = true;
        BtnCombatDelete_Click(null, null);
      }
    }

    private void BtnGenerateCampaignData_Click(object sender, RoutedEventArgs e)
    {
      if (Enum.IsDefined(typeof(CampaignDataGenType), CampaignDataGen))
      {
        switch (CampaignDataGen)
        {
          case (int)CampaignDataGenType.Formula:
            // Formula generation window
            break;
          case (int)CampaignDataGenType.PFHelper:
            // Generate new PFHelper save object
            break;
        }
      }
    }
  }
}
