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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using PFEditor.Classes;

namespace PFEditor
{
  /// <summary>
  /// Interaction logic for EditorWindow.xaml
  /// </summary>
  public partial class EditorWindow : Window
  {
    private List<ListItemResult> Bestiary;
    private List<ListItemResult> BestiaryDetail;
    private List<ListItemResult> BestiaryEnvironment;
    private List<ListItemResult> BestiaryFeat;
    private List<ListItemResult> BestiaryLanguage;
    private List<ListItemResult> BestiarySkill;
    private List<ListItemResult> BestiarySubType;
    private List<ListItemResult> BestiaryType;
    private List<ListItemResult> Continent;
    private List<ListItemResult> ContinentWeather;
    private List<ListItemResult> Environment;
    private List<ListItemResult> Faction;
    private List<ListItemResult> Feat;
    private new List<ListItemResult> Language;
    private List<ListItemResult> Location;
    private List<ListItemResult> MagicItem;
    private List<ListItemResult> MonsterSpawn;
    private List<ListItemResult> Month;
    private List<ListItemResult> Season;
    private List<ListItemResult> Spell;
    private List<ListItemResult> SpellDetail;
    private List<ListItemResult> Territory;
    private List<ListItemResult> Weather;
    private List<ListItemResult> Terrain;
    private List<ListItemResult> Time;
    private List<ListItemResult> TrackedEvent;
    private List<ListItemResult> Plane;
    private List<ListItemResult> Skill;

    public EditorWindow()
    {
      InitializeComponent();

      LoadDBData();
    }

    private void LoadDBData()
    {
      try
      {
        Bestiary = DBClient.GetList("Bestiary");
        BestiaryDetail = DBClient.GetList("BestiaryDetail");
        BestiaryEnvironment = DBClient.GetList("BestiaryEnvironment");
        BestiaryFeat = DBClient.GetList("BestiaryFeat");
        BestiaryLanguage = DBClient.GetList("BestiaryLanguage");
        BestiarySkill = DBClient.GetList("BestiarySkill");
        BestiarySubType = DBClient.GetList("BestiarySubType");
        BestiaryType = DBClient.GetList("BestiaryType");
        Continent = DBClient.GetList("Continent");
        ContinentWeather = DBClient.GetList("ContinentWeather");
        Environment = DBClient.GetList("Environment");
        Faction = DBClient.GetList("Faction");
        Feat = DBClient.GetList("Feat");
        Language = DBClient.GetList("Language");
        Location = DBClient.GetList("Location");
        MagicItem = DBClient.GetList("MagicItem");
        MonsterSpawn = DBClient.GetList("MonsterSpawn");
        Month = DBClient.GetList("Month");
        Season = DBClient.GetList("Season");
        Spell = DBClient.GetList("Spell");
        SpellDetail = DBClient.GetList("SpellDetail");
        Territory = DBClient.GetList("Territory");
        Weather = DBClient.GetList("Weather");
        Terrain = DBClient.GetList("Terrain");
        Time = DBClient.GetList("Time");
        TrackedEvent = DBClient.GetList("TrackedEvent");
        Plane = DBClient.GetList("Plane");
        Skill = DBClient.GetList("Skill");
      }
      catch (System.Exception ex)
      {
        MessageBox.Show("Unable to load DB data. Error:\n" + ex.Message);
      }
    }
  }
}
