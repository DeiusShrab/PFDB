using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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

      var config = PFConfig.GetAllConfigValues();
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

      PFConfig.UpdateConfigValues(config);

      MessageBox.Show("Remember to copy the new config file over to the server and restart it");
    }
  }
}
