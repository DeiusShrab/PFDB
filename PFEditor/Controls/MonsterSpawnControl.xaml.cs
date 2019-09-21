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
    #region Interface Variables

    public int SelectedContinent
    {
      get {
        if (DrpContinent.SelectedIndex >= 0)
          return (int)DrpContinent.SelectedValue;

        return 0;
      }
      set
      {
        if (ContinentList.Select(x => x.Id).Contains(value))
          DrpContinent.SelectedValue = value;
        else
          DrpContinent.SelectedIndex = -1;
      }
    }

    public int SelectedSeason
    {
      get
      {
        if (DrpSeason.SelectedIndex >= 0)
          return (int)DrpSeason.SelectedValue;

        return 0;
      }
      set
      {
        if (SeasonList.Select(x => x.Id).Contains(value))
          DrpSeason.SelectedValue = value;
        else
          DrpSeason.SelectedIndex = -1;
      }
    }

    #endregion

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
    private bool suppress = false;

    private const int ALL_ID = int.MinValue;
    private int COL_CONTINENT;
    private int COL_SEASON;

    public MonsterSpawnControl()
    {
      suppress = true;

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

      suppress = false;
    }

    private void BuildGrid()
    {
      DgMonsterSpawn.Columns.Clear();
      var validHeaders = new List<object>();

      DgMonsterSpawn.Columns.Add(new DataGridTextColumn {
        Header = "Name (CR) | Type (Subtypes)",
        Binding = new Binding("DisplayName"),
        MinWidth = 360
      });

      foreach (var item in TimeList)
      {
        DgMonsterSpawn.Columns.Add(new DataGridCheckBoxColumn
        {
          Header = item.Name,
          Binding = new Binding($"Times[{item.Id}]")
        });
      }

      DgMonsterSpawn.Columns.Add(new DataGridTextColumn
      {
        Header = "Continent",
        Binding = new Binding("ContinentId")
      });

      DgMonsterSpawn.Columns.Add(new DataGridTextColumn
      {
        Header = "Season",
        Binding = new Binding("SeasonId")
      });

      foreach (var column in DgMonsterSpawn.Columns)
      {
        validHeaders.Add(column.Header);
      }

      DgMonsterSpawn.ItemsSource = MonsterSpawnRows_Filter;

      foreach (var column in DgMonsterSpawn.Columns)
      {
        if (!validHeaders.Contains(column.Header))
          column.Visibility = Visibility.Collapsed;
      }
    }

    private void PopulateSpawnRows()
    {
      MonsterSpawnRows.Clear();
      MonsterSpawnRows_Filter.Clear();
      var timeList = TimeList.ToList();

      foreach (var item in MonsterSpawnList.Where(x => 
                            (SelectedContinent == ALL_ID || x.ContinentId == 0 || x.ContinentId == SelectedContinent)
                            && (SelectedSeason == ALL_ID || x.SeasonId == 0 || x.SeasonId == SelectedSeason)))
      {
        var row = MonsterSpawnRows.FirstOrDefault(x => x.BestiaryId == item.BestiaryId && x.ContinentId == item.ContinentId && x.SeasonId == item.SeasonId);
        if (row == null)
        {
          row = new MonsterSpawnRow(timeList)
          {
            DisplayName = $"{item.Name} ({Extensions.CRToString(item.CR)}) | {item.Type}",
            BestiaryId = item.BestiaryId,
            ContinentId = item.ContinentId,
            SeasonId = item.SeasonId,
            CR = item.CR,
            Name = item.Name,
            Type = item.Type
          };
          MonsterSpawnRows.Add(row);
        }

        row.Times[item.TimeId] = true;
      }

      MonsterSpawnRows_Filter.AddRange(MonsterSpawnRows);
    }

    private void ReloadData()
    {
      if (isDirty && MessageBox.Show("WARNING\nUnsaved data, are you sure you want to reload?", "WARNING", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
      {
        return;
      }

      suppress = true;

      var list = new List<ListItemResult>(TimeList);

      MonsterSpawnRows.Clear();
      MonsterSpawnRows_Filter.Clear();
      MonsterSpawnList.Clear();
      ContinentList.Clear();
      SeasonList.Clear();
      TimeList.Clear();

      ContinentList.Add(new ListItemResult { Id = ALL_ID, Name = "ALL" });
      SeasonList.Add(new ListItemResult { Id = ALL_ID, Name = "ALL" });
      TimeList.Add(new ListItemResult { Id = ALL_ID, Name = "ALL" });

      MonsterSpawnList.AddRange(DBClient.GetMonsterSpawnsForEdit());
      //MonsterSpawnList.Add(new MonsterSpawnEdit { BestiaryId = 1, ContinentId = 1, CR = 1, Name = "TEST", PlaneId = 1, SeasonId = 1, SpawnId = 1, TimeId = 1, Type = "TEST" });
      ContinentList.AddRange(DBClient.GetList("Continent").OrderBy(x => x.Name).Select(x => new ListItemResult { Id = x.Id, Name = $"{x.Name} ({x.Id})", Notes = x.Notes }));
      SeasonList.AddRange(DBClient.GetList("Season").OrderBy(x => x.Name).Select(x => new ListItemResult { Id = x.Id, Name = $"{x.Name} ({x.Id})", Notes = x.Notes }));
      TimeList.AddRange(DBClient.GetList("Time").OrderBy(x => x.Name));

      var alterGrid = TimeList.Count != list.Count;
      int i = 0;

      while (!alterGrid && i < list.Count)
      {
        if (list[i]?.Id != TimeList[i]?.Id || list[i]?.Name != TimeList[i]?.Name || list[i]?.Notes != TimeList[i]?.Notes)
          alterGrid = true;

        i++;
      }

      if (alterGrid)
        BuildGrid();

      TxtSearch.Text = string.Empty;
      DrpContinent.SelectedValue = ALL_ID;
      DrpSeason.SelectedValue = ALL_ID;

      PopulateSpawnRows();

      suppress = false;
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (suppress)
        return;

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
      if (suppress)
        return;

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
      if (suppress)
        return;

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
      if (suppress)
        return;

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
      if (suppress)
        return;

      if (e.Key == System.Windows.Input.Key.F5)
      {
        ReloadData();

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
      public DrWPF.Windows.Data.ObservableDictionary<int, bool> Times { get; }

      public MonsterSpawnRow(List<ListItemResult> _times)
      {
        Times = new DrWPF.Windows.Data.ObservableDictionary<int, bool>();
        foreach (var item in _times)
        {
          Times.Add(item.Id, false);
        }
      }
    }

    private void DrpContinent_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (suppress)
        return;

      PopulateSpawnRows();

      if (SelectedContinent == ALL_ID)
        DgMonsterSpawn.Columns.First(x => (string)x.Header == "Continent").Visibility = Visibility.Visible;
    }

    private void DrpSeason_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (suppress)
        return;

      PopulateSpawnRows();

      if (SelectedSeason == ALL_ID)
        DgMonsterSpawn.Columns.First(x => (string)x.Header == "Season").Visibility = Visibility.Visible;
    }

    private void BtnReloadData_Click(object sender, RoutedEventArgs e)
    {
      if (suppress)
        return;

      ReloadData();
    }
  }
}
