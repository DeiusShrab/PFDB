using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    #region Interface Variables

    public string Msc_MonthName
    {
      get { return TxtMonthName.Text; }
      set { TxtMonthName.Text = value; }
    }

    public string Msc_PlaneName
    {
      get { return TxtPlaneName.Text; }
      set { TxtPlaneName.Text = value; }
    }

    public string Msc_SeasonName
    {
      get { return TxtSeasonName.Text; }
      set { TxtSeasonName.Text = value; }
    }

    public string Msc_TimeName
    {
      get { return TxtTimeName.Text; }
      set { TxtTimeName.Text = value; }
    }

    public int Msc_MonthDays
    {
      get { return IntMonthDays.Value ?? 0; }
      set { IntMonthDays.Value = value; }
    }

    public int Msc_MonthOrder
    {
      get { return IntMonthOrder.Value ?? 0; }
      set { IntMonthOrder.Value = value; }
    }

    public int Msc_SeasonOrder
    {
      get { return IntSeasonOrder.Value ?? 0; }
      set { IntSeasonOrder.Value = value; }
    }

    public int Msc_TimeOrder
    {
      get { return IntTimeOrder.Value ?? 0; }
      set { IntTimeOrder.Value = value; }
    }

    public int Msc_MonthSeasonId
    {
      get { return Convert.ToInt32(DrpMonthSeasonId.SelectedValue); }
      set { DrpMonthSeasonId.SelectedValue = value; }
    }

    public bool Msc_TimeNight
    {
      get { return CbxTimeNight.IsChecked ?? false; }
      set { CbxTimeNight.IsChecked = value; }
    }

    public int Msc_MonthId
    {
      get { return Convert.ToInt32(LblMonthId.Content); }
      set { LblMonthId.Content = value; }
    }

    public int Msc_PlaneId
    {
      get { return Convert.ToInt32(LblPlaneId.Content); }
      set { LblPlaneId.Content = value; }
    }

    public int Msc_SeasonId
    {
      get { return Convert.ToInt32(LblSeasonId.Content); }
      set { LblSeasonId.Content = value; }
    }

    public int Msc_TimeId
    {
      get { return Convert.ToInt32(LblTimeId.Content); }
      set { LblTimeId.Content = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> MonthList;
    private ObservableCollection<ListItemResult> SeasonList;
    private ObservableCollection<ListItemResult> TimeList;
    private ObservableCollection<ListItemResult> PlaneList;

    private Month ActiveMonth = new Month();
    private Plane ActivePlane = new Plane();
    private Season ActiveSeason = new Season();
    private Time ActiveTime = new Time();

    #endregion

    public MiscControl()
    {
      InitializeComponent();

      MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
      TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
      PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));

      LbxMonth.DisplayMemberPath = LbxPlane.DisplayMemberPath = LbxSeason.DisplayMemberPath =
        LbxTime.DisplayMemberPath = "Name";

      LbxMonth.SelectedValuePath = LbxPlane.SelectedValuePath = LbxSeason.SelectedValuePath =
        LbxTime.SelectedValuePath = "Id";

      LbxMonth.ItemsSource = MonthList;
      LbxPlane.ItemsSource = PlaneList;
      LbxSeason.ItemsSource = SeasonList;
      LbxTime.ItemsSource = TimeList;

      DrpMonthSeasonId.ItemsSource = SeasonList;
    }



    #region Events

    private void Btn_Misc_TimeAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveTime = new Time();
      Msc_TimeId = 0;
      Msc_TimeName = string.Empty;
      Msc_TimeNight = false;
      Msc_TimeOrder = 0;
    }

    private void Btn_Misc_SeasonAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveSeason = new Season();
      Msc_SeasonId = 0;
      Msc_SeasonName = string.Empty;
      Msc_SeasonOrder = 0;
    }

    private void Btn_Misc_PlaneAdd_Click(object sender, RoutedEventArgs e)
    {
      ActivePlane = new Plane();
      Msc_PlaneId = 0;
      Msc_PlaneName = string.Empty;
    }

    private void Btn_Misc_MonthAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveMonth = new Month();
      Msc_MonthDays = 0;
      Msc_MonthId = 0;
      Msc_MonthName = string.Empty;
      Msc_MonthOrder = 0;
      Msc_MonthSeasonId = 0;
    }

    private void LbxTime_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxTime.SelectedItem != null)
      {
        ActiveTime = DBClient.GetTime((int)LbxTime.SelectedValue);
        Msc_TimeId = ActiveTime.TimeId;
        Msc_TimeName = ActiveTime.Name;
        Msc_TimeNight = ActiveTime.IsNight;
        Msc_TimeOrder = ActiveTime.TimeOrder;
      }
    }

    private void LbxSeason_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxSeason.SelectedItem != null)
      {
        ActiveSeason = DBClient.GetSeason((int)LbxSeason.SelectedValue);
        Msc_SeasonId = ActiveSeason.SeasonId;
        Msc_SeasonName = ActiveSeason.Name;
        Msc_SeasonOrder = ActiveSeason.Order;
      }
    }

    private void LbxPlane_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxPlane.SelectedItem != null)
      {
        ActivePlane = DBClient.GetPlane((int)LbxPlane.SelectedValue);
        Msc_PlaneId = ActivePlane.PlaneId;
        Msc_PlaneName = ActivePlane.Name;
      }
    }

    private void LbxMonth_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxMonth.SelectedItem != null)
      {
        ActiveMonth = DBClient.GetMonth((int)LbxMonth.SelectedValue);
        Msc_MonthDays = ActiveMonth.Days;
        Msc_MonthId = ActiveMonth.MonthId;
        Msc_MonthName = ActiveMonth.Name;
        Msc_MonthOrder = ActiveMonth.Order;
        Msc_MonthSeasonId = ActiveMonth.SeasonId;
      }
    }

    private void Btn_Misc_TimeSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveTime.TimeId == 0)
        ActiveTime.TimeId = DBClient.CreateTime(ActiveTime);
      else
        DBClient.UpdateTime(ActiveTime);

      Msc_TimeId = ActiveTime.TimeId;
      TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
    }

    private void Btn_Misc_SeasonSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveSeason.SeasonId == 0)
        ActiveSeason.SeasonId = DBClient.CreateSeason(ActiveSeason);
      else
        DBClient.UpdateSeason(ActiveSeason);

      Msc_SeasonId = ActiveSeason.SeasonId;
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
    }

    private void Btn_Misc_PlaneSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActivePlane.PlaneId == 0)
        ActivePlane.PlaneId = DBClient.CreatePlane(ActivePlane);
      else
        DBClient.UpdatePlane(ActivePlane);

      Msc_PlaneId = ActivePlane.PlaneId;
      PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));
    }

    private void Btn_Misc_MonthSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveMonth.MonthId == 0)
        ActiveMonth.MonthId = DBClient.CreateMonth(ActiveMonth);
      else
        DBClient.UpdateMonth(ActiveMonth);

      Msc_MonthId = ActiveMonth.MonthId;
      MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
    }


    #endregion
  }
}
