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

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for WeatherControl.xaml
  /// </summary>
  public partial class WeatherControl : UserControl
  {

    private ObservableCollection<ListItemResult> WeatherList;

    public WeatherControl()
    {
      InitializeComponent();

      WeatherList = new ObservableCollection<ListItemResult>(DBClient.GetList("Weather"));

      LbxWeather.DisplayMemberPath = "Name";
      LbxWeather.SelectedValuePath = "Id";

      LbxWeather.ItemsSource = WeatherList;
    }
  }
}
