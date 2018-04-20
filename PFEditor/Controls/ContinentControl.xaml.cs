using System.Windows.Controls;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for ContinentControl.xaml
  /// </summary>
  public partial class ContinentControl : UserControl
  {
    public ContinentControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;
    }

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
