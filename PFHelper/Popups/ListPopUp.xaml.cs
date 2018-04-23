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
using PFHelper.Classes;
using DBConnect;
using DBConnect.ConnectModels;

namespace PFHelper
{
  /// <summary>
  /// Interaction logic for WeatherPopUp.xaml
  /// </summary>
  public partial class ListPopUp : Window
  {
    private ObservableCollection<DisplayResult> ListContentsDR;
    private ObservableCollection<ListItemResult> ListContentsLIR;
    public int SelectedResult
    {
      get
      {
        if (DrpContent.SelectedValue != null)
          return (int)DrpContent.SelectedValue;

        return -1;
      }
    }

    public ListPopUp(IEnumerable<ListItemResult> listContents, string message = "")
    {
      InitializeComponent();

      ListContentsLIR = new ObservableCollection<ListItemResult>();
      ListContentsLIR.AddRange(listContents);

      DrpContent.DisplayMemberPath = "Name";
      DrpContent.SelectedValuePath = "Id";

      DrpContent.ItemsSource = ListContentsLIR;

      if (!string.IsNullOrWhiteSpace(message))
        LblMessage.Content = message;
    }

    public ListPopUp(IEnumerable<DisplayResult> listContents, string message = "")
    {
      InitializeComponent();

      ListContentsDR = new ObservableCollection<DisplayResult>();
      ListContentsDR.AddRange(listContents);

      DrpContent.DisplayMemberPath = "Display";
      DrpContent.SelectedValuePath = "Result";

      DrpContent.ItemsSource = ListContentsDR;

      if (!string.IsNullOrWhiteSpace(message))
        LblMessage.Content = message;
    }

    private void BtnCancel_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
      Close();
    }

    private void BtnOkay_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
      Close();
    }
  }
}
