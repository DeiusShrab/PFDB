using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DBConnect;
using PFDBCommon;
using PFDBCommon.ConnectModels;
using PFDBCommon.DBModels;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for WeatherControl.xaml
  /// </summary>
  public partial class WeatherControl : UserControl
  {
    #region Interface Properties

    public int? ContinentId
    {
      get
      {
        if (DrpContinent.SelectedValue != null)
          return (int)DrpContinent.SelectedValue;

        return null;
      }
      set { DrpContinent.SelectedValue = value; }
    }

    public int? NextCWID
    {
      get
      {
        if (DrpNextWeather.SelectedValue != null)
          return (int)DrpNextWeather.SelectedValue;

        return null;
      }
      set { DrpNextWeather.SelectedValue = value; }
    }

    public int? ParentCWID
    {
      get
      {
        if (DrpParentWeather.SelectedValue != null)
          return (int)DrpParentWeather.SelectedValue;

        return null;
      }
      set { DrpParentWeather.SelectedValue = value; }
    }

    public int? SeasonId
    {
      get
      {
        if (DrpSeason.SelectedValue != null)
          return (int)DrpSeason.SelectedValue;

        return null;
      }
      set { DrpSeason.SelectedValue = value; }
    }

    public int CW_WeatherId
    {
      get
      {
        int.TryParse(DrpWeather.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpWeather.SelectedValue = value; }
    }

    public string Description
    {
      get { return TxtDescription.Text; }
      set { TxtDescription.Text = value; }
    }

    public string Effects
    {
      get { return TxtEffects.Text; }
      set { TxtEffects.Text = value; }
    }

    public string CWName
    {
      get { return TxtGroupPhase.Text; }
      set { TxtGroupPhase.Text = value; }
    }

    public string WeatherName
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }

    public string SearchContinentWeather
    {
      get { return TxtSearchContinentWeather.Text; }
      set { TxtSearchContinentWeather.Text = value; }
    }

    public string SearchWeather
    {
      get { return TxtSearchWeather.Text; }
      set { TxtSearchWeather.Text = value; }
    }

    public int Duration
    {
      get { return IntDuration.Value ?? 0; }
      set { IntDuration.Value = value; }
    }

    public int Weight
    {
      get { return IntWeight.Value ?? 0; }
      set { IntWeight.Value = value; }
    }

    public int CWID
    {
      get { return Convert.ToInt32(LblCWID.Content.ToString()); }
      set { LblCWID.Content = value; }
    }

    public int WeatherId
    {
      get { return Convert.ToInt32(LblWeatherId.Content.ToString()); }
      set { LblWeatherId.Content = value; }
    }

    public string TotalWeight
    {
      get { return LblWeight.Content.ToString(); }
      set { LblWeight.Content = value; }
    }
    public bool ColdDanger
    {
      get { return CbxColdDanger.IsChecked ?? false; }
      set { CbxColdDanger.IsChecked = value; }
    }

    public bool ColdLethal
    {
      get { return CbxColdLethal.IsChecked ?? false; }
      set { CbxColdLethal.IsChecked = value; }
    }

    public bool Deadly
    {
      get { return CbxDeadly.IsChecked ?? false; }
      set { CbxDeadly.IsChecked = value; }
    }

    public bool FloodDanger
    {
      get { return CbxFloodDanger.IsChecked ?? false; }
      set { CbxFloodDanger.IsChecked = value; }
    }

    public bool HeatDanger
    {
      get { return CbxHeatDanger.IsChecked ?? false; }
      set { CbxHeatDanger.IsChecked = value; }
    }

    public bool HeatLethal
    {
      get { return CbxHeatLethal.IsChecked ?? false; }
      set { CbxHeatLethal.IsChecked = value; }
    }

    public bool Magical
    {
      get { return CbxMagical.IsChecked ?? false; }
      set { CbxMagical.IsChecked = value; }
    }

    public bool VisionObscured
    {
      get { return CbxVisionObscured.IsChecked ?? false; }
      set { CbxVisionObscured.IsChecked = value; }
    }

    public bool WindDanger
    {
      get { return CbxWindDanger.IsChecked ?? false; }
      set { CbxWindDanger.IsChecked = value; }
    }

    public bool RandomDuration
    {
      get { return CbxRandomDuration.IsChecked ?? false; }
      set { CbxRandomDuration.IsChecked = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> WeatherList;
    private ObservableCollection<ContinentWeather> ContinentWeatherList;
    private ObservableCollection<ListItemResult> ContinentList;
    private ObservableCollection<ListItemResult> SeasonList;

    private Weather ActiveWeather = new Weather();
    private ContinentWeather ActiveContinentWeather = new ContinentWeather();

    private bool isDirty = false;

    #endregion

    #region Constructor

    public WeatherControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      WeatherList = new ObservableCollection<ListItemResult>();
      ContinentList = new ObservableCollection<ListItemResult>();
      SeasonList = new ObservableCollection<ListItemResult>();
      ContinentWeatherList = new ObservableCollection<ContinentWeather>();

      LbxWeather.DisplayMemberPath = DrpContinent.DisplayMemberPath = DrpNextWeather.DisplayMemberPath = DrpParentWeather.DisplayMemberPath =
        DrpSeason.DisplayMemberPath = DrpWeather.DisplayMemberPath = "Name";
      LbxWeather.SelectedValuePath = DrpContinent.SelectedValuePath = DrpNextWeather.SelectedValuePath = DrpParentWeather.SelectedValuePath =
        DrpSeason.SelectedValuePath = DrpWeather.SelectedValuePath = "Id";

      LbxContinentWeather.DisplayMemberPath = "Name";
      LbxContinentWeather.SelectedValuePath = "CWID";

      LbxWeather.ItemsSource = WeatherList;
      LbxContinentWeather.ItemsSource = ContinentWeatherList;
      DrpContinent.ItemsSource = ContinentList;
      DrpNextWeather.ItemsSource = ContinentWeatherList;
      DrpParentWeather.ItemsSource = ContinentWeatherList;
      DrpSeason.ItemsSource = SeasonList;
      DrpWeather.ItemsSource = WeatherList;
    }

    #endregion

    #region Private Methods

    private void LoadActiveWeather()
    {
      if (ActiveWeather == null)
        return;

      WeatherId = ActiveWeather.WeatherId;
      WeatherName = ActiveWeather.Name;
      ColdDanger = ActiveWeather.ColdDanger;
      ColdLethal = ActiveWeather.ColdLethal;
      Deadly = ActiveWeather.Deadly;
      Description = ActiveWeather.Description;
      Effects = ActiveWeather.Effects;
      FloodDanger = ActiveWeather.Flooding;
      HeatDanger = ActiveWeather.HeatDanger;
      HeatLethal = ActiveWeather.HeatLethal;
      WindDanger = ActiveWeather.HighWind;
      Magical = ActiveWeather.Magical;
      VisionObscured = ActiveWeather.VisionObscured;
    }

    private void SaveActiveWeather()
    {
      ActiveWeather.WeatherId = WeatherId;
      ActiveWeather.Name = WeatherName;
      ActiveWeather.ColdDanger = ColdDanger;
      ActiveWeather.ColdLethal = ColdLethal;
      ActiveWeather.Deadly = Deadly;
      ActiveWeather.Description = Description;
      ActiveWeather.Effects = Effects;
      ActiveWeather.Flooding = FloodDanger;
      ActiveWeather.HeatDanger = HeatDanger;
      ActiveWeather.HeatLethal = HeatLethal;
      ActiveWeather.HighWind = WindDanger;
      ActiveWeather.Magical = Magical;
      ActiveWeather.VisionObscured = VisionObscured;
    }

    private void LoadActiveContinentWeather()
    {
      if (ActiveContinentWeather == null)
        return;

      ContinentId = ActiveContinentWeather.ContinentId;
      CWID = ActiveContinentWeather.CWID;
      Duration = ActiveContinentWeather.Duration;
      CWName = ActiveContinentWeather.CWName;
      NextCWID = ActiveContinentWeather.NextCWID;
      ParentCWID = ActiveContinentWeather.ParentCWID;
      RandomDuration = ActiveContinentWeather.RandomDuration;
      SeasonId = ActiveContinentWeather.SeasonId;
      CW_WeatherId = ActiveContinentWeather.WeatherId;
      Weight = ActiveContinentWeather.Weight;
    }

    private void SaveActiveContinentWeather()
    {
      if (ContinentId > 0 && SeasonId > 0)
      {
        ActiveContinentWeather.ContinentId = ContinentId.Value;
        ActiveContinentWeather.CWID = CWID;
        ActiveContinentWeather.Duration = Duration;
        ActiveContinentWeather.CWName = CWName;
        ActiveContinentWeather.NextCWID = NextCWID;
        ActiveContinentWeather.ParentCWID = ParentCWID;
        ActiveContinentWeather.RandomDuration = RandomDuration;
        ActiveContinentWeather.SeasonId = SeasonId.Value;
        ActiveContinentWeather.WeatherId = CW_WeatherId;
        ActiveContinentWeather.Weight = Weight;
      }
    }

    private void UpdateCWIDList()
    {
      if (ContinentId > 0 && SeasonId > 0)
      {
        ContinentWeatherList.Clear();
        ContinentWeatherList.AddRange(DBClient.GetContinentWeathers(ContinentId.Value, SeasonId.Value));
      }
    }

    #endregion

    #region Events

    private void LbxWeather_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxWeather.SelectedValue != null)
      {
        ActiveWeather = DBClient.GetWeather((int)LbxWeather.SelectedValue);

        LoadActiveWeather();
      }
    }

    private void BtnAddWeather_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      ActiveWeather = new Weather();

      LoadActiveWeather();
    }

    private void BtnDelWeather_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      if (LbxWeather.SelectedValue != null && MessageBox.Show("WARNING\nAre you sure you want to delete the selected Weather?") == MessageBoxResult.OK)
      {
        if (DBClient.DeleteWeather((int)LbxWeather.SelectedValue))
          WeatherList.Remove((ListItemResult)LbxWeather.SelectedItem);
      }
    }

    private void BtnSaveWeather_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(WeatherName))
      {
        MessageBox.Show("Weather to be saved must have a Name");
        return;
      }

      var saved = false;

      SaveActiveWeather();
      if (ActiveWeather.WeatherId == 0)
      {
        ActiveWeather = DBClient.CreateWeather(ActiveWeather);
        saved = ActiveWeather.WeatherId != 0;
      }
      else
        saved = DBClient.UpdateWeather(ActiveWeather);

      if (saved)
      {
        MessageBox.Show("Saved");
        WeatherList.Clear();
        WeatherList.AddRange(DBClient.GetList("Weather"));
        LoadActiveWeather();
      }
      else
        MessageBox.Show("ERROR - failed to save");
    }

    private void BtnAddContinentWeather_Click(object sender, RoutedEventArgs e)
    {
      ActiveContinentWeather = new ContinentWeather();

      LoadActiveContinentWeather();
    }

    private void BtnDelContinentWeather_Click(object sender, RoutedEventArgs e)
    {
      if (LbxContinentWeather.SelectedItem != null && MessageBox.Show("WARNING\nAre you sure you want to delete the selected ContinentWeather?") == MessageBoxResult.OK)
      {
        if (DBClient.DeleteContinentWeather((int)LbxContinentWeather.SelectedValue))
          ContinentWeatherList.Remove((ContinentWeather)LbxContinentWeather.SelectedValue);
      }
    }

    private void BtnSaveContinentWeather_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(CWName))
      {
        MessageBox.Show("ContinentWeather to be saved must have a Group");
        return;
      }

      if (!ContinentId.HasValue || !SeasonId.HasValue)
      {
        MessageBox.Show("Please select a Continent and Season");
        return;
      }

      var saved = false;

      SaveActiveContinentWeather();
      if (ActiveContinentWeather.CWID == 0)
      {
        ActiveContinentWeather = DBClient.CreateContinentWeather(ActiveContinentWeather);
        saved = ActiveContinentWeather.CWID != 0;
      }
      else
        saved = DBClient.UpdateContinentWeather(ActiveContinentWeather);

      if (saved)
        MessageBox.Show("Saved");
      else
        MessageBox.Show("ERROR - failed to save");
    }

    private void LbxContinentWeather_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
      if (LbxContinentWeather.SelectedItem != null)
      {
        ActiveContinentWeather = DBClient.GetContinentWeather((int)LbxContinentWeather.SelectedValue);

        LoadActiveContinentWeather();
      }
    }

    private void DrpContinent_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      UpdateCWIDList();
    }

    private void DrpSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      UpdateCWIDList();
    }

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        if (!isDirty || MessageBox.Show("WARNING - Unsaved data, proceed?", "WARNING", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
          WeatherList.Clear();
          ContinentList.Clear();
          SeasonList.Clear();
          ContinentWeatherList.Clear();

          WeatherList.AddRange(DBClient.GetList("Weather").OrderBy(x => x.Name));
          ContinentList.AddRange(DBClient.GetList("Continent").OrderBy(x => x.Name));
          SeasonList.AddRange(DBClient.GetList("Season").OrderBy(x => x.Name));

          if (ContinentId > 0 && SeasonId > 0)
            ContinentWeatherList.AddRange(DBClient.GetContinentWeathers(ContinentId.Value, SeasonId.Value));
        }

        e.Handled = true;
      }
    }
  }

  #endregion
}
