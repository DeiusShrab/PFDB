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

namespace CommonUI
{
  /// <summary>
  /// Interaction logic for WeatherPopUp.xaml
  /// </summary>
  public partial class ListPopUp : Window
  {
    private enum ListType
    {
      LIR,
      DR,
    }

    private string SearchText
    {
      get
      {
        return TxtSearch.Text;
      }
    }

    private ObservableCollection<DisplayResult> ObservableDR;
    private ObservableCollection<ListItemResult> ObservableLIR;
    private List<DisplayResult> ListDR;
    private List<ListItemResult> ListLIR;
    private ListType Type;
    private bool suppress;

    public int SelectedResult
    {
      get
      {
        if (LbxList.SelectedValue != null)
          return (int)LbxList.SelectedValue;

        return -1;
      }
    }

    public ListPopUp(IEnumerable<ListItemResult> listContents, string message = "")
    {
      suppress = true;
      InitializeComponent();

      Type = ListType.LIR;

      ObservableLIR = new ObservableCollection<ListItemResult>();
      ObservableLIR.AddRange(listContents);
      ListLIR = new List<ListItemResult>();
      ListLIR.AddRange(listContents);

      LbxList.DisplayMemberPath = "Name";
      LbxList.SelectedValuePath = "Id";

      LbxList.ItemsSource = ObservableLIR;

      if (!string.IsNullOrWhiteSpace(message))
        LblMessage.Content = message;

      suppress = false;
    }

    public ListPopUp(IEnumerable<DisplayResult> listContents, string message = "")
    {
      suppress = true;
      InitializeComponent();

      Type = ListType.DR;

      ObservableDR = new ObservableCollection<DisplayResult>();
      ObservableDR.AddRange(listContents);
      ListDR = new List<DisplayResult>();
      ListDR.AddRange(listContents);

      LbxList.DisplayMemberPath = "Display";
      LbxList.SelectedValuePath = "Result";

      LbxList.ItemsSource = ObservableDR;

      if (!string.IsNullOrWhiteSpace(message))
        LblMessage.Content = message;

      suppress = false;
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

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (suppress)
        return;

      if (string.IsNullOrWhiteSpace(SearchText))
      {
        if (Type == ListType.DR)
        {
          ObservableDR.Clear();
          ObservableDR.AddRange(ListDR);
        }
        else if (Type == ListType.LIR)
        {
          ObservableLIR.Clear();
          ObservableLIR.AddRange(ListLIR);
        }
      }
      else if (SearchText.Length > 0)
      {
        if (Type == ListType.DR)
        {
          ObservableDR.Clear();
          ObservableDR.AddRange(ListDR.Where(x => x.Display.ToLower().Contains(SearchText.ToLower())));
        }
        else if (Type == ListType.LIR)
        {
          ObservableLIR.Clear();
          ObservableLIR.AddRange(ListLIR.Where(x => x.Name.ToLower().Contains(SearchText.ToLower())));
        }
      }
    }
  }
}
