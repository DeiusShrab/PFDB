using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for MiscControl.xaml
  /// </summary>
  public partial class MiscControl : UserControl
  {
    #region Interface Properties

    public string MonthName
    {
      get { return TxtMonthName.Text; }
      set { TxtMonthName.Text = value; }
    }

    public string PlaneName
    {
      get { return TxtPlaneName.Text; }
      set { TxtPlaneName.Text = value; }
    }

    public string SeasonName
    {
      get { return TxtSeasonName.Text; }
      set { TxtSeasonName.Text = value; }
    }

    public string TimeName
    {
      get { return TxtTimeName.Text; }
      set { TxtTimeName.Text = value; }
    }

    public string TerrainName
    {
      get { return TxtTerrainName.Text; }
      set { TxtTerrainName.Text = value; }
    }

    public string EnvironmentName
    {
      get { return TxtEnvironmentName.Text; }
      set { TxtEnvironmentName.Text = value; }
    }

    public string TerrainDescription
    {
      get { return TxtTerrainDescription.Text; }
      set { TxtTerrainDescription.Text = value; }
    }

    public string EnvironmentTemp
    {
      get { return TxtEnvironmentTemp.Text; }
      set { TxtEnvironmentTemp.Text = value; }
    }

    public string EnvironmentNotes
    {
      get { return TxtEnvironmentNotes.Text; }
      set { TxtEnvironmentNotes.Text = value; }
    }

    public int MonthDays
    {
      get { return IntMonthDays.Value ?? 0; }
      set { IntMonthDays.Value = value; }
    }

    public int MonthOrder
    {
      get { return IntMonthOrder.Value ?? 0; }
      set { IntMonthOrder.Value = value; }
    }

    public int SeasonOrder
    {
      get { return IntSeasonOrder.Value ?? 0; }
      set { IntSeasonOrder.Value = value; }
    }

    public int TimeOrder
    {
      get { return IntTimeOrder.Value ?? 0; }
      set { IntTimeOrder.Value = value; }
    }

    public int MonthSeasonId
    {
      get { return Convert.ToInt32(DrpMonthSeasonId.SelectedValue); }
      set { DrpMonthSeasonId.SelectedValue = value; }
    }

    public decimal TerrainMovementMod
    {
      get { return DecTerrainMovementMod.Value ?? 0; }
      set { DecTerrainMovementMod.Value = value; }
    }

    public int EnvironmentTravelSpeed
    {
      get { return IntEnvironmentTravelSpeed.Value ?? 0; }
      set { IntEnvironmentTravelSpeed.Value = value; }
    }

    public bool TimeNight
    {
      get { return CbxTimeNight.IsChecked ?? false; }
      set { CbxTimeNight.IsChecked = value; }
    }

    public bool TerrainWater
    {
      get { return CbxTerrainWater.IsChecked ?? false; }
      set { CbxTerrainWater.IsChecked = value; }
    }

    public bool TerrainUnderground
    {
      get { return CbxTerrainUnderground.IsChecked ?? false; }
      set { CbxTerrainUnderground.IsChecked = value; }
    }

    public bool TerrainRough
    {
      get { return CbxTerrainRough.IsChecked ?? false; }
      set { CbxTerrainRough.IsChecked = value; }
    }

    public bool TerrainBroken
    {
      get { return CbxTerrainBroken.IsChecked ?? false; }
      set { CbxTerrainBroken.IsChecked = value; }
    }

    public int MonthId
    {
      get { return Convert.ToInt32(LblMonthId.Content); }
      set { LblMonthId.Content = value; }
    }

    public int PlaneId
    {
      get { return Convert.ToInt32(LblPlaneId.Content); }
      set { LblPlaneId.Content = value; }
    }

    public int SeasonId
    {
      get { return Convert.ToInt32(LblSeasonId.Content); }
      set { LblSeasonId.Content = value; }
    }

    public int TimeId
    {
      get { return Convert.ToInt32(LblTimeId.Content); }
      set { LblTimeId.Content = value; }
    }

    public int TerrainId
    {
      get { return Convert.ToInt32(LblTerrainId.Content); }
      set { LblTerrainId.Content = value; }
    }

    public int EnvironmentId
    {
      get { return Convert.ToInt32(LblEnvironmentId.Content); }
      set { LblEnvironmentId.Content = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> MonthList;
    private ObservableCollection<ListItemResult> SeasonList;
    private ObservableCollection<ListItemResult> TimeList;
    private ObservableCollection<ListItemResult> PlaneList;
    private ObservableCollection<ListItemResult> EnvironmentList;
    private ObservableCollection<ListItemResult> TerrainList;

    private Month ActiveMonth = new Month();
    private Plane ActivePlane = new Plane();
    private Season ActiveSeason = new Season();
    private Time ActiveTime = new Time();

    #endregion

    public MiscControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
      TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
      PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));
      TerrainList = new ObservableCollection<ListItemResult>(DBClient.GetList("Terrain"));
      EnvironmentList = new ObservableCollection<ListItemResult>(DBClient.GetList("Environment"));

      LbxMonth.DisplayMemberPath = LbxPlane.DisplayMemberPath = LbxSeason.DisplayMemberPath =
        LbxTime.DisplayMemberPath = LbxTerrain.DisplayMemberPath = LbxEnvironment.DisplayMemberPath = "Name";

      LbxMonth.SelectedValuePath = LbxPlane.SelectedValuePath = LbxSeason.SelectedValuePath =
        LbxTime.SelectedValuePath = LbxTerrain.SelectedValuePath = LbxEnvironment.SelectedValuePath = "Id";

      LbxMonth.ItemsSource = MonthList;
      LbxPlane.ItemsSource = PlaneList;
      LbxSeason.ItemsSource = SeasonList;
      LbxTime.ItemsSource = TimeList;
      LbxTerrain.ItemsSource = TerrainList;
      LbxEnvironment.ItemsSource = EnvironmentList;

      DrpMonthSeasonId.ItemsSource = SeasonList;
    }



    #region Events

    private void BtnTimeAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveTime = new Time();
      TimeId = 0;
      TimeName = string.Empty;
      TimeNight = false;
      TimeOrder = 0;
    }

    private void BtnSeasonAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveSeason = new Season();
      SeasonId = 0;
      SeasonName = string.Empty;
      SeasonOrder = 0;
    }

    private void BtnPlaneAdd_Click(object sender, RoutedEventArgs e)
    {
      ActivePlane = new Plane();
      PlaneId = 0;
      PlaneName = string.Empty;
    }

    private void BtnMonthAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveMonth = new Month();
      MonthDays = 0;
      MonthId = 0;
      MonthName = string.Empty;
      MonthOrder = 0;
      MonthSeasonId = 0;
    }

    private void LbxTime_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxTime.SelectedItem != null)
      {
        ActiveTime = DBClient.GetTime((int)LbxTime.SelectedValue);
        TimeId = ActiveTime.TimeId;
        TimeName = ActiveTime.Name;
        TimeNight = ActiveTime.IsNight;
        TimeOrder = ActiveTime.TimeOrder;
      }
    }

    private void LbxSeason_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxSeason.SelectedItem != null)
      {
        ActiveSeason = DBClient.GetSeason((int)LbxSeason.SelectedValue);
        SeasonId = ActiveSeason.SeasonId;
        SeasonName = ActiveSeason.Name;
        SeasonOrder = ActiveSeason.Order;
      }
    }

    private void LbxPlane_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxPlane.SelectedItem != null)
      {
        ActivePlane = DBClient.GetPlane((int)LbxPlane.SelectedValue);
        PlaneId = ActivePlane.PlaneId;
        PlaneName = ActivePlane.Name;
      }
    }

    private void LbxMonth_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxMonth.SelectedItem != null)
      {
        ActiveMonth = DBClient.GetMonth((int)LbxMonth.SelectedValue);
        MonthDays = ActiveMonth.Days;
        MonthId = ActiveMonth.MonthId;
        MonthName = ActiveMonth.Name;
        MonthOrder = ActiveMonth.Order;
        MonthSeasonId = ActiveMonth.SeasonId;
      }
    }

    private void BtnTimeSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveTime.TimeId == 0)
        ActiveTime.TimeId = DBClient.CreateTime(ActiveTime);
      else
        DBClient.UpdateTime(ActiveTime);

      TimeId = ActiveTime.TimeId;
      TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
    }

    private void BtnSeasonSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveSeason.SeasonId == 0)
        ActiveSeason.SeasonId = DBClient.CreateSeason(ActiveSeason);
      else
        DBClient.UpdateSeason(ActiveSeason);

      SeasonId = ActiveSeason.SeasonId;
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
    }

    private void BtnPlaneSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActivePlane.PlaneId == 0)
        ActivePlane.PlaneId = DBClient.CreatePlane(ActivePlane);
      else
        DBClient.UpdatePlane(ActivePlane);

      PlaneId = ActivePlane.PlaneId;
      PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));
    }

    private void BtnMonthSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveMonth.MonthId == 0)
        ActiveMonth.MonthId = DBClient.CreateMonth(ActiveMonth);
      else
        DBClient.UpdateMonth(ActiveMonth);

      MonthId = ActiveMonth.MonthId;
      MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
    }


    #endregion
  }
}
