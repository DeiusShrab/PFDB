using System.Windows.Controls;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for LocationControl.xaml
  /// </summary>
  public partial class LocationControl : UserControl
  {
    public LocationControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;
    }
  }
}
