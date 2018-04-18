using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
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
      get
      {
        if (LbxContinent.SelectedItem != null)
          return (int)LbxContinent.SelectedValue;
        return 0;
      }
      set
      {
        LbxContinent.SelectedValue = value;
      }
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
        LbxTime.SelectedValue = value;
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
        LbxPlane.SelectedValue = value;
      }
    }

    public int TerrainId
    {
      get
      {
        if (LbxTerrain.SelectedItem != null)
          return (int)LbxTerrain.SelectedValue;
        return 0;
      }
      set
      {
        LbxTerrain.SelectedValue = value;
      }
    }

    public int CombatRound
    {
      get { return _combatRound; }
      set { LblCombatRound.Content = _combatRound = value; }
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

    #endregion

    #region Data Properties

    private ObservableCollection<CombatGridItem> combatGridItems;
    private ObservableCollection<CombatEffectItem> combatEffectItems;
    private ObservableCollection<DisplayResult> randomEncounterItems;
    private ObservableCollection<DisplayResult> continentList;
    private ObservableCollection<DisplayValues> encounterResults;
    private ObservableCollection<DisplayResult> creatureInfos;

    private List<TrackedEvent> trackedEvents;
    private List<Continent> continents;
    private List<Season> seasons;
    private List<Month> months;

    private RandomWeatherResult WeatherResult;
    private Random random;
    private FantasyDate CurrentDate;
    private Weather CurrentWeather;
    private ContinentWeather CurrentWeatherGroup;
    private string saveDataPath = Path.Combine(System.Environment.CurrentDirectory, "pfdat.dat");
    private string selectedCombatMonsterHtml;
    private int selectedCombatMonsterId;

    #endregion

    #region Constructor

    public HelperWindow()
    {
      InitializeComponent();
      this.DataContext = this;

      random = new Random();
      CurrentDate = new FantasyDate();

      if (!LoadDBData())
      {
        MessageBox.Show("ERROR - Failed to connect to server. Is the config set correctly?");
        continents = new List<Continent>();
        seasons = new List<Season>();
        months = new List<Month>();
        trackedEvents = new List<TrackedEvent>();
      }

      LoadSavedData();

      encounterResults = new ObservableCollection<DisplayValues>();
      randomEncounterItems = new ObservableCollection<DisplayResult>();
      combatEffectItems = new ObservableCollection<CombatEffectItem>();
      combatGridItems = new ObservableCollection<CombatGridItem>();
      continentList = new ObservableCollection<DisplayResult>();
      creatureInfos = new ObservableCollection<DisplayResult>();

      foreach (var item in continents)
      {
        continentList.Add(new DisplayResult() { Display = item.Name, Result = item.ContinentId });
      }

      LbxD20.DisplayMemberPath = LbxD4.DisplayMemberPath = LbxD6.DisplayMemberPath = LbxD8.DisplayMemberPath = LbxD10.DisplayMemberPath = LbxD12.DisplayMemberPath
        = LbxEncounterCreatures.DisplayMemberPath = LbxContinent.DisplayMemberPath = LbxCreatureInfo.DisplayMemberPath = "Display";
      LbxD20.SelectedValuePath = LbxD4.SelectedValuePath = LbxD6.SelectedValuePath = LbxD8.SelectedValuePath = LbxD10.SelectedValuePath = LbxD12.SelectedValuePath
        = LbxEncounterCreatures.SelectedValuePath = LbxContinent.SelectedValuePath = LbxCreatureInfo.SelectedValuePath = "Result";

      LbxEncounterCRs.DisplayMemberPath = "Display";
      LbxEncounterCRs.SelectedValuePath = "Values";

      LbxEncounterCreatures.ItemsSource = randomEncounterItems;
      LbxEncounterCRs.ItemsSource = encounterResults;
      LbxContinent.ItemsSource = continentList;
      LbxCreatureInfo.ItemsSource = creatureInfos;
    }

    #endregion

    #region Methods

    private bool LoadDBData()
    {
      var ret = false;

      if (DBClient.ConfigExists())
      {
        try
        {
          DBClient.ConnectToApi();

          continents = DBClient.GetContinents();
          seasons = DBClient.GetSeasons();
          months = DBClient.GetMonths();
          trackedEvents = DBClient.GetTrackedEvents();
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
        ContinentId = saveObject.ContinentId;
        CombatRound = saveObject.CombatRound;

        RationsLeft = saveObject.Rations;
        CurrentDate = saveObject.Date;
        CurrentWeather = saveObject.Weather;
        WeatherResult = saveObject.WeatherResult;

        combatEffectItems = new ObservableCollection<CombatEffectItem>(saveObject.CombatEffects);
        combatGridItems = new ObservableCollection<CombatGridItem>(saveObject.CombatGridItems);
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

        combatEffectItems = new ObservableCollection<CombatEffectItem>();
        combatGridItems = new ObservableCollection<CombatGridItem>();
      }
    }

    private void SaveData()
    {
      var saveObject = new SaveObject();

      if (int.TryParse(TxtAPL.Text, out int apl))
        saveObject.Apl = apl;

      saveObject.CbxGroup = EncounterGroup;
      saveObject.CbxNpc = EncounterNPC;
      saveObject.CbxTime = EncounterTime;
      saveObject.CbxZone = EncounterZone;
      saveObject.CbxInfRations = RationsInfinite;
      saveObject.ContinentId = ContinentId;
      saveObject.CombatRound = CombatRound;

      saveObject.Rations = RationsLeft;
      saveObject.Date = CurrentDate;
      saveObject.Weather = CurrentWeather;
      saveObject.WeatherResult = WeatherResult;

      saveObject.CombatEffects = combatEffectItems.ToList();
      saveObject.CombatGridItems = combatGridItems.ToList();

      File.WriteAllText(saveDataPath, Newtonsoft.Json.JsonConvert.SerializeObject(saveObject));
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
        var roll = random.Next(d + 1);
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
        TerrainId = EncounterZone ? TerrainId : 0,
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
        case 0:
          return "1/2";
        case -1:
          return "1/3";
        case -2:
          return "1/4";
        case -3:
          return "1/6";
        case -4:
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
        }
      }
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
      CurrentDate.AddDays(i);
      NextWeather(i);
      UpdateDate();
    }

    private void NextWeather(int d = 1)
    {
      if (WeatherResult == null || ContinentId != WeatherResult.ContinentId)
      {
        ReloadWeatherTable();
        CurrentWeatherGroup = GetRandomWeather();
      }
      else if (CurrentDate.Season != WeatherResult.SeasonId)
        ReloadWeatherTable();

      while (d >= 0)
      {
        if (CurrentWeatherGroup.Duration >= d)
        {
          CurrentWeatherGroup.Duration -= d;
          d = -1;
        }
        else
        {
          d -= CurrentWeatherGroup.Duration;
          if (CurrentWeatherGroup.NextContinentWeatherId > 0)
          {
            if (WeatherResult.WeatherList.Select(x => x.WeatherId).Contains(CurrentWeatherGroup.NextContinentWeatherId))
              CurrentWeatherGroup = WeatherResult.WeatherList.First(x => x.WeatherId == CurrentWeatherGroup.NextContinentWeatherId);
            else
              CurrentWeatherGroup = DBClient.GetContinentWeather(CurrentWeatherGroup.NextContinentWeatherId);
          }
          else
            CurrentWeatherGroup = GetRandomWeather();
        }
      }

      CurrentWeather = DBClient.GetWeather(CurrentWeatherGroup.WeatherId);
    }

    private ContinentWeather GetRandomWeather()
    {
      var initialWeathers = WeatherResult.WeatherList.Where(x => x.ParentWeatherId == 0);
      var weather = initialWeathers.ElementAt(random.Next(initialWeathers.Count()));
      weather.Duration = weather.Duration - random.Next(weather.Duration);

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

    private void UpdateDate()
    {
      LblGrandDate.Content = $"YEAR {CurrentDate.Year} AA, Season of {seasons[CurrentDate.Season - 1].Name}, Month of {months[CurrentDate.Month - 1].Name}, Day {CurrentDate.Day}";

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

    private void ReloadWeatherTable()
    {
      var reqWeather = new RandomWeatherRequest()
      {
        ContinentId = ContinentId,
        SeasonId = CurrentDate.Season
      };

      WeatherResult = DBClient.GetRandomWeatherList(reqWeather);
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
      combatGridItems = new ObservableCollection<CombatGridItem>(combatGridItems.OrderBy(x => x.Init));
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
      SaveData();
    }

    private void CommandSave_Executed(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
    {
      SaveData();
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
        if (selectedCombatMonsterId != selectedItem.Result)
        {
          selectedCombatMonsterId = selectedItem.Result;
          var desc = DBClient.GetBestiaryDetail(selectedCombatMonsterId);
          if (desc != null)
            selectedCombatMonsterHtml = desc.FullText;
          else
            selectedCombatMonsterHtml = "<html><body><h1>NOT FOUND</h1></body></html>";

          BrowserCreature.NavigateToString(selectedCombatMonsterHtml);
        }
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

      if (DBClient.ConfigExists())
        DBClient.ReloadConfig(true);

      if (!LoadDBData())
      {
        MessageBox.Show("ERROR - Failed to connect to server. Is the config set correctly?");
      }
    }

    #endregion
  }
}
