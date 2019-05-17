using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for MonsterSpawnControl.xaml
  /// </summary>
  public partial class MonsterSpawnControl : UserControl
  {

    private ObservableCollection<MonsterSpawnEdit> MonsterSpawnList;
    private ObservableCollection<ListItemResult> ContinentList;
    private ObservableCollection<ListItemResult> SeasonList;
    private ObservableCollection<ListItemResult> TimeList;
    private ObservableCollection<MonsterSpawnRow> MonsterSpawnRows;
    private ObservableCollection<MonsterSpawnRow> MonsterSpawnRows_Filter;
    
    private bool sortNameAsc = true;
    private bool sortCrAsc = true;
    private bool sortTypeAsc = true;
    private bool isDirty = false;

    public MonsterSpawnControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      MonsterSpawnList = new ObservableCollection<MonsterSpawnEdit>();
      ContinentList = new ObservableCollection<ListItemResult>();
      SeasonList = new ObservableCollection<ListItemResult>();
      TimeList = new ObservableCollection<ListItemResult>();
      MonsterSpawnRows = new ObservableCollection<MonsterSpawnRow>();
      MonsterSpawnRows_Filter = new ObservableCollection<MonsterSpawnRow>();

      DrpContinent.DisplayMemberPath = DrpSeason.DisplayMemberPath = "Name";
      DrpContinent.SelectedValuePath = DrpSeason.SelectedValuePath = "Id";

      DrpContinent.ItemsSource = ContinentList;
      DrpSeason.ItemsSource = SeasonList;

      ReloadData();
    }

    private void BuildGrid()
    {
      DgMonsterSpawn.Columns.Clear();

      DgMonsterSpawn.Columns.Add(new DataGridTextColumn {
        Header = "Name (CR) | Type (Subtypes)",
        Binding = new Binding("DisplayName")
      });

      foreach (var item in TimeList)
      {
        DgMonsterSpawn.Columns.Add(new DataGridCheckBoxColumn
        {
          Header = item.Name,
          Binding = new Binding($"Times[{item.Id}]")
        });
      }
    }

    private void PopulateSpawnRows()
    {
      MonsterSpawnRows.Clear();
      MonsterSpawnRows_Filter.Clear();

      foreach (var item in MonsterSpawnList.Where(x => x.ContinentId == 0 && x.SeasonId == 0))
      {
        var row = MonsterSpawnRows.FirstOrDefault(x => x.BestiaryId == item.BestiaryId && x.ContinentId == item.ContinentId && x.SeasonId == item.SeasonId);
        if (row == null)
        {
          row = new MonsterSpawnRow();
          row.BestiaryId = item.BestiaryId;
          row.ContinentId = item.ContinentId;
          row.SeasonId = item.SeasonId;
          MonsterSpawnRows.Add(row);
        }

        row.Times[item.TimeId] = true;
      }
    }

    private void ReloadData()
    {
      var list = new List<ListItemResult>(TimeList);

      MonsterSpawnRows.Clear();
      MonsterSpawnRows_Filter.Clear();
      MonsterSpawnList.Clear();
      ContinentList.Clear();
      SeasonList.Clear();
      TimeList.Clear();

      MonsterSpawnList.AddRange(DBClient.GetMonsterSpawnsForEdit());
      ContinentList.AddRange(DBClient.GetList("Continent").OrderBy(x => x.Name));
      SeasonList.AddRange(DBClient.GetList("Season").OrderBy(x => x.Name));
      TimeList.AddRange(DBClient.GetList("Time").OrderBy(x => x.Name));

      var alterGrid = TimeList.Count != list.Count;
      int i = 0;

      while (!alterGrid && i < list.Count)
      {
        if (list[i] != TimeList[i])
          alterGrid = true;
      }

      if (alterGrid)
        BuildGrid();
      
      PopulateSpawnRows();
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (MonsterSpawnRows != null) // Prevent designer failure
      {
        if (string.IsNullOrWhiteSpace(TxtSearch.Text))
        {
          MonsterSpawnRows_Filter.Clear();
          MonsterSpawnRows_Filter.AddRange(MonsterSpawnRows);
        }
        else if (TxtSearch.Text.Length >= 3)
        {
          if (RadSearchName.IsChecked == true)
          {
            MonsterSpawnRows_Filter.Clear();
            MonsterSpawnRows_Filter.AddRange(MonsterSpawnRows.Where(x => x.Name.ToLower().Contains(TxtSearch.Text.ToLower())));
          }
          else
          {
            MonsterSpawnRows_Filter.Clear();
            MonsterSpawnRows_Filter.AddRange(MonsterSpawnRows.Where(x => x.Type.ToLower().Contains(TxtSearch.Text.ToLower())));
          }
        }
      }
    }

    private void BtnSortName_Click(object sender, RoutedEventArgs e)
    {
      if (sortNameAsc)
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderBy(x => x.Name));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }
      else
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderByDescending(x => x.Name));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }

      sortNameAsc = !sortNameAsc;
      sortCrAsc = true;
      sortTypeAsc = true;
    }

    private void BtnSortCr_Click(object sender, RoutedEventArgs e)
    {
      if (sortCrAsc)
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderBy(x => x.CR));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }
      else
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderByDescending(x => x.CR));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }

      sortCrAsc = !sortCrAsc;
      sortNameAsc = true;
      sortTypeAsc = true;
    }

    private void BtnSortType_Click(object sender, RoutedEventArgs e)
    {
      if (sortTypeAsc)
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderBy(x => x.Type));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }
      else
      {
        var temp = new List<MonsterSpawnRow>(MonsterSpawnRows_Filter.OrderByDescending(x => x.Type));
        MonsterSpawnRows_Filter.Clear();
        MonsterSpawnRows_Filter.AddRange(temp);
      }

      sortTypeAsc = !sortTypeAsc;
      sortNameAsc = true;
      sortCrAsc = true;
    }

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        if (!isDirty || MessageBox.Show("WARNING\nUnsaved data, are you sure you want to reload?", "WARNING", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
          ReloadData();
        }

        e.Handled = true;
      }
    }

    private class MonsterSpawnRow
    {
      public int BestiaryId { get; set; }
      public string DisplayName { get; set; }
      public string Name { get; set; }
      public string Type { get; set; }
      public int CR { get; set; }
      public int SeasonId { get; set; }
      public int ContinentId { get; set; }
      public DrWPF.Windows.Data.ObservableDictionary<int, bool> Times { get; set; } = new DrWPF.Windows.Data.ObservableDictionary<int, bool>();
    }
  }
}
