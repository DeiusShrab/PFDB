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
using System.Windows.Shapes;
using DBConnect;
using DBConnect.ConnectModels;

namespace PFHelper.Controls
{
  /// <summary>
  /// Interaction logic for ConfigEditor.xaml
  /// </summary>
  public partial class ConfigEditor : Window
  {
    public ObservableCollection<ListItemResult> ConfigValues => configValues;
    private ObservableCollection<ListItemResult> configValues;

    public ConfigEditor()
    {
      configValues = new ObservableCollection<ListItemResult>();
      this.DataContext = this;
      InitializeComponent();

      var config = DBClient.GetAllConfigValues();
      foreach (var key in config.Keys)
      {
        configValues.Add(new ListItemResult() { Name = key, Notes = config[key] });
      }
    }

    private void Btn_Clear_Click(object sender, RoutedEventArgs e)
    {
      configValues.Clear();
    }

    private void Btn_Exit_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
      var config = new Dictionary<string, string>();

      foreach (var item in configValues)
      {
        config.Add(item.Name, item.Notes);
      }

      DBClient.UpdateConfigValues(config);

      MessageBox.Show("Remember to copy the new config file over to the server and restart it");
    }
  }
}
