using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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

    private ObservableCollection<ListItemResult> MonsterSpawnList;
    private ObservableCollection<ListItemResult> ContinentList;
    private ObservableCollection<ListItemResult> SeasonList;
    private ObservableCollection<ListItemResult> TimeList;
    private ObservableCollection<ListItemResult> MonsterSpawnList_Filter;

    private Bictionary<int, string> BiContinents;
    private Bictionary<int, string> BiSeasons;
    private Bictionary<int, string> BiTimes;

    private DataSet DsMonsterSpawns;
    private int MonsterSpawnBestiaryId;
    private bool sortNameAsc = true;
    private bool sortCrAsc = true;

    public MonsterSpawnControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      MonsterSpawnList = new ObservableCollection<ListItemResult>();
      ContinentList = new ObservableCollection<ListItemResult>();
      SeasonList = new ObservableCollection<ListItemResult>();
      TimeList = new ObservableCollection<ListItemResult>();
      MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>();

      LbxSpawnList.DisplayMemberPath = "Name";
      LbxSpawnList.SelectedValuePath = "Id";

      LbxSpawnList.ItemsSource = MonsterSpawnList_Filter;
    }


    private void LoadMonsterSpawnData()
    {
      BiContinents = new Bictionary<int, string>();
      foreach (var item in ContinentList)
      {
        BiContinents.Add(item.Id, item.Name);
      }
      BiSeasons = new Bictionary<int, string>();
      foreach (var item in SeasonList)
      {
        BiSeasons.Add(item.Id, item.Name);
      }
      BiTimes = new Bictionary<int, string>();
      foreach (var item in TimeList)
      {
        BiTimes.Add(item.Id, item.Name);
      }

      var spawnList = DBClient.GetMonsterSpawns(MonsterSpawnBestiaryId);

      DsMonsterSpawns = new DataSet();

      foreach (var season in SeasonList)
      {
        var dt = new DataTable(season.Name);

        dt.Columns.Add("Continent");
        foreach (var time in TimeList)
        {
          dt.Columns.Add(time.Name, typeof(bool));
        }
        foreach (var continent in ContinentList)
        {
          var r = dt.NewRow();
          r[0] = continent.Name;

          for (int i = 1; i < r.ItemArray.Length; i++)
            r[i] = false;

          dt.Rows.Add(r);
        }

        DsMonsterSpawns.Tables.Add(dt);
      }

      foreach (var item in spawnList)
      {
        // Table (Season) -> Row (Continent) -> Column (Time)
        DsMonsterSpawns.Tables[BiSeasons[item.SeasonId]].Select($"Continent = '{BiContinents[item.ContinentId]}'")[0].SetField(BiTimes[item.TimeId], true);
      }

      // Bind table to grid(s)
      // https://stackoverflow.com/questions/20770438/how-to-bind-datatable-to-datagrid
      TabsMonsterSpawn.Items.Clear();
      foreach (DataTable table in DsMonsterSpawns.Tables)
      {
        var tab = new TabItem
        {
          Header = table.TableName,
          Content = new DataGrid()
          {
            DataContext = table.DefaultView,
            ItemsSource = table.DefaultView
          }
        };
        TabsMonsterSpawn.Items.Add(tab);
      }
    }

    private List<MonsterSpawn> ExportMonsterSpawnData()
    {
      var ret = new List<MonsterSpawn>();

      foreach (DataTable table in DsMonsterSpawns.Tables)
      {
        var sid = BiSeasons[table.TableName];
        foreach (DataRow row in table.Rows)
        {
          var cid = BiContinents[(string)row[0]];
          for (int i = 1; i < table.Columns.Count; i++)
          {
            if ((bool?)row[i] == true)
            {
              ret.Add(new MonsterSpawn() { BestiaryId = (int)LbxSpawnList.SelectedValue, ContinentId = cid, SeasonId = sid, TimeId = BiTimes[table.Columns[i].ColumnName] });
            }
          }
        }
      }

      return ret;
    }


    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (MonsterSpawnList != null) // Prevent designer failure
      {
        if (string.IsNullOrWhiteSpace(TxtSearch.Text))
        {
          MonsterSpawnList_Filter.Clear();
          MonsterSpawnList_Filter.AddRange(MonsterSpawnList);
        }
        else if (TxtSearch.Text.Length >= 3)
        {
          if (RadSearchName.IsChecked == true)
          {
            MonsterSpawnList_Filter.Clear();
            MonsterSpawnList_Filter.AddRange(MonsterSpawnList.Where(x => x.Name.ToLower().Contains(TxtSearch.Text.ToLower())));
          }
          else
          {
            MonsterSpawnList_Filter.Clear();
            MonsterSpawnList_Filter.AddRange(MonsterSpawnList.Where(x => x.Notes.ToLower().Contains(TxtSearch.Text.ToLower())));
          }
        }
      }
    }

    private void BtnSortName_Click(object sender, RoutedEventArgs e)
    {
      if (sortNameAsc)
      {
        var temp = MonsterSpawnList_Filter.OrderBy(x => x.Name);
        MonsterSpawnList_Filter.Clear();
        MonsterSpawnList_Filter.AddRange(temp);
      }
      else
      {
        var temp = MonsterSpawnList_Filter.OrderByDescending(x => x.Name);
        MonsterSpawnList_Filter.Clear();
        MonsterSpawnList_Filter.AddRange(temp);
      }

      sortNameAsc = !sortNameAsc;
      sortCrAsc = true;
    }

    private void LbxSpawnList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxSpawnList.SelectedItem != null)
      {
        MonsterSpawnBestiaryId = (int)LbxSpawnList.SelectedValue;
        LoadMonsterSpawnData();
      }
    }

    private void BtnSortCr_Click(object sender, RoutedEventArgs e)
    {
      if (sortCrAsc)
      {
        var temp = MonsterSpawnList_Filter.OrderBy(x => x.Id);
        MonsterSpawnList_Filter.Clear();
        MonsterSpawnList_Filter.AddRange(temp);
      }
      else
      {
        var temp = MonsterSpawnList_Filter.OrderByDescending(x => x.Id);
        MonsterSpawnList_Filter.Clear();
        MonsterSpawnList_Filter.AddRange(temp);
      }

      sortCrAsc = !sortCrAsc;
      sortNameAsc = true;
    }

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        // Reload lists

        MonsterSpawnList.Clear();
        ContinentList.Clear();
        SeasonList.Clear();
        TimeList.Clear();
        MonsterSpawnList_Filter.Clear();

        MonsterSpawnList.AddRange(DBClient.GetList("MonsterSpawn"));
        ContinentList.AddRange(DBClient.GetList("Continent").OrderBy(x => x.Name));
        SeasonList.AddRange(DBClient.GetList("Season").OrderBy(x => x.Name));
        TimeList.AddRange(DBClient.GetList("Time").OrderBy(x => x.Name));
        MonsterSpawnList_Filter.AddRange(MonsterSpawnList);

        e.Handled = true;
      }
    }
  }
}
