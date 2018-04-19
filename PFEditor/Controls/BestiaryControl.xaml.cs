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
using PFEditor.Classes;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for BestiaryControl.xaml
  /// </summary>
  public partial class BestiaryControl : UserControl
  {
    #region Interface Properties

    public string Bes_ACMods
    {
      get { return CtlBestiaryAbilities.Bes_ACMods; }
      set { CtlBestiaryAbilities.Bes_ACMods = value; }
    }

    public string Bes_Aura
    {
      get { return CtlBestiaryAbilities.Bes_Aura; }
      set { CtlBestiaryAbilities.Bes_Aura = value; }
    }

    public string Bes_Defense
    {
      get { return CtlBestiaryAbilities.Bes_Defense; }
      set { CtlBestiaryAbilities.Bes_Defense = value; }
    }

    public string Bes_Immune
    {
      get { return CtlBestiaryAbilities.Bes_Immune; }
      set { CtlBestiaryAbilities.Bes_Immune = value; }
    }

    public string Bes_Languages
    {
      get { return CtlBestiaryAbilities.Bes_Languages; }
      set { CtlBestiaryAbilities.Bes_Languages = value; }
    }

    public string Bes_Offense
    {
      get { return CtlBestiaryAbilities.Bes_Offense; }
      set { CtlBestiaryAbilities.Bes_Offense = value; }
    }

    public string Bes_OffenseNote
    {
      get { return CtlBestiaryAbilities.Bes_OffenseNote; }
      set { CtlBestiaryAbilities.Bes_OffenseNote = value; }
    }

    public string Bes_RacialMods
    {
      get { return CtlBestiaryAbilities.Bes_RacialMods; }
      set { CtlBestiaryAbilities.Bes_RacialMods = value; }
    }

    public string Bes_Reach
    {
      get { return CtlBestiaryAbilities.Bes_Reach; }
      set { CtlBestiaryAbilities.Bes_Reach = value; }
    }

    public string Bes_Resist
    {
      get { return CtlBestiaryAbilities.Bes_Resist; }
      set { CtlBestiaryAbilities.Bes_Resist = value; }
    }

    public string Bes_SaveMods
    {
      get { return CtlBestiaryAbilities.Bes_SaveMods; }
      set { CtlBestiaryAbilities.Bes_SaveMods = value; }
    }

    public string Bes_Senses
    {
      get { return CtlBestiaryAbilities.Bes_Senses; }
      set { CtlBestiaryAbilities.Bes_Senses = value; }
    }

    public string Bes_Space
    {
      get { return CtlBestiaryAbilities.Bes_Space; }
      set { CtlBestiaryAbilities.Bes_Space = value; }
    }

    public string Bes_SpecialAbilities
    {
      get { return CtlBestiaryAbilities.Bes_SpecialAbilities; }
      set { CtlBestiaryAbilities.Bes_SpecialAbilities = value; }
    }

    public string Bes_SpecialQualities
    {
      get { return CtlBestiaryAbilities.Bes_SpecialQualities; }
      set { CtlBestiaryAbilities.Bes_SpecialQualities = value; }
    }

    public string Bes_SpeedMods
    {
      get { return CtlBestiaryAbilities.Bes_SpeedMods; }
      set { CtlBestiaryAbilities.Bes_SpeedMods = value; }
    }

    public int Bes_AC
    {
      get { return Convert.ToInt32(TxtAC.Text); }
      set { TxtAC.Text = value.ToString(); }
    }

    public int Bes_ACFlat
    {
      get { return Convert.ToInt32(TxtACFlat.Text); }
      set { TxtACFlat.Text = value.ToString(); }
    }

    public int Bes_ACTouch
    {
      get { return Convert.ToInt32(TxtACTouch.Text); }
      set { TxtACTouch.Text = value.ToString(); }
    }

    public string Bes_AgeCategory
    {
      get { return TxtAgeCategory.Text; }
      set { TxtAgeCategory.Text = value; }
    }

    public string Bes_Align
    {
      get { return TxtAlign.Text; }
      set { TxtAlign.Text = value; }
    }

    public string Bes_Archetype
    {
      get { return TxtArchetype.Text; }
      set { TxtArchetype.Text = value; }
    }

    public int Bes_BAB
    {
      get { return Convert.ToInt32(TxtBAB.Text); }
      set { TxtBAB.Text = value.ToString(); }
    }

    public string Bes_BaseStatistics
    {
      get { return TxtBaseStatistics.Text; }
      set { TxtBaseStatistics.Text = value; }
    }

    public string Bes_BeforeCombat
    {
      get { return TxtBeforeCombat.Text; }
      set { TxtBeforeCombat.Text = value; }
    }

    public string Bes_Bloodline
    {
      get { return TxtBloodline.Text; }
      set { TxtBloodline.Text = value; }
    }

    public int Bes_CHA
    {
      get { return Convert.ToInt32(TxtCHA.Text); }
      set { TxtCHA.Text = value.ToString(); }
    }

    public string Bes_Class
    {
      get { return TxtClass.Text; }
      set { TxtClass.Text = value; }
    }

    public int Bes_CMB
    {
      get { return Convert.ToInt32(TxtCMB.Text); }
      set { TxtCMB.Text = value.ToString(); }
    }

    public int Bes_CMD
    {
      get { return Convert.ToInt32(TxtCMD.Text); }
      set { TxtCMD.Text = value.ToString(); }
    }

    public int Bes_CON
    {
      get { return Convert.ToInt32(TxtCON.Text); }
      set { TxtCON.Text = value.ToString(); }
    }

    public int Bes_CR
    {
      get { return (int)DrpCR.SelectedValue; }
      set { DrpCR.SelectedValue = value; }
    }

    public string Bes_CreatureSource
    {
      get { return TxtCreatureSource.Text; }
      set { TxtCreatureSource.Text = value; }
    }

    public int Bes_DEX
    {
      get { return Convert.ToInt32(TxtDEX.Text); }
      set { TxtDEX.Text = value.ToString(); }
    }

    public string Bes_DuringCombat
    {
      get { return TxtDuringCombat.Text; }
      set { TxtDuringCombat.Text = value; }
    }

    public string Bes_FamiliarNotes
    {
      get { return TxtFamiliarNotes.Text; }
      set { TxtFamiliarNotes.Text = value; }
    }

    public string Bes_FeatNotes
    {
      get { return TxtFeatNotes.Text; }
      set { TxtFeatNotes.Text = value; }
    }

    public string Bes_FeatSearch
    {
      get { return TxtFeatSearch.Text; }
      set { TxtFeatSearch.Text = value; }
    }

    public string Bes_FocusedSchool
    {
      get { return TxtFocusedSchool.Text; }
      set { TxtFocusedSchool.Text = value; }
    }

    public int Bes_Fort
    {
      get { return Convert.ToInt32(TxtFort.Text); }
      set { TxtFort.Text = value.ToString(); }
    }

    public string Bes_Fulltext
    {
      get { return TxtFulltext.Text; }
      set { TxtFulltext.Text = value; }
    }

    public string Bes_Gear
    {
      get { return TxtGear.Text; }
      set { TxtGear.Text = value; }
    }

    public string Bes_Gender
    {
      get { return TxtGender.Text; }
      set { TxtGender.Text = value; }
    }

    public string Bes_Group
    {
      get { return TxtGroup.Text; }
      set { TxtGroup.Text = value; }
    }

    public string Bes_HD
    {
      get { return TxtHD.Text; }
      set { TxtHD.Text = value; }
    }

    public int Bes_HP
    {
      get { return Convert.ToInt32(TxtHP.Text); }
      set { TxtHP.Text = value.ToString(); }
    }

    public int Bes_Init
    {
      get { return Convert.ToInt32(TxtInit.Text); }
      set { TxtInit.Text = value.ToString(); }
    }

    public int Bes_INT
    {
      get { return Convert.ToInt32(TxtINT.Text); }
      set { TxtINT.Text = value.ToString(); }
    }

    public string Bes_Melee
    {
      get { return TxtMelee.Text; }
      set { TxtMelee.Text = value; }
    }

    public string Bes_Morale
    {
      get { return TxtMorale.Text; }
      set { TxtMorale.Text = value; }
    }

    public string Bes_Mystery
    {
      get { return TxtMystery.Text; }
      set { TxtMystery.Text = value; }
    }

    public string Bes_Name
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }

    public string Bes_Notes
    {
      get { return TxtNotes.Text; }
      set { TxtNotes.Text = value; }
    }

    public string Bes_Organization
    {
      get { return TxtOrganization.Text; }
      set { TxtOrganization.Text = value; }
    }

    public string Bes_Patron
    {
      get { return TxtPatron.Text; }
      set { TxtPatron.Text = value; }
    }

    public string Bes_ProhibitedSchools
    {
      get { return TxtProhibitedSchools.Text; }
      set { TxtProhibitedSchools.Text = value; }
    }

    public string Bes_Race
    {
      get { return TxtRace.Text; }
      set { TxtRace.Text = value; }
    }

    public string Bes_Ranged
    {
      get { return TxtRanged.Text; }
      set { TxtRanged.Text = value; }
    }

    public int Bes_Ref
    {
      get { return Convert.ToInt32(TxtRef.Text); }
      set { TxtRef.Text = value.ToString(); }
    }

    public string Bes_Search
    {
      get { return TxtSearch.Text; }
      set { TxtSearch.Text = value; }
    }

    public string Bes_Size
    {
      get { return TxtSize.Text; }
      set { TxtSize.Text = value; }
    }

    public string Bes_SkillNotes
    {
      get { return TxtSkillNotes.Text; }
      set { TxtSkillNotes.Text = value; }
    }

    public string Bes_SkillSearch
    {
      get { return TxtSkillSearch.Text; }
      set { TxtSkillSearch.Text = value; }
    }

    public int Bes_SpdBurrow
    {
      get { return Convert.ToInt32(TxtSpdBurrow.Text); }
      set { TxtSpdBurrow.Text = value.ToString(); }
    }

    public int Bes_SpdClimb
    {
      get { return Convert.ToInt32(TxtSpdClimb.Text); }
      set { TxtSpdClimb.Text = value.ToString(); }
    }

    public int Bes_SpdFly
    {
      get { return Convert.ToInt32(TxtSpdFly.Text); }
      set { TxtSpdFly.Text = value.ToString(); }
    }

    public int Bes_SpdLand
    {
      get { return Convert.ToInt32(TxtSpdLand.Text); }
      set { TxtSpdLand.Text = value.ToString(); }
    }

    public int Bes_SpdSwim
    {
      get { return Convert.ToInt32(TxtSpdSwim.Text); }
      set { TxtSpdSwim.Text = value.ToString(); }
    }

    public string Bes_Special
    {
      get { return TxtSpecial.Text; }
      set { TxtSpecial.Text = value; }
    }

    public string Bes_SpellDomains
    {
      get { return TxtSpellDomains.Text; }
      set { TxtSpellDomains.Text = value; }
    }

    public int Bes_STR
    {
      get { return Convert.ToInt32(TxtSTR.Text); }
      set { TxtSTR.Text = value.ToString(); }
    }

    public string Bes_SubType
    {
      get { return TxtSubType.Text; }
      set { TxtSubType.Text = value; }
    }

    public string Bes_TemplatesApplied
    {
      get { return TxtTemplatesApplied.Text; }
      set { TxtTemplatesApplied.Text = value; }
    }

    public string Bes_Traits
    {
      get { return TxtTraits.Text; }
      set { TxtTraits.Text = value; }
    }

    public string Bes_Treasure
    {
      get { return TxtTreasure.Text; }
      set { TxtTreasure.Text = value; }
    }

    public string Bes_VariantParent
    {
      get { return TxtVariantParent.Text; }
      set { TxtVariantParent.Text = value; }
    }

    public string Bes_VisualDescription
    {
      get { return TxtVisualDescription.Text; }
      set { TxtVisualDescription.Text = value; }
    }

    public int Bes_Will
    {
      get { return Convert.ToInt32(TxtWill.Text); }
      set { TxtWill.Text = value.ToString(); }
    }

    public int Bes_WIS
    {
      get { return Convert.ToInt32(TxtWIS.Text); }
      set { TxtWIS.Text = value.ToString(); }
    }

    public int Bes_XP
    {
      get { return Convert.ToInt32(TxtXP.Text); }
      set { TxtXP.Text = value.ToString(); }
    }

    public int Bes_MR
    {
      get { return IntMR.Value ?? 0; }
      set { IntMR.Value = value; }
    }

    public int Bes_MT
    {
      get { return IntMT.Value ?? 0; }
      set { IntMT.Value = value; }
    }

    public int Bes_SkillBonus
    {
      get { return IntSkillBonus.Value ?? 0; }
      set { IntSkillBonus.Value = value; }
    }

    public int Bes_Type
    {
      get { return Convert.ToInt32(DrpType.SelectedValue); }
      set { DrpType.SelectedValue = value; }
    }

    public bool Bes_Companion
    {
      get { return CbxCompanion.IsChecked ?? false; }
      set { CbxCompanion.IsChecked = value; }
    }

    public bool Bes_DontUseRacialHD
    {
      get { return CbxDontUseRacialHD.IsChecked ?? false; }
      set { CbxDontUseRacialHD.IsChecked = value; }
    }

    public bool Bes_Mythic
    {
      get { return CbxMythic.IsChecked ?? false; }
      set { CbxMythic.IsChecked = value; }
    }

    public bool Bes_NPC
    {
      get { return CbxNPC.IsChecked ?? false; }
      set { CbxNPC.IsChecked = value; }
    }

    public bool Bes_Template
    {
      get { return CbxTemplate.IsChecked ?? false; }
      set { CbxTemplate.IsChecked = value; }
    }

    public bool Bes_UniqueCreature
    {
      get { return CbxUniqueCreature.IsChecked ?? false; }
      set { CbxUniqueCreature.IsChecked = value; }
    }

    public int Bes_BestiaryId
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

      BestiaryList = new ObservableCollection<ListItemResult>(DBClient.GetList("Bestiary"));
      FeatList = new ObservableCollection<ListItemResult>(DBClient.GetList("Feat"));
      SkillList = new ObservableCollection<ListItemResult>(DBClient.GetList("Skill"));
      BestiaryTypeList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiaryType"));

      LbxBestiary.DisplayMemberPath = LbxFeatsAll.DisplayMemberPath = LbxFeatsAssigned.DisplayMemberPath = LbxSkillsAll.DisplayMemberPath =
        LbxSkillsAssigned.DisplayMemberPath = LbxSpellAbilities.DisplayMemberPath = LbxSpellsKnown.DisplayMemberPath = LbxSpellsPrepared.DisplayMemberPath = "Name";

      LbxBestiary.SelectedValuePath = LbxFeatsAll.SelectedValuePath = LbxFeatsAssigned.SelectedValuePath = LbxSkillsAll.SelectedValuePath =
        LbxSkillsAssigned.SelectedValuePath = LbxSpellAbilities.SelectedValuePath = LbxSpellsKnown.SelectedValuePath = LbxSpellsPrepared.SelectedValuePath = "Id";

      DrpCR.DisplayMemberPath = DrpType.DisplayMemberPath = "Display";

      DrpCR.SelectedValuePath = DrpType.SelectedValuePath = "Result";

      LbxBestiary.ItemsSource = BestiaryList;
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

      Bes_AC = ActiveBestiary.Ac;
      Bes_ACFlat = ActiveBestiary.Acflat;
      Bes_ACMods = ActiveBestiary.Acmods;
      Bes_ACTouch = ActiveBestiary.Actouch;
      Bes_AgeCategory = ActiveBestiary.AgeCategory;
      Bes_Align = ActiveBestiary.Alignment;
      Bes_Archetype = ActiveBestiary.ClassArchetypes;
      Bes_Aura = ActiveBestiary.Aura;
      Bes_BAB = ActiveBestiary.BaseAtk;
      Bes_BaseStatistics = ActiveBestiary.BaseStatistics;
      Bes_BeforeCombat = ActiveBestiary.BeforeCombat;
      Bes_BestiaryId = ActiveBestiary.BestiaryId;
      Bes_Bloodline = ActiveBestiary.Bloodline;
      Bes_CHA = ActiveBestiary.Cha;
      Bes_Class = ActiveBestiary.Class;
      Bes_CMB = ActiveBestiary.Cmb;
      Bes_CMD = ActiveBestiary.Cmd;
      Bes_Companion = ActiveBestiary.CompanionFlag;
      Bes_CON = ActiveBestiary.Con;
      Bes_CR = ActiveBestiary.Cr;
      Bes_CreatureSource = ActiveBestiaryDetail.MonsterSource;
      Bes_Defense = ActiveBestiary.DefensiveAbilities;
      Bes_DEX = ActiveBestiary.Dex;
      Bes_DontUseRacialHD = ActiveBestiary.DontUseRacialHd;
      Bes_DuringCombat = ActiveBestiary.DuringCombat;
      Bes_FamiliarNotes = ActiveBestiary.CompanionFamiliarLink;
      Bes_FeatNotes = ActiveBestiary.Feats;
      Bes_FocusedSchool = ActiveBestiary.FocusedSchool;
      Bes_Fort = ActiveBestiary.Fortitude;
      Bes_Fulltext = ActiveBestiaryDetail.FullText;
      Bes_Gear = ActiveBestiary.Gear;
      Bes_Gender = ActiveBestiary.Gender;
      Bes_Group = ActiveBestiary.Group;
      Bes_HD = ActiveBestiary.Hd;
      Bes_HP = ActiveBestiary.Hp;
      Bes_Immune = ActiveBestiary.Immune;
      Bes_Init = ActiveBestiary.Init;
      Bes_INT = ActiveBestiary.Int;
      Bes_Languages = ActiveBestiary.Languages;
      Bes_Melee = ActiveBestiary.Melee;
      Bes_Morale = ActiveBestiary.Morale;
      Bes_MR = ActiveBestiary.Mr;
      Bes_MT = ActiveBestiary.Mt;
      Bes_Mystery = ActiveBestiary.Mystery;
      Bes_Mythic = ActiveBestiary.Mythic;
      Bes_Name = ActiveBestiary.Name;
      Bes_Notes = ActiveBestiary.Note;
      Bes_NPC = ActiveBestiary.CharacterFlag;
      Bes_Offense = ActiveBestiary.OffenseNote;
      Bes_OffenseNote = ActiveBestiary.OffenseNote;
      Bes_Organization = ActiveBestiary.Organization;
      Bes_Patron = ActiveBestiary.Patron;
      Bes_ProhibitedSchools = ActiveBestiary.ProhibitedSchools;
      Bes_Race = ActiveBestiary.Race;
      Bes_RacialMods = ActiveBestiary.RacialMods;
      Bes_Ranged = ActiveBestiary.Ranged;
      Bes_Reach = ActiveBestiary.Reach;
      Bes_Ref = ActiveBestiary.Reflex;
      Bes_Resist = ActiveBestiary.Resist;
      Bes_SaveMods = ActiveBestiary.SaveMods;
      Bes_Senses = ActiveBestiary.Senses;
      Bes_Size = ActiveBestiary.Size;
      Bes_Space = ActiveBestiary.Space;
      Bes_SpdBurrow = ActiveBestiary.Burrow;
      Bes_SpdClimb = ActiveBestiary.Climb;
      Bes_SpdFly = ActiveBestiary.Fly;
      Bes_SpdLand = ActiveBestiary.Land;
      Bes_SpdSwim = ActiveBestiary.Swim;
      Bes_Special = ActiveBestiary.SpecialAttacks;
      Bes_SpecialAbilities = ActiveBestiary.SpecialAbilities;
      Bes_SpecialQualities = "Not Used?";
      Bes_SpeedMods = ActiveBestiary.SpeedMod;
      Bes_SpellDomains = ActiveBestiary.SpellDomains;
      Bes_STR = ActiveBestiary.Str;
      Bes_SubType = ActiveBestiary.SubType;
      Bes_Template = ActiveBestiary.IsTemplate;
      Bes_TemplatesApplied = ActiveBestiary.TemplatesApplied;
      Bes_Traits = ActiveBestiary.Traits;
      Bes_Treasure = ActiveBestiary.Traits;
      Bes_Type = ActiveBestiary.Type;
      Bes_UniqueCreature = ActiveBestiary.UniqueMonster;
      Bes_VariantParent = ActiveBestiary.VariantParent;
      Bes_VisualDescription = ActiveBestiaryDetail.Description;
      Bes_Will = ActiveBestiary.Will;
      Bes_WIS = ActiveBestiary.Wis;
      Bes_XP = ActiveBestiary.Xp;
    }

    private void SaveActiveBestiary()
    {
      ActiveBestiary.Ac = Bes_AC;
      ActiveBestiary.Acflat = Bes_ACFlat;
      ActiveBestiary.Acmods = Bes_ACMods;
      ActiveBestiary.Actouch = Bes_ACTouch;
      ActiveBestiary.AgeCategory = Bes_AgeCategory;
      ActiveBestiary.Alignment = Bes_Align;
      ActiveBestiary.ClassArchetypes = Bes_Archetype;
      ActiveBestiary.Aura = Bes_Aura;
      ActiveBestiary.BaseAtk = Bes_BAB;
      ActiveBestiary.BaseStatistics = Bes_BaseStatistics;
      ActiveBestiary.BeforeCombat = Bes_BeforeCombat;
      ActiveBestiary.BestiaryId = Bes_BestiaryId;
      ActiveBestiary.Bloodline = Bes_Bloodline;
      ActiveBestiary.Cha = Bes_CHA;
      ActiveBestiary.Class = Bes_Class;
      ActiveBestiary.Cmb = Bes_CMB;
      ActiveBestiary.Cmd = Bes_CMD;
      ActiveBestiary.CompanionFlag = Bes_Companion;
      ActiveBestiary.Con = Bes_CON;
      ActiveBestiary.Cr = Bes_CR;
      ActiveBestiaryDetail.MonsterSource = Bes_CreatureSource;
      ActiveBestiary.DefensiveAbilities = Bes_Defense;
      ActiveBestiary.Dex = Bes_DEX;
      ActiveBestiary.DontUseRacialHd = Bes_DontUseRacialHD;
      ActiveBestiary.DuringCombat = Bes_DuringCombat;
      ActiveBestiary.CompanionFamiliarLink = Bes_FamiliarNotes;
      ActiveBestiary.Feats = Bes_FeatNotes;
      ActiveBestiary.FocusedSchool = Bes_FocusedSchool;
      ActiveBestiary.Fortitude = Bes_Fort;
      ActiveBestiaryDetail.FullText = Bes_Fulltext;
      ActiveBestiary.Gear = Bes_Gear;
      ActiveBestiary.Gender = Bes_Gender;
      ActiveBestiary.Group = Bes_Group;
      ActiveBestiary.Hd = Bes_HD;
      ActiveBestiary.Hp = Bes_HP;
      ActiveBestiary.Immune = Bes_Immune;
      ActiveBestiary.Init = Bes_Init;
      ActiveBestiary.Int = Bes_INT;
      ActiveBestiary.Languages = Bes_Languages;
      ActiveBestiary.Melee = Bes_Melee;
      ActiveBestiary.Morale = Bes_Morale;
      ActiveBestiary.Mr = Bes_MR;
      ActiveBestiary.Mt = Bes_MT;
      ActiveBestiary.Mystery = Bes_Mystery;
      ActiveBestiary.Mythic = Bes_Mythic;
      ActiveBestiary.Name = Bes_Name;
      ActiveBestiary.Note = Bes_Notes;
      ActiveBestiary.CharacterFlag = Bes_NPC;
      ActiveBestiary.OffenseNote = Bes_Offense;
      ActiveBestiary.OffenseNote = Bes_OffenseNote;
      ActiveBestiary.Organization = Bes_Organization;
      ActiveBestiary.Patron = Bes_Patron;
      ActiveBestiary.ProhibitedSchools = Bes_ProhibitedSchools;
      ActiveBestiary.Race = Bes_Race;
      ActiveBestiary.RacialMods = Bes_RacialMods;
      ActiveBestiary.Ranged = Bes_Ranged;
      ActiveBestiary.Reach = Bes_Reach;
      ActiveBestiary.Reflex = Bes_Ref;
      ActiveBestiary.Resist = Bes_Resist;
      ActiveBestiary.SaveMods = Bes_SaveMods;
      ActiveBestiary.Senses = Bes_Senses;
      ActiveBestiary.Size = Bes_Size;
      ActiveBestiary.Space = Bes_Space;
      ActiveBestiary.Burrow = Bes_SpdBurrow;
      ActiveBestiary.Climb = Bes_SpdClimb;
      ActiveBestiary.Fly = Bes_SpdFly;
      ActiveBestiary.Land = Bes_SpdLand;
      ActiveBestiary.Swim = Bes_SpdSwim;
      ActiveBestiary.SpecialAttacks = Bes_Special;
      ActiveBestiary.SpecialAbilities = Bes_SpecialAbilities;
      // Special Qualities?
      ActiveBestiary.SpeedMod = Bes_SpeedMods;
      ActiveBestiary.SpellDomains = Bes_SpellDomains;
      ActiveBestiary.Str = Bes_STR;
      ActiveBestiary.SubType = Bes_SubType;
      ActiveBestiary.IsTemplate = Bes_Template;
      ActiveBestiary.TemplatesApplied = Bes_TemplatesApplied;
      ActiveBestiary.Traits = Bes_Traits;
      ActiveBestiary.Traits = Bes_Treasure;
      ActiveBestiary.Type = Bes_Type;
      ActiveBestiary.UniqueMonster = Bes_UniqueCreature;
      ActiveBestiary.VariantParent = Bes_VariantParent;
      ActiveBestiaryDetail.Description = Bes_VisualDescription;
      ActiveBestiary.Will = Bes_Will;
      ActiveBestiary.Wis = Bes_WIS;
      ActiveBestiary.Xp = Bes_XP;
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
        ActiveBestiary.BestiaryId = DBClient.CreateBestiary(ActiveBestiary);
      else
        DBClient.UpdateBestiary(ActiveBestiary);

      ActiveBestiaryDetail.BestiaryId = ActiveBestiary.BestiaryId;
      DBClient.UpdateBestiaryDetail(ActiveBestiaryDetail);

      BestiaryList = new ObservableCollection<ListItemResult>(DBClient.GetList("Bestiary"));
    }

    private void BtnSortName_Click(object sender, RoutedEventArgs e)
    {
      if (sortNameAsc)
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Name));
      else
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Name));

      sortNameAsc = !sortNameAsc;
      sortCrAsc = true;
    }

    private void BtnSortCR_Click(object sender, RoutedEventArgs e)
    {
      if (sortCrAsc)
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Notes));
      else
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Notes));

      sortCrAsc = !sortCrAsc;
      sortNameAsc = true;
    }

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (BestiaryList != null) // Prevent designer failure
      {
        if (string.IsNullOrWhiteSpace(TxtSearch.Text))
        {
          BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList);
        }
        else if (TxtSearch.Text.Length >= 3)
        {
          BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.Where(x => x.Name.ToLower().Contains(TxtSearch.Text.ToLower())));
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
  }
}
