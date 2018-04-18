using System.Windows;
using System.Windows.Input;
using DBConnect;

namespace PFEditor
{
  /// <summary>
  /// Interaction logic for EditorWindow.xaml
  /// </summary>
  public partial class EditorWindow : Window
  {
    #region Constructor

    public EditorWindow()
    {
      if (!DBClient.ConfigExists())
      {
        MessageBox.Show("Not configured - please run configuration tool in PFHelper");
        Application.Current.Shutdown();
      }

      InitializeComponent();

      LoadDBData();      
    }

    private void LoadDBData()
    {
      try
      {
        DBClient.ConnectToApi();
      }
      catch (System.Exception ex)
      {
        MessageBox.Show("Unable to load DB data. Error:\n" + ex.Message);
      }
    }

    #endregion
  }
}
