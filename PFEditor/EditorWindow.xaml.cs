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
    #region Interface Variables

    #region Bestiary
    public string Bes_ACMods
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_ACMods; }
      set { Ctl_Bes_BestiaryAbilities.Bes_ACMods = value; }
    }

    public string Bes_Aura
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Aura; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Aura = value; }
    }

    public string Bes_Defense
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Defense; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Defense = value; }
    }

    public string Bes_Immune
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Immune; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Immune = value; }
    }

    public string Bes_Languages
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Languages; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Languages = value; }
    }

    public string Bes_Offense
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Offense; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Offense = value; }
    }

    public string Bes_OffenseNote
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_OffenseNote; }
      set { Ctl_Bes_BestiaryAbilities.Bes_OffenseNote = value; }
    }

    public string Bes_RacialMods
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_RacialMods; }
      set { Ctl_Bes_BestiaryAbilities.Bes_RacialMods = value; }
    }

    public string Bes_Reach
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Reach; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Reach = value; }
    }

    public string Bes_Resist
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Resist; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Resist = value; }
    }

    public string Bes_SaveMods
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_SaveMods; }
      set { Ctl_Bes_BestiaryAbilities.Bes_SaveMods = value; }
    }

    public string Bes_Senses
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Senses; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Senses = value; }
    }

    public string Bes_Space
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_Space; }
      set { Ctl_Bes_BestiaryAbilities.Bes_Space = value; }
    }

    public string Bes_SpecialAbilities
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_SpecialAbilities; }
      set { Ctl_Bes_BestiaryAbilities.Bes_SpecialAbilities = value; }
    }

    public string Bes_SpecialQualities
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_SpecialQualities; }
      set { Ctl_Bes_BestiaryAbilities.Bes_SpecialQualities = value; }
    }

    public string Bes_SpeedMods
    {
      get { return Ctl_Bes_BestiaryAbilities.Bes_SpeedMods; }
      set { Ctl_Bes_BestiaryAbilities.Bes_SpeedMods = value; }
    }

    public string Bes_AC
    {
      get { return Txt_Bes_AC.Text; }
      set { Txt_Bes_AC.Text = value; }
    }

    public string Bes_ACFlat
    {
      get { return Txt_Bes_ACFlat.Text; }
      set { Txt_Bes_ACFlat.Text = value; }
    }

    public string Bes_ACTouch
    {
      get { return Txt_Bes_ACTouch.Text; }
      set { Txt_Bes_ACTouch.Text = value; }
    }

    public string Bes_AgeCategory
    {
      get { return Txt_Bes_AgeCategory.Text; }
      set { Txt_Bes_AgeCategory.Text = value; }
    }

    public string Bes_Align
    {
      get { return Txt_Bes_Align.Text; }
      set { Txt_Bes_Align.Text = value; }
    }

    public string Bes_Archetype
    {
      get { return Txt_Bes_Archetype.Text; }
      set { Txt_Bes_Archetype.Text = value; }
    }

    public string Bes_BAB
    {
      get { return Txt_Bes_BAB.Text; }
      set { Txt_Bes_BAB.Text = value; }
    }

    public string Bes_BaseStatistics
    {
      get { return Txt_Bes_BaseStatistics.Text; }
      set { Txt_Bes_BaseStatistics.Text = value; }
    }

    public string Bes_BeforeCombat
    {
      get { return Txt_Bes_BeforeCombat.Text; }
      set { Txt_Bes_BeforeCombat.Text = value; }
    }

    public string Bes_Bloodline
    {
      get { return Txt_Bes_Bloodline.Text; }
      set { Txt_Bes_Bloodline.Text = value; }
    }

    public string Bes_CHA
    {
      get { return Txt_Bes_CHA.Text; }
      set { Txt_Bes_CHA.Text = value; }
    }

    public string Bes_Class
    {
      get { return Txt_Bes_Class.Text; }
      set { Txt_Bes_Class.Text = value; }
    }

    public string Bes_CMB
    {
      get { return Txt_Bes_CMB.Text; }
      set { Txt_Bes_CMB.Text = value; }
    }

    public string Bes_CMD
    {
      get { return Txt_Bes_CMD.Text; }
      set { Txt_Bes_CMD.Text = value; }
    }

    public string Bes_CON
    {
      get { return Txt_Bes_CON.Text; }
      set { Txt_Bes_CON.Text = value; }
    }

    public string Bes_CR
    {
      get { return Txt_Bes_CR.Text; }
      set { Txt_Bes_CR.Text = value; }
    }

    public string Bes_CreatureSource
    {
      get { return Txt_Bes_CreatureSource.Text; }
      set { Txt_Bes_CreatureSource.Text = value; }
    }

    public string Bes_DEX
    {
      get { return Txt_Bes_DEX.Text; }
      set { Txt_Bes_DEX.Text = value; }
    }

    public string Bes_DuringCombat
    {
      get { return Txt_Bes_DuringCombat.Text; }
      set { Txt_Bes_DuringCombat.Text = value; }
    }

    public string Bes_FamiliarNotes
    {
      get { return Txt_Bes_FamiliarNotes.Text; }
      set { Txt_Bes_FamiliarNotes.Text = value; }
    }

    public string Bes_FeatNotes
    {
      get { return Txt_Bes_FeatNotes.Text; }
      set { Txt_Bes_FeatNotes.Text = value; }
    }

    public string Bes_FeatSearch
    {
      get { return Txt_Bes_FeatSearch.Text; }
      set { Txt_Bes_FeatSearch.Text = value; }
    }

    public string Bes_FocusedSchool
    {
      get { return Txt_Bes_FocusedSchool.Text; }
      set { Txt_Bes_FocusedSchool.Text = value; }
    }

    public string Bes_Fort
    {
      get { return Txt_Bes_Fort.Text; }
      set { Txt_Bes_Fort.Text = value; }
    }

    public string Bes_Fulltext
    {
      get { return Txt_Bes_Fulltext.Text; }
      set { Txt_Bes_Fulltext.Text = value; }
    }

    public string Bes_Gear
    {
      get { return Txt_Bes_Gear.Text; }
      set { Txt_Bes_Gear.Text = value; }
    }

    public string Bes_Gender
    {
      get { return Txt_Bes_Gender.Text; }
      set { Txt_Bes_Gender.Text = value; }
    }

    public string Bes_Group
    {
      get { return Txt_Bes_Group.Text; }
      set { Txt_Bes_Group.Text = value; }
    }

    public string Bes_HD
    {
      get { return Txt_Bes_HD.Text; }
      set { Txt_Bes_HD.Text = value; }
    }

    public string Bes_HP
    {
      get { return Txt_Bes_HP.Text; }
      set { Txt_Bes_HP.Text = value; }
    }

    public string Bes_Init
    {
      get { return Txt_Bes_Init.Text; }
      set { Txt_Bes_Init.Text = value; }
    }

    public string Bes_INT
    {
      get { return Txt_Bes_INT.Text; }
      set { Txt_Bes_INT.Text = value; }
    }

    public string Bes_Melee
    {
      get { return Txt_Bes_Melee.Text; }
      set { Txt_Bes_Melee.Text = value; }
    }

    public string Bes_Morale
    {
      get { return Txt_Bes_Morale.Text; }
      set { Txt_Bes_Morale.Text = value; }
    }

    public string Bes_Mystery
    {
      get { return Txt_Bes_Mystery.Text; }
      set { Txt_Bes_Mystery.Text = value; }
    }

    public string Bes_Name
    {
      get { return Txt_Bes_Name.Text; }
      set { Txt_Bes_Name.Text = value; }
    }

    public string Bes_Notes
    {
      get { return Txt_Bes_Notes.Text; }
      set { Txt_Bes_Notes.Text = value; }
    }

    public string Bes_Organization
    {
      get { return Txt_Bes_Organization.Text; }
      set { Txt_Bes_Organization.Text = value; }
    }

    public string Bes_Patron
    {
      get { return Txt_Bes_Patron.Text; }
      set { Txt_Bes_Patron.Text = value; }
    }

    public string Bes_ProhibitedSchools
    {
      get { return Txt_Bes_ProhibitedSchools.Text; }
      set { Txt_Bes_ProhibitedSchools.Text = value; }
    }

    public string Bes_Race
    {
      get { return Txt_Bes_Race.Text; }
      set { Txt_Bes_Race.Text = value; }
    }

    public string Bes_Ranged
    {
      get { return Txt_Bes_Ranged.Text; }
      set { Txt_Bes_Ranged.Text = value; }
    }

    public string Bes_Ref
    {
      get { return Txt_Bes_Ref.Text; }
      set { Txt_Bes_Ref.Text = value; }
    }

    public string Bes_Search
    {
      get { return Txt_Bes_Search.Text; }
      set { Txt_Bes_Search.Text = value; }
    }

    public string Bes_Size
    {
      get { return Txt_Bes_Size.Text; }
      set { Txt_Bes_Size.Text = value; }
    }

    public string Bes_SkillNotes
    {
      get { return Txt_Bes_SkillNotes.Text; }
      set { Txt_Bes_SkillNotes.Text = value; }
    }

    public string Bes_SkillSearch
    {
      get { return Txt_Bes_SkillSearch.Text; }
      set { Txt_Bes_SkillSearch.Text = value; }
    }

    public string Bes_SpdBurrow
    {
      get { return Txt_Bes_SpdBurrow.Text; }
      set { Txt_Bes_SpdBurrow.Text = value; }
    }

    public string Bes_SpdClimb
    {
      get { return Txt_Bes_SpdClimb.Text; }
      set { Txt_Bes_SpdClimb.Text = value; }
    }

    public string Bes_SpdFly
    {
      get { return Txt_Bes_SpdFly.Text; }
      set { Txt_Bes_SpdFly.Text = value; }
    }

    public string Bes_SpdLand
    {
      get { return Txt_Bes_SpdLand.Text; }
      set { Txt_Bes_SpdLand.Text = value; }
    }

    public string Bes_SpdSwim
    {
      get { return Txt_Bes_SpdSwim.Text; }
      set { Txt_Bes_SpdSwim.Text = value; }
    }

    public string Bes_Special
    {
      get { return Txt_Bes_Special.Text; }
      set { Txt_Bes_Special.Text = value; }
    }

    public string Bes_SpellDomains
    {
      get { return Txt_Bes_SpellDomains.Text; }
      set { Txt_Bes_SpellDomains.Text = value; }
    }

    public string Bes_STR
    {
      get { return Txt_Bes_STR.Text; }
      set { Txt_Bes_STR.Text = value; }
    }

    public string Bes_SubType
    {
      get { return Txt_Bes_SubType.Text; }
      set { Txt_Bes_SubType.Text = value; }
    }

    public string Bes_TemplatesApplied
    {
      get { return Txt_Bes_TemplatesApplied.Text; }
      set { Txt_Bes_TemplatesApplied.Text = value; }
    }

    public string Bes_Traits
    {
      get { return Txt_Bes_Traits.Text; }
      set { Txt_Bes_Traits.Text = value; }
    }

    public string Bes_Treasure
    {
      get { return Txt_Bes_Treasure.Text; }
      set { Txt_Bes_Treasure.Text = value; }
    }

    public string Bes_VariantParent
    {
      get { return Txt_Bes_VariantParent.Text; }
      set { Txt_Bes_VariantParent.Text = value; }
    }

    public string Bes_VisualDescription
    {
      get { return Txt_Bes_VisualDescription.Text; }
      set { Txt_Bes_VisualDescription.Text = value; }
    }

    public string Bes_Will
    {
      get { return Txt_Bes_Will.Text; }
      set { Txt_Bes_Will.Text = value; }
    }

    public string Bes_WIS
    {
      get { return Txt_Bes_WIS.Text; }
      set { Txt_Bes_WIS.Text = value; }
    }

    public string Bes_XP
    {
      get { return Txt_Bes_XP.Text; }
      set { Txt_Bes_XP.Text = value; }
    }
    #endregion

    #endregion

    #region Private Variables

    private List<ListItemResult> BestiaryList;
    private List<ListItemResult> BestiaryDetailList;
    private List<ListItemResult> BestiaryEnvironmentList;
    private List<ListItemResult> BestiaryFeatList;
    private List<ListItemResult> BestiaryLanguageList;
    private List<ListItemResult> BestiarySkillList;
    private List<ListItemResult> BestiarySubTypeList;
    private List<ListItemResult> BestiaryTypeList;
    private List<ListItemResult> ContinentList;
    private List<ListItemResult> ContinentWeatherList;
    private List<ListItemResult> EnvironmentList;
    private List<ListItemResult> FactionList;
    private List<ListItemResult> FeatList;
    private List<ListItemResult> LanguageList;
    private List<ListItemResult> LocationList;
    private List<ListItemResult> MagicItemList;
    private List<ListItemResult> MonsterSpawnList;
    private List<ListItemResult> MonthList;
    private List<ListItemResult> SeasonList;
    private List<ListItemResult> SpellList;
    private List<ListItemResult> SpellDetailList;
    private List<ListItemResult> TerritoryList;
    private List<ListItemResult> WeatherList;
    private List<ListItemResult> TerrainList;
    private List<ListItemResult> TimeList;
    private List<ListItemResult> TrackedEventList;
    private List<ListItemResult> PlaneList;
    private List<ListItemResult> SkillList;

    private Bestiary ActiveBestiary = null;
    private BestiaryDetail ActiveBestiaryDetail = null;
    private BestiaryEnvironment ActiveBestiaryEnvironment = null;
    private BestiaryFeat ActiveBestiaryFeat = null;
    private BestiaryLanguage ActiveBestiaryLanguage = null;
    private BestiarySkill ActiveBestiarySkill = null;
    private BestiarySubType ActiveBestiarySubType = null;
    private BestiaryType ActiveBestiaryType = null;
    private Continent ActiveContinent = null;
    private ContinentWeather ActiveContinentWeather = null;
    private Environment ActiveEnvironment = null;
    private Faction ActiveFaction = null;
    private Feat ActiveFeat = null;
    private Language ActiveLanguage = null;
    private Location ActiveLocation = null;
    private MagicItem ActiveMagicItem = null;
    private MonsterSpawn ActiveMonsterSpawn = null;
    private Month ActiveMonth = null;
    private Plane ActivePlane = null;
    private Season ActiveSeason = null;
    private Skill ActiveSkill = null;
    private Spell ActiveSpell = null;
    private SpellDetail ActiveSpellDetail = null;
    private Terrain ActiveTerrain = null;
    private Territory ActiveTerritory = null;
    private Time ActiveTime = null;
    private TrackedEvent ActiveTrackedEvent = null;
    private Weather ActiveWeather = null;

    #endregion

    public EditorWindow()
    {
      InitializeComponent();

      LoadDBData();
    }

    private void LoadDBData()
    {
      try
      {
        BestiaryList = DBClient.GetList("Bestiary");
        BestiaryDetailList = DBClient.GetList("BestiaryDetail");
        BestiaryEnvironmentList = DBClient.GetList("BestiaryEnvironment");
        BestiaryFeatList = DBClient.GetList("BestiaryFeat");
        BestiaryLanguageList = DBClient.GetList("BestiaryLanguage");
        BestiarySkillList = DBClient.GetList("BestiarySkill");
        BestiarySubTypeList = DBClient.GetList("BestiarySubType");
        BestiaryTypeList = DBClient.GetList("BestiaryType");
        ContinentList = DBClient.GetList("Continent");
        ContinentWeatherList = DBClient.GetList("ContinentWeather");
        EnvironmentList = DBClient.GetList("Environment");
        FactionList = DBClient.GetList("Faction");
        FeatList = DBClient.GetList("Feat");
        LanguageList = DBClient.GetList("Language");
        LocationList = DBClient.GetList("Location");
        MagicItemList = DBClient.GetList("MagicItem");
        MonsterSpawnList = DBClient.GetList("MonsterSpawn");
        MonthList = DBClient.GetList("Month");
        SeasonList = DBClient.GetList("Season");
        SpellList = DBClient.GetList("Spell");
        SpellDetailList = DBClient.GetList("SpellDetail");
        TerritoryList = DBClient.GetList("Territory");
        WeatherList = DBClient.GetList("Weather");
        TerrainList = DBClient.GetList("Terrain");
        TimeList = DBClient.GetList("Time");
        TrackedEventList = DBClient.GetList("TrackedEvent");
        PlaneList = DBClient.GetList("Plane");
        SkillList = DBClient.GetList("Skill");
      }
      catch (System.Exception ex)
      {
        MessageBox.Show("Unable to load DB data. Error:\n" + ex.Message);
      }
    }
  }
}
