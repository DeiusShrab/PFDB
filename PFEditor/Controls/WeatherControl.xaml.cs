using System.Collections.ObjectModel;
using System.Windows.Controls;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for WeatherControl.xaml
  /// </summary>
  public partial class WeatherControl : UserControl
  {
    #region Interface Properties


    public int ContinentId
    {
      get
      {
        int.TryParse(DrpContinent.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpContinent.SelectedValue = value; }
    }

    public int NextWeatherId
    {
      get
      {
        int.TryParse(DrpNextWeather.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpNextWeather.SelectedValue = value; }
    }

    public int ParentWeatherId
    {
      get
      {
        int.TryParse(DrpParentWeather.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpParentWeather.SelectedValue = value; }
    }

    public int SeasonId
    {
      get
      {
        int.TryParse(DrpSeason.SelectedValue.ToString(), out int ret);
        return ret;
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

    public string GroupPhase
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

    public string CWID
    {
      get { return LblCWID.Content.ToString(); }
      set { LblCWID.Content = value; }
    }

    public string WeatherId
    {
      get { return LblWeatherId.Content.ToString(); }
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



    #endregion

    private ObservableCollection<ListItemResult> WeatherList;
    private ObservableCollection<ListItemResult> ContinentWeatherList;
    private ObservableCollection<ListItemResult> ContinentList;
    private ObservableCollection<ListItemResult> SeasonList;

    private Weather ActiveWeather = new Weather();
    private ContinentWeather ActiveContinentWeather = new ContinentWeather();

    public WeatherControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      WeatherList = new ObservableCollection<ListItemResult>(DBClient.GetList("Weather"));
      ContinentList = new ObservableCollection<ListItemResult>(DBClient.GetList("Continent"));
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
      ContinentWeatherList = new ObservableCollection<ListItemResult>();

      LbxWeather.DisplayMemberPath = LbxContinentWeather.DisplayMemberPath = DrpContinent.DisplayMemberPath = DrpNextWeather.DisplayMemberPath = 
        DrpParentWeather.DisplayMemberPath = DrpSeason.DisplayMemberPath = DrpWeather.DisplayMemberPath = "Name";
      LbxWeather.SelectedValuePath = LbxContinentWeather.SelectedValuePath = DrpContinent.SelectedValuePath = DrpNextWeather.SelectedValuePath =
        DrpParentWeather.SelectedValuePath = DrpSeason.SelectedValuePath = DrpWeather.SelectedValuePath = "Id";

      LbxWeather.ItemsSource = WeatherList;
      LbxContinentWeather.ItemsSource = ContinentWeatherList;
      DrpContinent.ItemsSource = ContinentList;
      DrpNextWeather.ItemsSource = ContinentWeatherList;
      DrpParentWeather.ItemsSource = ContinentWeatherList;
      DrpSeason.ItemsSource = SeasonList;
      DrpWeather.ItemsSource = WeatherList;
      
    }
  }
}
