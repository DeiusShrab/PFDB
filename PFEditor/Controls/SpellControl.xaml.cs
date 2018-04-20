using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using PFEditor.Classes;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for SpellControl.xaml
  /// </summary>
  public partial class SpellControl : UserControl
  {
    #region Interface Properties

    public string Area
    {
      get { return TxtArea.Text; }
      set { TxtArea.Text = value; }
    }

    public string Bloodline
    {
      get { return TxtBloodline.Text; }
      set { TxtBloodline.Text = value; }
    }

    public string CastingTime
    {
      get { return TxtCastingTime.Text; }
      set { TxtCastingTime.Text = value; }
    }

    public string Duration
    {
      get { return TxtDuration.Text; }
      set { TxtDuration.Text = value; }
    }

    public string SpellEffect
    {
      get { return TxtEffect.Text; }
      set { TxtEffect.Text = value; }
    }

    public string Fulltext
    {
      get { return TxtFulltext.Text; }
      set { TxtFulltext.Text = value; }
    }

    public string Materials
    {
      get { return TxtMaterials.Text; }
      set { TxtMaterials.Text = value; }
    }

    public string SpellName
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }

    public string Patron
    {
      get { return TxtPatron.Text; }
      set { TxtPatron.Text = value; }
    }

    public string Range
    {
      get { return TxtRange.Text; }
      set { TxtRange.Text = value; }
    }

    public string Save
    {
      get { return TxtSave.Text; }
      set { TxtSave.Text = value; }
    }

    public string Search
    {
      get { return TxtSearch.Text; }
      set { TxtSearch.Text = value; }
    }

    public string ShortDesc
    {
      get { return TxtShortDesc.Text; }
      set { TxtShortDesc.Text = value; }
    }

    public string SR
    {
      get { return TxtSR.Text; }
      set { TxtSR.Text = value; }
    }

    public string Targets
    {
      get { return TxtTargets.Text; }
      set { TxtTargets.Text = value; }
    }

    public string Components
    {
      get { return TxtComponents.Text; }
      set { TxtComponents.Text = value; }
    }

    public string Deity
    {
      get { return TxtDeity.Text; }
      set { TxtDeity.Text = value; }
    }

    public string Domain
    {
      get { return TxtDomain.Text; }
      set { TxtDomain.Text = value; }
    }

    public int SpellId
    {
      get { return Convert.ToInt32(LblSpellId.Content.ToString()); }
      set { LblSpellId.Content = value; }
    }

    public int? LevelAdept
    {
      get
      {
        if (DrpLevelAdept.SelectedValue != null)
          return (int)DrpLevelAdept.SelectedValue;

        return null;
      }
      set { DrpLevelAdept.SelectedValue = value; }
    }

    public int? LevelAlchemist
    {
      get
      {
        if (DrpLevelAlchemist.SelectedValue != null)
          return (int)DrpLevelAlchemist.SelectedValue;

        return null;
      }
      set { DrpLevelAlchemist.SelectedValue = value; }
    }

    public int? LevelAntipaladin
    {
      get
      {
        if (DrpLevelAntipaladin.SelectedValue != null)
          return (int)DrpLevelAntipaladin.SelectedValue;

        return null;
      }
      set { DrpLevelAntipaladin.SelectedValue = value; }
    }

    public int? LevelBard
    {
      get
      {
        if (DrpLevelBard.SelectedValue != null)
          return (int)DrpLevelBard.SelectedValue;

        return null;
      }
      set { DrpLevelBard.SelectedValue = value; }
    }

    public int? LevelBloodRager
    {
      get
      {
        if (DrpLevelBloodRager.SelectedValue != null)
          return (int)DrpLevelBloodRager.SelectedValue;

        return null;
      }
      set { DrpLevelBloodRager.SelectedValue = value; }
    }

    public int? LevelCleric
    {
      get
      {
        if (DrpLevelCleric.SelectedValue != null)
          return (int)DrpLevelCleric.SelectedValue;

        return null;
      }
      set { DrpLevelCleric.SelectedValue = value; }
    }

    public int? LevelDruid
    {
      get
      {
        if (DrpLevelDruid.SelectedValue != null)
          return (int)DrpLevelDruid.SelectedValue;

        return null;
      }
      set { DrpLevelDruid.SelectedValue = value; }
    }

    public int? LevelHunter
    {
      get
      {
        if (DrpLevelHunter.SelectedValue != null)
          return (int)DrpLevelHunter.SelectedValue;

        return null;
      }
      set { DrpLevelHunter.SelectedValue = value; }
    }

    public int? LevelInquisitor
    {
      get
      {
        if (DrpLevelInquisitor.SelectedValue != null)
          return (int)DrpLevelInquisitor.SelectedValue;

        return null;
      }
      set { DrpLevelInquisitor.SelectedValue = value; }
    }

    public int? LevelInvestigator
    {
      get
      {
        if (DrpLevelInvestigator.SelectedValue != null)
          return (int)DrpLevelInvestigator.SelectedValue;

        return null;
      }
      set { DrpLevelInvestigator.SelectedValue = value; }
    }

    public int? LevelMagus
    {
      get
      {
        if (DrpLevelMagus.SelectedValue != null)
          return (int)DrpLevelMagus.SelectedValue;

        return null;
      }
      set { DrpLevelMagus.SelectedValue = value; }
    }

    public int? LevelMedium
    {
      get
      {
        if (DrpLevelMedium.SelectedValue != null)
          return (int)DrpLevelMedium.SelectedValue;

        return null;
      }
      set { DrpLevelMedium.SelectedValue = value; }
    }

    public int? LevelMesmerist
    {
      get
      {
        if (DrpLevelMesmerist.SelectedValue != null)
          return (int)DrpLevelMesmerist.SelectedValue;

        return null;
      }
      set { DrpLevelMesmerist.SelectedValue = value; }
    }

    public int? LevelOccultist
    {
      get
      {
        if (DrpLevelOccultist.SelectedValue != null)
          return (int)DrpLevelOccultist.SelectedValue;

        return null;
      }
      set { DrpLevelOccultist.SelectedValue = value; }
    }

    public int? LevelOracle
    {
      get
      {
        if (DrpLevelOracle.SelectedValue != null)
          return (int)DrpLevelOracle.SelectedValue;

        return null;
      }
      set { DrpLevelOracle.SelectedValue = value; }
    }

    public int? LevelPaladin
    {
      get
      {
        if (DrpLevelPaladin.SelectedValue != null)
          return (int)DrpLevelPaladin.SelectedValue;

        return null;
      }
      set { DrpLevelPaladin.SelectedValue = value; }
    }

    public int? LevelPsychic
    {
      get
      {
        if (DrpLevelPsychic.SelectedValue != null)
          return (int)DrpLevelPsychic.SelectedValue;

        return null;
      }
      set { DrpLevelPsychic.SelectedValue = value; }
    }

    public int? LevelRanger
    {
      get
      {
        if (DrpLevelRanger.SelectedValue != null)
          return (int)DrpLevelRanger.SelectedValue;

        return null;
      }
      set { DrpLevelRanger.SelectedValue = value; }
    }

    public int? LevelShaman
    {
      get
      {
        if (DrpLevelShaman.SelectedValue != null)
          return (int)DrpLevelShaman.SelectedValue;

        return null;
      }
      set { DrpLevelShaman.SelectedValue = value; }
    }

    public int? LevelSkald
    {
      get
      {
        if (DrpLevelSkald.SelectedValue != null)
          return (int)DrpLevelSkald.SelectedValue;

        return null;
      }
      set { DrpLevelSkald.SelectedValue = value; }
    }

    public int? LevelSor
    {
      get
      {
        if (DrpLevelSor.SelectedValue != null)
          return (int)DrpLevelSor.SelectedValue;

        return null;
      }
      set { DrpLevelSor.SelectedValue = value; }
    }

    public int? LevelSpiritualist
    {
      get
      {
        if (DrpLevelSpiritualist.SelectedValue != null)
          return (int)DrpLevelSpiritualist.SelectedValue;

        return null;
      }
      set { DrpLevelSpiritualist.SelectedValue = value; }
    }

    public int? LevelSummoner
    {
      get
      {
        if (DrpLevelSummoner.SelectedValue != null)
          return (int)DrpLevelSummoner.SelectedValue;

        return null;
      }
      set { DrpLevelSummoner.SelectedValue = value; }
    }

    public int? LevelWitch
    {
      get
      {
        if (DrpLevelWitch.SelectedValue != null)
          return (int)DrpLevelWitch.SelectedValue;

        return null;
      }
      set { DrpLevelWitch.SelectedValue = value; }
    }

    public int? LevelWiz
    {
      get
      {
        if (DrpLevelWiz.SelectedValue != null)
          return (int)DrpLevelWiz.SelectedValue;

        return null;
      }
      set { DrpLevelWiz.SelectedValue = value; }
    }

    public int? SchoolId
    {
      get
      {
        if (DrpSchool.SelectedValue != null)
          return (int)DrpSchool.SelectedValue;

        return null;
      }
      set { DrpSchool.SelectedValue = value; }
    }

    public int? SubSchoolId
    {
      get
      {
        if (DrpSubSchool.SelectedValue != null)
          return (int)DrpSubSchool.SelectedValue;

        return null;
      }
      set { DrpSubSchool.SelectedValue = value; }
    }

    public int MaterialCost
    {
      get { return IntMaterialCost.Value ?? 0; }
      set { IntMaterialCost.Value = value; }
    }

    public bool Acid
    {
      get { return CbxAcid.IsChecked ?? false; }
      set { CbxAcid.IsChecked = value; }
    }

    public bool Air
    {
      get { return CbxAir.IsChecked ?? false; }
      set { CbxAir.IsChecked = value; }
    }

    public bool Chaotic
    {
      get { return CbxChaotic.IsChecked ?? false; }
      set { CbxChaotic.IsChecked = value; }
    }

    public bool Cold
    {
      get { return CbxCold.IsChecked ?? false; }
      set { CbxCold.IsChecked = value; }
    }

    public bool CostlyComponents
    {
      get { return CbxCostlyComponents.IsChecked ?? false; }
      set { CbxCostlyComponents.IsChecked = value; }
    }

    public bool Curse
    {
      get { return CbxCurse.IsChecked ?? false; }
      set { CbxCurse.IsChecked = value; }
    }

    public bool Darkness
    {
      get { return CbxDarkness.IsChecked ?? false; }
      set { CbxDarkness.IsChecked = value; }
    }

    public bool Death
    {
      get { return CbxDeath.IsChecked ?? false; }
      set { CbxDeath.IsChecked = value; }
    }

    public bool Disease
    {
      get { return CbxDisease.IsChecked ?? false; }
      set { CbxDisease.IsChecked = value; }
    }

    public bool Dismissable
    {
      get { return CbxDismissable.IsChecked ?? false; }
      set { CbxDismissable.IsChecked = value; }
    }

    public bool DivineFocus
    {
      get { return CbxDivineFocus.IsChecked ?? false; }
      set { CbxDivineFocus.IsChecked = value; }
    }

    public bool Earth
    {
      get { return CbxEarth.IsChecked ?? false; }
      set { CbxEarth.IsChecked = value; }
    }

    public bool Electricity
    {
      get { return CbxElectricity.IsChecked ?? false; }
      set { CbxElectricity.IsChecked = value; }
    }

    public bool Emotion
    {
      get { return CbxEmotion.IsChecked ?? false; }
      set { CbxEmotion.IsChecked = value; }
    }

    public bool Evil
    {
      get { return CbxEvil.IsChecked ?? false; }
      set { CbxEvil.IsChecked = value; }
    }

    public bool Fear
    {
      get { return CbxFear.IsChecked ?? false; }
      set { CbxFear.IsChecked = value; }
    }

    public bool Fire
    {
      get { return CbxFire.IsChecked ?? false; }
      set { CbxFire.IsChecked = value; }
    }

    public bool SpellFocus
    {
      get { return CbxFocus.IsChecked ?? false; }
      set { CbxFocus.IsChecked = value; }
    }

    public bool Force
    {
      get { return CbxForce.IsChecked ?? false; }
      set { CbxForce.IsChecked = value; }
    }

    public bool Good
    {
      get { return CbxGood.IsChecked ?? false; }
      set { CbxGood.IsChecked = value; }
    }

    public bool LanguageDependent
    {
      get { return CbxLanguageDependent.IsChecked ?? false; }
      set { CbxLanguageDependent.IsChecked = value; }
    }

    public bool Lawful
    {
      get { return CbxLawful.IsChecked ?? false; }
      set { CbxLawful.IsChecked = value; }
    }

    public bool Light
    {
      get { return CbxLight.IsChecked ?? false; }
      set { CbxLight.IsChecked = value; }
    }

    public bool Material
    {
      get { return CbxMaterial.IsChecked ?? false; }
      set { CbxMaterial.IsChecked = value; }
    }

    public bool MindAffecting
    {
      get { return CbxMindAffecting.IsChecked ?? false; }
      set { CbxMindAffecting.IsChecked = value; }
    }

    public bool Mythic
    {
      get { return CbxMythic.IsChecked ?? false; }
      set { CbxMythic.IsChecked = value; }
    }

    public bool Pain
    {
      get { return CbxPain.IsChecked ?? false; }
      set { CbxPain.IsChecked = value; }
    }

    public bool Poison
    {
      get { return CbxPoison.IsChecked ?? false; }
      set { CbxPoison.IsChecked = value; }
    }

    public bool Shadow
    {
      get { return CbxShadow.IsChecked ?? false; }
      set { CbxShadow.IsChecked = value; }
    }

    public bool Shapeable
    {
      get { return CbxShapeable.IsChecked ?? false; }
      set { CbxShapeable.IsChecked = value; }
    }

    public bool Somatic
    {
      get { return CbxSomatic.IsChecked ?? false; }
      set { CbxSomatic.IsChecked = value; }
    }

    public bool Sonic
    {
      get { return CbxSonic.IsChecked ?? false; }
      set { CbxSonic.IsChecked = value; }
    }

    public bool Verbal
    {
      get { return CbxVerbal.IsChecked ?? false; }
      set { CbxVerbal.IsChecked = value; }
    }

    public bool Water
    {
      get { return CbxWater.IsChecked ?? false; }
      set { CbxWater.IsChecked = value; }
    }

    public int SelectedSpellSchoolId
    {
      get
      {
        int.TryParse(DrpSchool.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpSchool.SelectedValue = value; }
    }

    public int SelectedSpellSubSchoolId
    {
      get
      {
        int.TryParse(DrpSubSchool.SelectedValue.ToString(), out int ret);
        return ret;
      }
      set { DrpSubSchool.SelectedValue = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> SpellList;
    private ObservableCollection<ListItemResult> SpellSchoolList;
    private ObservableCollection<ListItemResult> SpellSubSchoolList;
    private ObservableCollection<ListItemResult> SpellSubSchoolList_Filter;
    private List<DisplayResult> Mag_LevelList;

    private Spell ActiveSpell = new Spell();
    private SpellDetail ActiveSpellDetail = new SpellDetail();    

    #endregion

    #region Constructor

    public SpellControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;

      Mag_LevelList = new List<DisplayResult>
      {
        new DisplayResult() { Display = "--", Result = -1 },
        new DisplayResult() { Display = "0", Result = 0 },
        new DisplayResult() { Display = "1", Result = 1 },
        new DisplayResult() { Display = "2", Result = 2 },
        new DisplayResult() { Display = "3", Result = 3 },
        new DisplayResult() { Display = "4", Result = 4 },
        new DisplayResult() { Display = "5", Result = 5 },
        new DisplayResult() { Display = "6", Result = 6 },
        new DisplayResult() { Display = "7", Result = 7 },
        new DisplayResult() { Display = "8", Result = 8 },
        new DisplayResult() { Display = "9", Result = 9 }
      };

      SpellList = new ObservableCollection<ListItemResult>(DBClient.GetList("Spell"));
      SpellSchoolList = new ObservableCollection<ListItemResult>(DBClient.GetList("SpellSchool"));
      SpellSubSchoolList = new ObservableCollection<ListItemResult>(DBClient.GetList("SpellSubSchool"));
      SpellSubSchoolList_Filter = new ObservableCollection<ListItemResult>(SpellSubSchoolList.Where(x => x.Notes == SelectedSpellSubSchoolId.ToString()));

      LbxSpell.DisplayMemberPath = "Name";
      LbxSpell.SelectedValuePath = "Id";

      DrpLevelAdept.DisplayMemberPath = DrpLevelAlchemist.DisplayMemberPath = DrpLevelAntipaladin.DisplayMemberPath =
        DrpLevelBard.DisplayMemberPath = DrpLevelBloodRager.DisplayMemberPath = DrpLevelCleric.DisplayMemberPath = DrpLevelDruid.DisplayMemberPath =
        DrpLevelHunter.DisplayMemberPath = DrpLevelInquisitor.DisplayMemberPath = DrpLevelInvestigator.DisplayMemberPath = DrpLevelMagus.DisplayMemberPath =
        DrpLevelMedium.DisplayMemberPath = DrpLevelMesmerist.DisplayMemberPath = DrpLevelOccultist.DisplayMemberPath = DrpLevelOracle.DisplayMemberPath =
        DrpLevelPaladin.DisplayMemberPath = DrpLevelPsychic.DisplayMemberPath = DrpLevelRanger.DisplayMemberPath = DrpLevelShaman.DisplayMemberPath =
        DrpLevelSkald.DisplayMemberPath = DrpLevelSor.DisplayMemberPath = DrpLevelSpiritualist.DisplayMemberPath = DrpLevelSummoner.DisplayMemberPath =
        DrpLevelWitch.DisplayMemberPath = DrpLevelWiz.DisplayMemberPath = "Display";

      DrpLevelAdept.SelectedValuePath = DrpLevelAlchemist.SelectedValuePath = DrpLevelAntipaladin.SelectedValuePath =
        DrpLevelBard.SelectedValuePath = DrpLevelBloodRager.SelectedValuePath = DrpLevelCleric.SelectedValuePath = DrpLevelDruid.SelectedValuePath =
        DrpLevelHunter.SelectedValuePath = DrpLevelInquisitor.SelectedValuePath = DrpLevelInvestigator.SelectedValuePath = DrpLevelMagus.SelectedValuePath =
        DrpLevelMedium.SelectedValuePath = DrpLevelMesmerist.SelectedValuePath = DrpLevelOccultist.SelectedValuePath = DrpLevelOracle.SelectedValuePath =
        DrpLevelPaladin.SelectedValuePath = DrpLevelPsychic.SelectedValuePath = DrpLevelRanger.SelectedValuePath = DrpLevelShaman.SelectedValuePath =
        DrpLevelSkald.SelectedValuePath = DrpLevelSor.SelectedValuePath = DrpLevelSpiritualist.SelectedValuePath = DrpLevelSummoner.SelectedValuePath =
        DrpLevelWitch.SelectedValuePath = DrpLevelWiz.SelectedValuePath = "Result";


      LbxSpell.ItemsSource = SpellList;

      DrpLevelAdept.ItemsSource = Mag_LevelList;
      DrpLevelAlchemist.ItemsSource = Mag_LevelList;
      DrpLevelAntipaladin.ItemsSource = Mag_LevelList;
      DrpLevelBard.ItemsSource = Mag_LevelList;
      DrpLevelBloodRager.ItemsSource = Mag_LevelList;
      DrpLevelCleric.ItemsSource = Mag_LevelList;
      DrpLevelDruid.ItemsSource = Mag_LevelList;
      DrpLevelHunter.ItemsSource = Mag_LevelList;
      DrpLevelInquisitor.ItemsSource = Mag_LevelList;
      DrpLevelInvestigator.ItemsSource = Mag_LevelList;
      DrpLevelMagus.ItemsSource = Mag_LevelList;
      DrpLevelMedium.ItemsSource = Mag_LevelList;
      DrpLevelMesmerist.ItemsSource = Mag_LevelList;
      DrpLevelOccultist.ItemsSource = Mag_LevelList;
      DrpLevelOracle.ItemsSource = Mag_LevelList;
      DrpLevelPaladin.ItemsSource = Mag_LevelList;
      DrpLevelPsychic.ItemsSource = Mag_LevelList;
      DrpLevelRanger.ItemsSource = Mag_LevelList;
      DrpLevelShaman.ItemsSource = Mag_LevelList;
      DrpLevelSkald.ItemsSource = Mag_LevelList;
      DrpLevelSor.ItemsSource = Mag_LevelList;
      DrpLevelSpiritualist.ItemsSource = Mag_LevelList;
      DrpLevelSummoner.ItemsSource = Mag_LevelList;
      DrpLevelWitch.ItemsSource = Mag_LevelList;
      DrpLevelWiz.ItemsSource = Mag_LevelList;

      DrpSchool.ItemsSource = SpellSchoolList;
      DrpSubSchool.ItemsSource = SpellSubSchoolList_Filter;
    }

    #endregion

    #region Private Methods

    private void LoadActiveSpell()
    {
      if (LbxSpell.SelectedItem != null)
      {
        ActiveSpell = DBClient.GetSpell((int)LbxSpell.SelectedItem);
        ActiveSpellDetail = DBClient.GetSpellDetail((int)LbxSpell.SelectedItem);

        Acid = ActiveSpell.Acid;
        LevelAdept = ActiveSpell.Adept;
        Air = ActiveSpell.Air;
        LevelAlchemist = ActiveSpell.Alchemist;
        LevelAntipaladin = ActiveSpell.Antipaladin;
        Area = ActiveSpell.Area;
        LevelBard = ActiveSpell.Bard;
        Bloodline = ActiveSpell.Bloodline;
        LevelBloodRager = ActiveSpell.BloodRager;
        CastingTime = ActiveSpell.CastingTime;
        Chaotic = ActiveSpell.Chaotic;
        LevelCleric = ActiveSpell.Cleric;
        Cold = ActiveSpell.Cold;
        Components = ActiveSpell.Components;
        CostlyComponents = ActiveSpell.CostlyComponents;
        Curse = ActiveSpell.Curse;
        Darkness = ActiveSpell.Darkness;
        Death = ActiveSpell.Death;
        Deity = ActiveSpell.Deity;
        Disease = ActiveSpell.Disease;
        Dismissable = ActiveSpell.Dismissable;
        DivineFocus = ActiveSpell.DivineFocus;
        Domain = ActiveSpell.Domain;
        LevelDruid = ActiveSpell.Druid;
        Duration = ActiveSpell.Duration;
        Earth = ActiveSpell.Earth;
        SpellEffect = ActiveSpell.Effect;
        Electricity = ActiveSpell.Electricity;
        Emotion = ActiveSpell.Emotion;
        Evil = ActiveSpell.Evil;
        Fear = ActiveSpell.Fear;
        Fire = ActiveSpell.Fire;
        SpellFocus = ActiveSpell.Focus;
        Force = ActiveSpell.Force;
        Good = ActiveSpell.Good;
        LevelHunter = ActiveSpell.Hunter;
        LevelInquisitor = ActiveSpell.Inquisitor;
        LevelInvestigator = ActiveSpell.Investigator;
        LanguageDependent = ActiveSpell.LanguageDependent;
        Lawful = ActiveSpell.Lawful;
        Light = ActiveSpell.Light;
        LevelMagus = ActiveSpell.Magus;
        Material = ActiveSpell.Material;
        MaterialCost = ActiveSpell.MaterialCost;
        LevelMedium = ActiveSpell.Medium;
        LevelMesmerist = ActiveSpell.Mesmerist;
        MindAffecting = ActiveSpell.MindAffecting;
        Mythic = ActiveSpell.Mythic;
        SpellName = ActiveSpell.Name;
        LevelOccultist = ActiveSpell.Occultist;
        LevelOracle = ActiveSpell.Oracle;
        Pain = ActiveSpell.Pain;
        LevelPaladin = ActiveSpell.Paladin;
        Patron = ActiveSpell.Patron;
        Poison = ActiveSpell.Poison;
        LevelPsychic = ActiveSpell.Psychic;
        Range = ActiveSpell.Range;
        LevelRanger = ActiveSpell.Ranger;
        Save = ActiveSpell.SavingThrow;
        SchoolId = ActiveSpell.SchoolId;
        Shadow = ActiveSpell.Shadow;
        LevelShaman = ActiveSpell.Shaman;
        Shapeable = ActiveSpell.Shapeable;
        ShortDesc = ActiveSpell.ShortDescription;
        LevelSkald = ActiveSpell.Skald;
        Somatic = ActiveSpell.Somatic;
        Sonic = ActiveSpell.Sonic;
        LevelSor = ActiveSpell.Sor;
        SpellId = ActiveSpell.SpellId;
        SR = ActiveSpell.SpellResistance;
        LevelSpiritualist = ActiveSpell.Spiritualist;
        SubSchoolId = ActiveSpell.SubSchoolId;
        LevelSummoner = ActiveSpell.Summoner;
        Targets = ActiveSpell.Targets;
        Verbal = ActiveSpell.Verbal;
        Water = ActiveSpell.Water;
        LevelWitch = ActiveSpell.Witch;
        LevelWiz = ActiveSpell.Wiz;

        Fulltext = ActiveSpellDetail.FullText;
        SpellId = ActiveSpellDetail.SpellId;
      }
    }

    private void SaveActiveSpell()
    {
      ActiveSpell.Acid = Acid;
      ActiveSpell.Adept = LevelAdept;
      ActiveSpell.Air = Air;
      ActiveSpell.Alchemist = LevelAlchemist;
      ActiveSpell.Antipaladin = LevelAntipaladin;
      ActiveSpell.Area = Area;
      ActiveSpell.Bard = LevelBard;
      ActiveSpell.Bloodline = Bloodline;
      ActiveSpell.BloodRager = LevelBloodRager;
      ActiveSpell.CastingTime = CastingTime;
      ActiveSpell.Chaotic = Chaotic;
      ActiveSpell.Cleric = LevelCleric;
      ActiveSpell.Cold = Cold;
      ActiveSpell.Components = Components;
      ActiveSpell.CostlyComponents = CostlyComponents;
      ActiveSpell.Curse = Curse;
      ActiveSpell.Darkness = Darkness;
      ActiveSpell.Death = Death;
      ActiveSpell.Deity = Deity;
      ActiveSpell.Disease = Disease;
      ActiveSpell.Dismissable = Dismissable;
      ActiveSpell.DivineFocus = DivineFocus;
      ActiveSpell.Domain = Domain;
      ActiveSpell.Druid = LevelDruid;
      ActiveSpell.Duration = Duration;
      ActiveSpell.Earth = Earth;
      ActiveSpell.Effect = SpellEffect;
      ActiveSpell.Electricity = Electricity;
      ActiveSpell.Emotion = Emotion;
      ActiveSpell.Evil = Evil;
      ActiveSpell.Fear = Fear;
      ActiveSpell.Fire = Fire;
      ActiveSpell.Focus = SpellFocus;
      ActiveSpell.Force = Force;
      ActiveSpell.Good = Good;
      ActiveSpell.Hunter = LevelHunter;
      ActiveSpell.Inquisitor = LevelInquisitor;
      ActiveSpell.Investigator = LevelInvestigator;
      ActiveSpell.LanguageDependent = LanguageDependent;
      ActiveSpell.Lawful = Lawful;
      ActiveSpell.Light = Light;
      ActiveSpell.Magus = LevelMagus;
      ActiveSpell.Material = Material;
      ActiveSpell.MaterialCost = MaterialCost;
      ActiveSpell.Medium = LevelMedium;
      ActiveSpell.Mesmerist = LevelMesmerist;
      ActiveSpell.MindAffecting = MindAffecting;
      ActiveSpell.Mythic = Mythic;
      ActiveSpell.Name = SpellName;
      ActiveSpell.Occultist = LevelOccultist;
      ActiveSpell.Oracle = LevelOracle;
      ActiveSpell.Pain = Pain;
      ActiveSpell.Paladin = LevelPaladin;
      ActiveSpell.Patron = Patron;
      ActiveSpell.Poison = Poison;
      ActiveSpell.Psychic = LevelPsychic;
      ActiveSpell.Range = Range;
      ActiveSpell.Ranger = LevelRanger;
      ActiveSpell.SavingThrow = Save;
      ActiveSpell.SchoolId = SchoolId ?? 0;
      ActiveSpell.Shadow = Shadow;
      ActiveSpell.Shaman = LevelShaman;
      ActiveSpell.Shapeable = Shapeable;
      ActiveSpell.ShortDescription = ShortDesc;
      ActiveSpell.Skald = LevelSkald;
      ActiveSpell.Somatic = Somatic;
      ActiveSpell.Sonic = Sonic;
      ActiveSpell.Sor = LevelSor;
      ActiveSpell.SpellId = SpellId;
      ActiveSpell.SpellResistance = SR;
      ActiveSpell.Spiritualist = LevelSpiritualist;
      ActiveSpell.SubSchoolId = SubSchoolId ?? 0;
      ActiveSpell.Summoner = LevelSummoner;
      ActiveSpell.Targets = Targets;
      ActiveSpell.Verbal = Verbal;
      ActiveSpell.Water = Water;
      ActiveSpell.Witch = LevelWitch;
      ActiveSpell.Wiz = LevelWiz;

      ActiveSpellDetail.FullText = Fulltext;
      ActiveSpellDetail.SpellId = SpellId;
    }

    #endregion

    private void DrpSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if (SelectedSpellSchoolId > 0)
        SpellSubSchoolList_Filter = new ObservableCollection<ListItemResult>(SpellSubSchoolList.Where(x => x.Notes == SelectedSpellSubSchoolId.ToString()));
    }

    private void BtnNewSpell_Click(object sender, System.Windows.RoutedEventArgs e)
    {

    }
  }
}
