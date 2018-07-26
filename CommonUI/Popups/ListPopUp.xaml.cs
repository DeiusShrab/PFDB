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
    private string SearchText
    {
      get
      {
        return TxtSearch.Text;
      }
    }

    private ObservableCollection<DisplayResult> _observable;
    private List<DisplayResult> _list;
    private bool _suppress;

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
      _suppress = true;
      InitializeComponent();

      var newContents = new List<DisplayResult>();
      foreach (var item in listContents)
      {
        newContents.Add(new DisplayResult { Display = item.Name, Result = item.Id });
      }

      Init(newContents, message);

      _suppress = false;
    }

    public ListPopUp(IEnumerable<DisplayResult> listContents, string message = "")
    {
      _suppress = true;
      InitializeComponent();

      Init(listContents, message);

      _suppress = false;
    }

    private void Init(IEnumerable<DisplayResult> listContents, string message)
    {
      _observable = new ObservableCollection<DisplayResult>();
      _observable.AddRange(listContents);
      _list = new List<DisplayResult>();
      _list.AddRange(listContents);

      LbxList.DisplayMemberPath = "Display";
      LbxList.SelectedValuePath = "Result";

      LbxList.ItemsSource = _observable;

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

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (_suppress)
        return;

      if (string.IsNullOrWhiteSpace(SearchText))
      {
        _observable.Clear();
        _observable.AddRange(_list);
      }
      else if (SearchText.Length > 0)
      {
        _observable.Clear();
        _observable.AddRange(_list.Where(x => x.Display.ToLower().Contains(SearchText.ToLower())));
      }
    }
  }
}
