using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
  /// Interaction logic for BestiaryControl.xaml
  /// </summary>
  public partial class BestiaryControl : UserControl
  {
    #region Interface Properties

    public string ACMods
    {
      get { return CtlBestiaryAbilities.ACMods; }
      set { CtlBestiaryAbilities.ACMods = value; }
    }

    public string Aura
    {
      get { return CtlBestiaryAbilities.Aura; }
      set { CtlBestiaryAbilities.Aura = value; }
    }

    public string Defense
    {
      get { return CtlBestiaryAbilities.Defense; }
      set { CtlBestiaryAbilities.Defense = value; }
    }

    public string Immune
    {
      get { return CtlBestiaryAbilities.Immune; }
      set { CtlBestiaryAbilities.Immune = value; }
    }

    public string Languages
    {
      get { return CtlBestiaryAbilities.Languages; }
      set { CtlBestiaryAbilities.Languages = value; }
    }

    public string Offense
    {
      get { return CtlBestiaryAbilities.Offense; }
      set { CtlBestiaryAbilities.Offense = value; }
    }

    public string OffenseNote
    {
      get { return CtlBestiaryAbilities.OffenseNote; }
      set { CtlBestiaryAbilities.OffenseNote = value; }
    }

    public string RacialMods
    {
      get { return CtlBestiaryAbilities.RacialMods; }
      set { CtlBestiaryAbilities.RacialMods = value; }
    }

    public string Reach
    {
      get { return CtlBestiaryAbilities.Reach; }
      set { CtlBestiaryAbilities.Reach = value; }
    }

    public string Resist
    {
      get { return CtlBestiaryAbilities.Resist; }
      set { CtlBestiaryAbilities.Resist = value; }
    }

    public string SaveMods
    {
      get { return CtlBestiaryAbilities.SaveMods; }
      set { CtlBestiaryAbilities.SaveMods = value; }
    }

    public string Senses
    {
      get { return CtlBestiaryAbilities.Senses; }
      set { CtlBestiaryAbilities.Senses = value; }
    }

    public string Space
    {
      get { return CtlBestiaryAbilities.Space; }
      set { CtlBestiaryAbilities.Space = value; }
    }

    public string SpecialAbilities
    {
      get { return CtlBestiaryAbilities.SpecialAbilities; }
      set { CtlBestiaryAbilities.SpecialAbilities = value; }
    }

    public string SpecialQualities
    {
      get { return CtlBestiaryAbilities.SpecialQualities; }
      set { CtlBestiaryAbilities.SpecialQualities = value; }
    }

    public string SpeedMods
    {
      get { return CtlBestiaryAbilities.SpeedMods; }
      set { CtlBestiaryAbilities.SpeedMods = value; }
    }

    public int AC
    {
      get { return Convert.ToInt32(TxtAC.Text); }
      set { TxtAC.Text = value.ToString(); }
    }

    public int ACFlat
    {
      get { return Convert.ToInt32(TxtACFlat.Text); }
      set { TxtACFlat.Text = value.ToString(); }
    }

    public int ACTouch
    {
      get { return Convert.ToInt32(TxtACTouch.Text); }
      set { TxtACTouch.Text = value.ToString(); }
    }

    public string AgeCategory
    {
      get { return TxtAgeCategory.Text; }
      set { TxtAgeCategory.Text = value; }
    }

    public string Align
    {
      get { return TxtAlign.Text; }
      set { TxtAlign.Text = value; }
    }

    public string Archetype
    {
      get { return TxtArchetype.Text; }
      set { TxtArchetype.Text = value; }
    }

    public int BAB
    {
      get { return Convert.ToInt32(TxtBAB.Text); }
      set { TxtBAB.Text = value.ToString(); }
    }

    public string BaseStatistics
    {
      get { return TxtBaseStatistics.Text; }
      set { TxtBaseStatistics.Text = value; }
    }

    public string BeforeCombat
    {
      get { return TxtBeforeCombat.Text; }
      set { TxtBeforeCombat.Text = value; }
    }

    public string Bloodline
    {
      get { return TxtBloodline.Text; }
      set { TxtBloodline.Text = value; }
    }

    public int CHA
    {
      get { return Convert.ToInt32(TxtCHA.Text); }
      set { TxtCHA.Text = value.ToString(); }
    }

    public string Class
    {
      get { return TxtClass.Text; }
      set { TxtClass.Text = value; }
    }

    public int CMB
    {
      get { return Convert.ToInt32(TxtCMB.Text); }
      set { TxtCMB.Text = value.ToString(); }
    }

    public int CMD
    {
      get { return Convert.ToInt32(TxtCMD.Text); }
      set { TxtCMD.Text = value.ToString(); }
    }

    public int CON
    {
      get { return Convert.ToInt32(TxtCON.Text); }
      set { TxtCON.Text = value.ToString(); }
    }

    public int CR
    {
      get { return (int)DrpCR.SelectedValue; }
      set { DrpCR.SelectedValue = value; }
    }

    public string CreatureSource
    {
      get { return TxtCreatureSource.Text; }
      set { TxtCreatureSource.Text = value; }
    }

    public int DEX
    {
      get { return Convert.ToInt32(TxtDEX.Text); }
      set { TxtDEX.Text = value.ToString(); }
    }

    public string DuringCombat
    {
      get { return TxtDuringCombat.Text; }
      set { TxtDuringCombat.Text = value; }
    }

    public string FamiliarNotes
    {
      get { return TxtFamiliarNotes.Text; }
      set { TxtFamiliarNotes.Text = value; }
    }

    public string FeatNotes
    {
      get { return TxtFeatNotes.Text; }
      set { TxtFeatNotes.Text = value; }
    }

    public string FeatSearch
    {
      get { return TxtFeatSearch.Text; }
      set { TxtFeatSearch.Text = value; }
    }

    public string FocusedSchool
    {
      get { return TxtFocusedSchool.Text; }
      set { TxtFocusedSchool.Text = value; }
    }

    public int Fort
    {
      get { return Convert.ToInt32(TxtFort.Text); }
      set { TxtFort.Text = value.ToString(); }
    }

    public string Fulltext
    {
      get { return TxtFulltext.Text; }
      set { TxtFulltext.Text = value; }
    }

    public string Gear
    {
      get { return TxtGear.Text; }
      set { TxtGear.Text = value; }
    }

    public string Gender
    {
      get { return TxtGender.Text; }
      set { TxtGender.Text = value; }
    }

    public string Group
    {
      get { return TxtGroup.Text; }
      set { TxtGroup.Text = value; }
    }

    public string HD
    {
      get { return TxtHD.Text; }
      set { TxtHD.Text = value; }
    }

    public int HP
    {
      get { return Convert.ToInt32(TxtHP.Text); }
      set { TxtHP.Text = value.ToString(); }
    }

    public int Init
    {
      get { return Convert.ToInt32(TxtInit.Text); }
      set { TxtInit.Text = value.ToString(); }
    }

    public int INT
    {
      get { return Convert.ToInt32(TxtINT.Text); }
      set { TxtINT.Text = value.ToString(); }
    }

    public string Melee
    {
      get { return TxtMelee.Text; }
      set { TxtMelee.Text = value; }
    }

    public string Morale
    {
      get { return TxtMorale.Text; }
      set { TxtMorale.Text = value; }
    }

    public string Mystery
    {
      get { return TxtMystery.Text; }
      set { TxtMystery.Text = value; }
    }

    public string BestiaryName
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }

    public string Notes
    {
      get { return TxtNotes.Text; }
      set { TxtNotes.Text = value; }
    }

    public string Organization
    {
      get { return TxtOrganization.Text; }
      set { TxtOrganization.Text = value; }
    }

    public string Patron
    {
      get { return TxtPatron.Text; }
      set { TxtPatron.Text = value; }
    }

    public string ProhibitedSchools
    {
      get { return TxtProhibitedSchools.Text; }
      set { TxtProhibitedSchools.Text = value; }
    }

    public string Race
    {
      get { return TxtRace.Text; }
      set { TxtRace.Text = value; }
    }

    public string Ranged
    {
      get { return TxtRanged.Text; }
      set { TxtRanged.Text = value; }
    }

    public int Ref
    {
      get { return Convert.ToInt32(TxtRef.Text); }
      set { TxtRef.Text = value.ToString(); }
    }

    public string Search
    {
      get { return TxtSearch.Text; }
      set { TxtSearch.Text = value; }
    }

    public string Size
    {
      get { return TxtSize.Text; }
      set { TxtSize.Text = value; }
    }

    public string SkillNotes
    {
      get { return TxtSkillNotes.Text; }
      set { TxtSkillNotes.Text = value; }
    }

    public string SkillSearch
    {
      get { return TxtSkillSearch.Text; }
      set { TxtSkillSearch.Text = value; }
    }

    public int SpdBurrow
    {
      get { return Convert.ToInt32(TxtSpdBurrow.Text); }
      set { TxtSpdBurrow.Text = value.ToString(); }
    }

    public int SpdClimb
    {
      get { return Convert.ToInt32(TxtSpdClimb.Text); }
      set { TxtSpdClimb.Text = value.ToString(); }
    }

    public int SpdFly
    {
      get { return Convert.ToInt32(TxtSpdFly.Text); }
      set { TxtSpdFly.Text = value.ToString(); }
    }

    public int SpdLand
    {
      get { return Convert.ToInt32(TxtSpdLand.Text); }
      set { TxtSpdLand.Text = value.ToString(); }
    }

    public int SpdSwim
    {
      get { return Convert.ToInt32(TxtSpdSwim.Text); }
      set { TxtSpdSwim.Text = value.ToString(); }
    }

    public string Special
    {
      get { return TxtSpecial.Text; }
      set { TxtSpecial.Text = value; }
    }

    public string SpellDomains
    {
      get { return TxtSpellDomains.Text; }
      set { TxtSpellDomains.Text = value; }
    }

    public int STR
    {
      get { return Convert.ToInt32(TxtSTR.Text); }
      set { TxtSTR.Text = value.ToString(); }
    }

    public string SubType
    {
      get { return TxtSubType.Text; }
      set { TxtSubType.Text = value; }
    }

    public string TemplatesApplied
    {
      get { return TxtTemplatesApplied.Text; }
      set { TxtTemplatesApplied.Text = value; }
    }

    public string Traits
    {
      get { return TxtTraits.Text; }
      set { TxtTraits.Text = value; }
    }

    public string Treasure
    {
      get { return TxtTreasure.Text; }
      set { TxtTreasure.Text = value; }
    }

    public string VariantParent
    {
      get { return TxtVariantParent.Text; }
      set { TxtVariantParent.Text = value; }
    }

    public string VisualDescription
    {
      get { return TxtVisualDescription.Text; }
      set { TxtVisualDescription.Text = value; }
    }

    public int Will
    {
      get { return Convert.ToInt32(TxtWill.Text); }
      set { TxtWill.Text = value.ToString(); }
    }

    public int WIS
    {
      get { return Convert.ToInt32(TxtWIS.Text); }
      set { TxtWIS.Text = value.ToString(); }
    }

    public int XP
    {
      get { return Convert.ToInt32(TxtXP.Text); }
      set { TxtXP.Text = value.ToString(); }
    }

    public int MR
    {
      get { return IntMR.Value ?? 0; }
      set { IntMR.Value = value; }
    }

    public int MT
    {
      get { return IntMT.Value ?? 0; }
      set { IntMT.Value = value; }
    }

    public int SkillBonus
    {
      get { return IntSkillBonus.Value ?? 0; }
      set { IntSkillBonus.Value = value; }
    }

    public int? Type
    {
      get
      {
        if (DrpType.SelectedIndex > 0)
          return Convert.ToInt32(DrpType.SelectedValue);

        return null;
      }
      set { DrpType.SelectedValue = value; }
    }

    public bool Companion
    {
      get { return CbxCompanion.IsChecked ?? false; }
      set { CbxCompanion.IsChecked = value; }
    }

    public bool DontUseRacialHD
    {
      get { return CbxDontUseRacialHD.IsChecked ?? false; }
      set { CbxDontUseRacialHD.IsChecked = value; }
    }

    public bool Mythic
    {
      get { return CbxMythic.IsChecked ?? false; }
      set { CbxMythic.IsChecked = value; }
    }

    public bool NPC
    {
      get { return CbxNPC.IsChecked ?? false; }
      set { CbxNPC.IsChecked = value; }
    }

    public bool BestiaryTemplate
    {
      get { return CbxTemplate.IsChecked ?? false; }
      set { CbxTemplate.IsChecked = value; }
    }

    public bool UniqueCreature
    {
      get { return CbxUniqueCreature.IsChecked ?? false; }
      set { CbxUniqueCreature.IsChecked = value; }
    }

    public int BestiaryId
    {
      get { return (int)LblBestiaryId.Content; }
      set { LblBestiaryId.Content = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> BestiaryList;
    private ObservableCollection<ListItemResult> FeatList;
    private ObservableCollection<ListItemResult> SkillList;
    private ObservableCollection<ListItemResult> BestiaryTypeList;
    private ObservableCollection<ListItemResult> BestiaryList_Filter;
    private List<DisplayResult> CR_List;

    private Bestiary ActiveBestiary = new Bestiary();
    private BestiaryDetail ActiveBestiaryDetail = new BestiaryDetail();

    private bool sortNameAsc = true;
    private bool sortCrAsc = true;

    #endregion

    public BestiaryControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      CR_List = new List<DisplayResult>();
      for (int i = -4; i < 32; i++)
      {
        CR_List.Add(new DisplayResult() { Display = ParseCR(i), Result = i });
      }

      BestiaryList = new ObservableCollection<ListItemResult>();
      FeatList = new ObservableCollection<ListItemResult>();
      SkillList = new ObservableCollection<ListItemResult>();
      BestiaryTypeList = new ObservableCollection<ListItemResult>();
      BestiaryList_Filter = new ObservableCollection<ListItemResult>();

      LbxBestiary.DisplayMemberPath = LbxFeatsAll.DisplayMemberPath = LbxFeatsAssigned.DisplayMemberPath = LbxSkillsAll.DisplayMemberPath =
        LbxSkillsAssigned.DisplayMemberPath = LbxSpellAbilities.DisplayMemberPath = LbxSpellsKnown.DisplayMemberPath = LbxSpellsPrepared.DisplayMemberPath = DrpType.DisplayMemberPath = "Name";

      LbxBestiary.SelectedValuePath = LbxFeatsAll.SelectedValuePath = LbxFeatsAssigned.SelectedValuePath = LbxSkillsAll.SelectedValuePath =
        LbxSkillsAssigned.SelectedValuePath = LbxSpellAbilities.SelectedValuePath = LbxSpellsKnown.SelectedValuePath = LbxSpellsPrepared.SelectedValuePath = DrpType.SelectedValuePath = "Id";

      DrpCR.DisplayMemberPath = "Display";

      DrpCR.SelectedValuePath = "Result";

      LbxBestiary.ItemsSource = BestiaryList_Filter;
      LbxFeatsAll.ItemsSource = FeatList;
      LbxSkillsAll.ItemsSource = SkillList;

      DrpCR.ItemsSource = CR_List;
      DrpType.ItemsSource = BestiaryTypeList;
    }

    #region Private Functions

    private string ParseCR(int cr)
    {
      switch (cr)
      {
        case 0:
          return "1/2";
        case -1:
          return "1/3";
        case -2:
          return "1/4";
        case -3:
          return "1/6";
        case -4:
          return "1/8";
        default:
          return cr.ToString();
      }
    }

    private void LoadActiveBestiary()
    {
      if (ActiveBestiary == null || ActiveBestiaryDetail == null)
        return;

      AC = ActiveBestiary.Ac;
      ACFlat = ActiveBestiary.Acflat;
      ACMods = ActiveBestiary.Acmods;
      ACTouch = ActiveBestiary.Actouch;
      AgeCategory = ActiveBestiary.AgeCategory;
      Align = ActiveBestiary.Alignment;
      Archetype = ActiveBestiary.ClassArchetypes;
      Aura = ActiveBestiary.Aura;
      BAB = ActiveBestiary.BaseAtk;
      BaseStatistics = ActiveBestiary.BaseStatistics;
      BeforeCombat = ActiveBestiary.BeforeCombat;
      BestiaryId = ActiveBestiary.BestiaryId;
      Bloodline = ActiveBestiary.Bloodline;
      CHA = ActiveBestiary.Cha;
      Class = ActiveBestiary.Class;
      CMB = ActiveBestiary.Cmb;
      CMD = ActiveBestiary.Cmd;
      Companion = ActiveBestiary.CompanionFlag;
      CON = ActiveBestiary.Con;
      CR = ActiveBestiary.Cr;
      CreatureSource = ActiveBestiaryDetail.MonsterSource;
      Defense = ActiveBestiary.DefensiveAbilities;
      DEX = ActiveBestiary.Dex;
      DontUseRacialHD = ActiveBestiary.DontUseRacialHd;
      DuringCombat = ActiveBestiary.DuringCombat;
      FamiliarNotes = ActiveBestiary.CompanionFamiliarLink;
      FeatNotes = ActiveBestiary.Feats;
      FocusedSchool = ActiveBestiary.FocusedSchool;
      Fort = ActiveBestiary.Fortitude;
      Fulltext = ActiveBestiaryDetail.FullText;
      Gear = ActiveBestiary.Gear;
      Gender = ActiveBestiary.Gender;
      Group = ActiveBestiary.Group;
      HD = ActiveBestiary.Hd;
      HP = ActiveBestiary.Hp;
      Immune = ActiveBestiary.Immune;
      Init = ActiveBestiary.Init;
      INT = ActiveBestiary.Int;
      Languages = ActiveBestiary.Languages;
      Melee = ActiveBestiary.Melee;
      Morale = ActiveBestiary.Morale;
      MR = ActiveBestiary.Mr;
      MT = ActiveBestiary.Mt;
      Mystery = ActiveBestiary.Mystery;
      Mythic = ActiveBestiary.Mythic;
      BestiaryName = ActiveBestiary.Name;
      Notes = ActiveBestiary.Note;
      NPC = ActiveBestiary.CharacterFlag;
      Offense = ActiveBestiary.OffenseNote;
      OffenseNote = ActiveBestiary.OffenseNote;
      Organization = ActiveBestiary.Organization;
      Patron = ActiveBestiary.Patron;
      ProhibitedSchools = ActiveBestiary.ProhibitedSchools;
      Race = ActiveBestiary.Race;
      RacialMods = ActiveBestiary.RacialMods;
      Ranged = ActiveBestiary.Ranged;
      Reach = ActiveBestiary.Reach;
      Ref = ActiveBestiary.Reflex;
      Resist = ActiveBestiary.Resist;
      SaveMods = ActiveBestiary.SaveMods;
      Senses = ActiveBestiary.Senses;
      Size = ActiveBestiary.Size;
      Space = ActiveBestiary.Space;
      SpdBurrow = ActiveBestiary.Burrow;
      SpdClimb = ActiveBestiary.Climb;
      SpdFly = ActiveBestiary.Fly;
      SpdLand = ActiveBestiary.Land;
      SpdSwim = ActiveBestiary.Swim;
      Special = ActiveBestiary.SpecialAttacks;
      SpecialAbilities = ActiveBestiary.SpecialAbilities;
      SpecialQualities = "Not Used?";
      SpeedMods = ActiveBestiary.SpeedMod;
      SpellDomains = ActiveBestiary.SpellDomains;
      STR = ActiveBestiary.Str;
      SubType = ActiveBestiary.SubType;
      BestiaryTemplate = ActiveBestiary.IsTemplate;
      TemplatesApplied = ActiveBestiary.TemplatesApplied;
      Traits = ActiveBestiary.Traits;
      Treasure = ActiveBestiary.Traits;
      Type = ActiveBestiary.Type;
      UniqueCreature = ActiveBestiary.UniqueMonster;
      VariantParent = ActiveBestiary.VariantParent;
      VisualDescription = ActiveBestiaryDetail.Description;
      Will = ActiveBestiary.Will;
      WIS = ActiveBestiary.Wis;
      XP = ActiveBestiary.Xp;
    }

    private void SaveActiveBestiary()
    {
      ActiveBestiary.Ac = AC;
      ActiveBestiary.Acflat = ACFlat;
      ActiveBestiary.Acmods = ACMods;
      ActiveBestiary.Actouch = ACTouch;
      ActiveBestiary.AgeCategory = AgeCategory;
      ActiveBestiary.Alignment = Align;
      ActiveBestiary.ClassArchetypes = Archetype;
      ActiveBestiary.Aura = Aura;
      ActiveBestiary.BaseAtk = BAB;
      ActiveBestiary.BaseStatistics = BaseStatistics;
      ActiveBestiary.BeforeCombat = BeforeCombat;
      ActiveBestiary.BestiaryId = BestiaryId;
      ActiveBestiary.Bloodline = Bloodline;
      ActiveBestiary.Cha = CHA;
      ActiveBestiary.Class = Class;
      ActiveBestiary.Cmb = CMB;
      ActiveBestiary.Cmd = CMD;
      ActiveBestiary.CompanionFlag = Companion;
      ActiveBestiary.Con = CON;
      ActiveBestiary.Cr = CR;
      ActiveBestiaryDetail.MonsterSource = CreatureSource;
      ActiveBestiary.DefensiveAbilities = Defense;
      ActiveBestiary.Dex = DEX;
      ActiveBestiary.DontUseRacialHd = DontUseRacialHD;
      ActiveBestiary.DuringCombat = DuringCombat;
      ActiveBestiary.CompanionFamiliarLink = FamiliarNotes;
      ActiveBestiary.Feats = FeatNotes;
      ActiveBestiary.FocusedSchool = FocusedSchool;
      ActiveBestiary.Fortitude = Fort;
      ActiveBestiaryDetail.FullText = Fulltext;
      ActiveBestiary.Gear = Gear;
      ActiveBestiary.Gender = Gender;
      ActiveBestiary.Group = Group;
      ActiveBestiary.Hd = HD;
      ActiveBestiary.Hp = HP;
      ActiveBestiary.Immune = Immune;
      ActiveBestiary.Init = Init;
      ActiveBestiary.Int = INT;
      ActiveBestiary.Languages = Languages;
      ActiveBestiary.Melee = Melee;
      ActiveBestiary.Morale = Morale;
      ActiveBestiary.Mr = MR;
      ActiveBestiary.Mt = MT;
      ActiveBestiary.Mystery = Mystery;
      ActiveBestiary.Mythic = Mythic;
      ActiveBestiary.Name = BestiaryName;
      ActiveBestiary.Note = Notes;
      ActiveBestiary.CharacterFlag = NPC;
      ActiveBestiary.OffenseNote = Offense;
      ActiveBestiary.OffenseNote = OffenseNote;
      ActiveBestiary.Organization = Organization;
      ActiveBestiary.Patron = Patron;
      ActiveBestiary.ProhibitedSchools = ProhibitedSchools;
      ActiveBestiary.Race = Race;
      ActiveBestiary.RacialMods = RacialMods;
      ActiveBestiary.Ranged = Ranged;
      ActiveBestiary.Reach = Reach;
      ActiveBestiary.Reflex = Ref;
      ActiveBestiary.Resist = Resist;
      ActiveBestiary.SaveMods = SaveMods;
      ActiveBestiary.Senses = Senses;
      ActiveBestiary.Size = Size;
      ActiveBestiary.Space = Space;
      ActiveBestiary.Burrow = SpdBurrow;
      ActiveBestiary.Climb = SpdClimb;
      ActiveBestiary.Fly = SpdFly;
      ActiveBestiary.Land = SpdLand;
      ActiveBestiary.Swim = SpdSwim;
      ActiveBestiary.SpecialAttacks = Special;
      ActiveBestiary.SpecialAbilities = SpecialAbilities;
      // Special Qualities?
      ActiveBestiary.SpeedMod = SpeedMods;
      ActiveBestiary.SpellDomains = SpellDomains;
      ActiveBestiary.Str = STR;
      ActiveBestiary.SubType = SubType;
      ActiveBestiary.IsTemplate = BestiaryTemplate;
      ActiveBestiary.TemplatesApplied = TemplatesApplied;
      ActiveBestiary.Traits = Traits;
      ActiveBestiary.Traits = Treasure;
      ActiveBestiary.Type = Type;
      ActiveBestiary.UniqueMonster = UniqueCreature;
      ActiveBestiary.VariantParent = VariantParent;
      ActiveBestiaryDetail.Description = VisualDescription;
      ActiveBestiary.Will = Will;
      ActiveBestiary.Wis = WIS;
      ActiveBestiary.Xp = XP;
    }

    #endregion


    #region Events

    private void LbxBestiary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxBestiary.SelectedItem != null)
      {
        ActiveBestiary = DBClient.GetBestiary((int)LbxBestiary.SelectedValue);
        ActiveBestiaryDetail = DBClient.GetBestiaryDetail((int)LbxBestiary.SelectedValue);

        LoadActiveBestiary();
      }
    }

    private void BtnAddNew_Click(object sender, RoutedEventArgs e)
    {
      ActiveBestiary = new Bestiary();
      ActiveBestiaryDetail = new BestiaryDetail();

      LoadActiveBestiary();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveBestiary.BestiaryId == 0)
        ActiveBestiary = DBClient.CreateBestiary(ActiveBestiary);
      else
        DBClient.UpdateBestiary(ActiveBestiary);

      ActiveBestiaryDetail.BestiaryId = ActiveBestiary.BestiaryId;
      DBClient.UpdateBestiaryDetail(ActiveBestiaryDetail);

      BestiaryId = ActiveBestiary.BestiaryId;
      BestiaryList.Clear();
      BestiaryList.AddRange(DBClient.GetList("Bestiary"));
    }

    private void BtnSortName_Click(object sender, RoutedEventArgs e)
    {
      if (sortNameAsc)
      {
        var temp = new List<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Name));
        BestiaryList_Filter.Clear();
        BestiaryList_Filter.AddRange(temp);
      }
      else
      {
        var temp = new List<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Name));
        BestiaryList_Filter.Clear();
        BestiaryList_Filter.AddRange(temp);
      }

      sortNameAsc = !sortNameAsc;
      sortCrAsc = true;
    }

    private void BtnSortCR_Click(object sender, RoutedEventArgs e)
    {
      if (sortCrAsc)
      {
        var temp = new List<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Notes));
        BestiaryList_Filter.Clear();
        BestiaryList_Filter.AddRange(temp);
      }
      else
      {
        var temp = new List<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Notes));
        BestiaryList_Filter.Clear();
        BestiaryList_Filter.AddRange(temp);
      }

      sortCrAsc = !sortCrAsc;
      sortNameAsc = true;
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (BestiaryList != null) // Prevent designer failure
      {
        if (string.IsNullOrWhiteSpace(TxtSearch.Text))
        {
          BestiaryList_Filter.Clear();
          BestiaryList_Filter.AddRange(BestiaryList);
        }
        else if (TxtSearch.Text.Length >= 3)
        {
          BestiaryList_Filter.Clear();
          BestiaryList_Filter.AddRange(BestiaryList.Where(x => x.Name.ToLower().Contains(TxtSearch.Text.ToLower())));
        }
      }
    }

    private void LbxSpellsKnown_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (LbxSpellsKnown.SelectedItem != null)
      {
        // Load spell in spells tab
        // Switch to spells tab
      }
    }

    #endregion

    private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
      if (e.Key == System.Windows.Input.Key.F5)
      {
        // Reload lists
        BestiaryList.Clear();
        FeatList.Clear();
        SkillList.Clear();
        BestiaryTypeList.Clear();
        BestiaryList_Filter.Clear();

        BestiaryList.AddRange(DBClient.GetList("Bestiary").OrderBy(x => x.Name));
        FeatList.AddRange(DBClient.GetList("Feat").OrderBy(x => x.Name));
        SkillList.AddRange(DBClient.GetList("Skill").OrderBy(x => x.Name));
        BestiaryTypeList.AddRange(DBClient.GetList("BestiaryType").OrderBy(x => x.Name));
        BestiaryList_Filter.AddRange(BestiaryList);

        e.Handled = true;
      }
    }

    private void BtnClone_Click(object sender, RoutedEventArgs e)
    {
      if (BestiaryId > 0)
      {
        ActiveBestiary.BestiaryId = BestiaryId = 0;
      }
    }
  }
}
