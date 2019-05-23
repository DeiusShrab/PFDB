using System;
using System.Collections.Generic;
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

namespace CommonUI.Popups
{
  /// <summary>
  /// Interaction logic for InputPopUp.xaml
  /// </summary>
  public partial class InputPopUp : Window
  {
    public string TextResult
    {
      get
      {
        return TxtText.Text;
      }
      set
      {
        TxtText.Text = value;
      }
    }

    public InputPopUp()
    {
      InitializeComponent();
    }

    public InputPopUp(string text)
    {
      InitializeComponent();
      LblText.Content = text;
    }

    private void BtnOk_Click(object sender, RoutedEventArgs e)
    {
      this.DialogResult = true;
    }
  }
}
