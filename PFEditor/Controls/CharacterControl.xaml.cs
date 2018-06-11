using System.Collections.ObjectModel;
using System.Windows.Controls;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for CharacterControl.xaml
  /// </summary>
  public partial class CharacterControl : UserControl
  {
    #region Interface Variables

    #endregion

    #region Private Variables

    private ObservableCollection<DisplayResult> characterList;

    private Character activeCharacter;

    #endregion

    #region Constructor

    public CharacterControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;
    }

    #endregion

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        // Reload lists

        e.Handled = true;
      }
    }
  }
}
