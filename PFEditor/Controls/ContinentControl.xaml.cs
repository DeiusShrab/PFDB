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
  }
}
