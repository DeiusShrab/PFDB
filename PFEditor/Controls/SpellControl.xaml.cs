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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
    #region Interface Variables

    public string Spl_Bloodline
    {
      get { return TxtBloodline.Text; }
      set { TxtBloodline.Text = value; }
    }

    public string Spl_CastingTime
    {
      get { return TxtCastingTime.Text; }
      set { TxtCastingTime.Text = value; }
    }

    public string Spl_Duration
    {
      get { return TxtDuration.Text; }
      set { TxtDuration.Text = value; }
    }

    public string Spl_Effect
    {
      get { return TxtEffect.Text; }
      set { TxtEffect.Text = value; }
    }

    public string Spl_Fulltext
    {
      get { return TxtFulltext.Text; }
      set { TxtFulltext.Text = value; }
    }

    public string Spl_Materials
    {
      get { return TxtMaterials.Text; }
      set { TxtMaterials.Text = value; }
    }

    public string Spl_Name
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }

    public string Spl_Patron
    {
      get { return TxtPatron.Text; }
      set { TxtPatron.Text = value; }
    }

    public string Spl_Range
    {
      get { return TxtRange.Text; }
      set { TxtRange.Text = value; }
    }

    public string Spl_Save
    {
      get { return TxtSave.Text; }
      set { TxtSave.Text = value; }
    }

    public string Spl_Search
    {
      get { return TxtSearch.Text; }
      set { TxtSearch.Text = value; }
    }

    public string Spl_ShortDesc
    {
      get { return TxtShortDesc.Text; }
      set { TxtShortDesc.Text = value; }
    }

    public string Spl_SR
    {
      get { return TxtSR.Text; }
      set { TxtSR.Text = value; }
    }

    public string Spl_Targets
    {
      get { return TxtTargets.Text; }
      set { TxtTargets.Text = value; }
    }

    public int Spl_LevelAdept
    {
      get { return Convert.ToInt32(DrpLevelAdept.SelectedValue); }
      set { DrpLevelAdept.SelectedValue = value; }
    }

    public int Spl_LevelAlchemist
    {
      get { return Convert.ToInt32(DrpLevelAlchemist.SelectedValue); }
      set { DrpLevelAlchemist.SelectedValue = value; }
    }

    public int Spl_LevelAntipaladin
    {
      get { return Convert.ToInt32(DrpLevelAntipaladin.SelectedValue); }
      set { DrpLevelAntipaladin.SelectedValue = value; }
    }

    public int Spl_LevelBard
    {
      get { return Convert.ToInt32(DrpLevelBard.SelectedValue); }
      set { DrpLevelBard.SelectedValue = value; }
    }

    public int Spl_LevelBloodRager
    {
      get { return Convert.ToInt32(DrpLevelBloodRager.SelectedValue); }
      set { DrpLevelBloodRager.SelectedValue = value; }
    }

    public int Spl_LevelCleric
    {
      get { return Convert.ToInt32(DrpLevelCleric.SelectedValue); }
      set { DrpLevelCleric.SelectedValue = value; }
    }

    public int Spl_LevelDruid
    {
      get { return Convert.ToInt32(DrpLevelDruid.SelectedValue); }
      set { DrpLevelDruid.SelectedValue = value; }
    }

    public int Spl_LevelHunter
    {
      get { return Convert.ToInt32(DrpLevelHunter.SelectedValue); }
      set { DrpLevelHunter.SelectedValue = value; }
    }

    public int Spl_LevelInquisitor
    {
      get { return Convert.ToInt32(DrpLevelInquisitor.SelectedValue); }
      set { DrpLevelInquisitor.SelectedValue = value; }
    }

    public int Spl_LevelInvestigator
    {
      get { return Convert.ToInt32(DrpLevelInvestigator.SelectedValue); }
      set { DrpLevelInvestigator.SelectedValue = value; }
    }

    public int Spl_LevelMagus
    {
      get { return Convert.ToInt32(DrpLevelMagus.SelectedValue); }
      set { DrpLevelMagus.SelectedValue = value; }
    }

    public int Spl_LevelMedium
    {
      get { return Convert.ToInt32(DrpLevelMedium.SelectedValue); }
      set { DrpLevelMedium.SelectedValue = value; }
    }

    public int Spl_LevelMesmerist
    {
      get { return Convert.ToInt32(DrpLevelMesmerist.SelectedValue); }
      set { DrpLevelMesmerist.SelectedValue = value; }
    }

    public int Spl_LevelOccultist
    {
      get { return Convert.ToInt32(DrpLevelOccultist.SelectedValue); }
      set { DrpLevelOccultist.SelectedValue = value; }
    }

    public int Spl_LevelOracle
    {
      get { return Convert.ToInt32(DrpLevelOracle.SelectedValue); }
      set { DrpLevelOracle.SelectedValue = value; }
    }

    public int Spl_LevelPaladin
    {
      get { return Convert.ToInt32(DrpLevelPaladin.SelectedValue); }
      set { DrpLevelPaladin.SelectedValue = value; }
    }

    public int Spl_LevelPsychic
    {
      get { return Convert.ToInt32(DrpLevelPsychic.SelectedValue); }
      set { DrpLevelPsychic.SelectedValue = value; }
    }

    public int Spl_LevelRanger
    {
      get { return Convert.ToInt32(DrpLevelRanger.SelectedValue); }
      set { DrpLevelRanger.SelectedValue = value; }
    }

    public int Spl_LevelShaman
    {
      get { return Convert.ToInt32(DrpLevelShaman.SelectedValue); }
      set { DrpLevelShaman.SelectedValue = value; }
    }

    public int Spl_LevelSkald
    {
      get { return Convert.ToInt32(DrpLevelSkald.SelectedValue); }
      set { DrpLevelSkald.SelectedValue = value; }
    }

    public int Spl_LevelSor
    {
      get { return Convert.ToInt32(DrpLevelSor.SelectedValue); }
      set { DrpLevelSor.SelectedValue = value; }
    }

    public int Spl_LevelSpiritualist
    {
      get { return Convert.ToInt32(DrpLevelSpiritualist.SelectedValue); }
      set { DrpLevelSpiritualist.SelectedValue = value; }
    }

    public int Spl_LevelSummoner
    {
      get { return Convert.ToInt32(DrpLevelSummoner.SelectedValue); }
      set { DrpLevelSummoner.SelectedValue = value; }
    }

    public int Spl_LevelWitch
    {
      get { return Convert.ToInt32(DrpLevelWitch.SelectedValue); }
      set { DrpLevelWitch.SelectedValue = value; }
    }

    public int Spl_LevelWiz
    {
      get { return Convert.ToInt32(DrpLevelWiz.SelectedValue); }
      set { DrpLevelWiz.SelectedValue = value; }
    }

    public int Spl_School
    {
      get { return Convert.ToInt32(DrpSchool.SelectedValue); }
      set { DrpSchool.SelectedValue = value; }
    }

    public int Spl_SubSchool
    {
      get { return Convert.ToInt32(DrpSubSchool.SelectedValue); }
      set { DrpSubSchool.SelectedValue = value; }
    }

    public bool Spl_Acid
    {
      get { return CbxAcid.IsChecked ?? false; }
      set { CbxAcid.IsChecked = value; }
    }

    public bool Spl_Air
    {
      get { return CbxAir.IsChecked ?? false; }
      set { CbxAir.IsChecked = value; }
    }

    public bool Spl_Chaotic
    {
      get { return CbxChaotic.IsChecked ?? false; }
      set { CbxChaotic.IsChecked = value; }
    }

    public bool Spl_Cold
    {
      get { return CbxCold.IsChecked ?? false; }
      set { CbxCold.IsChecked = value; }
    }

    public bool Spl_CostlyComponents
    {
      get { return CbxCostlyComponents.IsChecked ?? false; }
      set { CbxCostlyComponents.IsChecked = value; }
    }

    public bool Spl_Curse
    {
      get { return CbxCurse.IsChecked ?? false; }
      set { CbxCurse.IsChecked = value; }
    }

    public bool Spl_Darkness
    {
      get { return CbxDarkness.IsChecked ?? false; }
      set { CbxDarkness.IsChecked = value; }
    }

    public bool Spl_Death
    {
      get { return CbxDeath.IsChecked ?? false; }
      set { CbxDeath.IsChecked = value; }
    }

    public bool Spl_Disease
    {
      get { return CbxDisease.IsChecked ?? false; }
      set { CbxDisease.IsChecked = value; }
    }

    public bool Spl_Dismissable
    {
      get { return CbxDismissable.IsChecked ?? false; }
      set { CbxDismissable.IsChecked = value; }
    }

    public bool Spl_DivineFocus
    {
      get { return CbxDivineFocus.IsChecked ?? false; }
      set { CbxDivineFocus.IsChecked = value; }
    }

    public bool Spl_Earth
    {
      get { return CbxEarth.IsChecked ?? false; }
      set { CbxEarth.IsChecked = value; }
    }

    public bool Spl_Electricity
    {
      get { return CbxElectricity.IsChecked ?? false; }
      set { CbxElectricity.IsChecked = value; }
    }

    public bool Spl_Emotion
    {
      get { return CbxEmotion.IsChecked ?? false; }
      set { CbxEmotion.IsChecked = value; }
    }

    public bool Spl_Evil
    {
      get { return CbxEvil.IsChecked ?? false; }
      set { CbxEvil.IsChecked = value; }
    }

    public bool Spl_Fear
    {
      get { return CbxFear.IsChecked ?? false; }
      set { CbxFear.IsChecked = value; }
    }

    public bool Spl_Fire
    {
      get { return CbxFire.IsChecked ?? false; }
      set { CbxFire.IsChecked = value; }
    }

    public bool Spl_Focus
    {
      get { return CbxFocus.IsChecked ?? false; }
      set { CbxFocus.IsChecked = value; }
    }

    public bool Spl_Force
    {
      get { return CbxForce.IsChecked ?? false; }
      set { CbxForce.IsChecked = value; }
    }

    public bool Spl_Good
    {
      get { return CbxGood.IsChecked ?? false; }
      set { CbxGood.IsChecked = value; }
    }

    public bool Spl_LanguageDependent
    {
      get { return CbxLanguageDependent.IsChecked ?? false; }
      set { CbxLanguageDependent.IsChecked = value; }
    }

    public bool Spl_Lawful
    {
      get { return CbxLawful.IsChecked ?? false; }
      set { CbxLawful.IsChecked = value; }
    }

    public bool Spl_Light
    {
      get { return CbxLight.IsChecked ?? false; }
      set { CbxLight.IsChecked = value; }
    }

    public bool Spl_Material
    {
      get { return CbxMaterial.IsChecked ?? false; }
      set { CbxMaterial.IsChecked = value; }
    }

    public bool Spl_MindAffecting
    {
      get { return CbxMindAffecting.IsChecked ?? false; }
      set { CbxMindAffecting.IsChecked = value; }
    }

    public bool Spl_Mythic
    {
      get { return CbxMythic.IsChecked ?? false; }
      set { CbxMythic.IsChecked = value; }
    }

    public bool Spl_Pain
    {
      get { return CbxPain.IsChecked ?? false; }
      set { CbxPain.IsChecked = value; }
    }

    public bool Spl_Poison
    {
      get { return CbxPoison.IsChecked ?? false; }
      set { CbxPoison.IsChecked = value; }
    }

    public bool Spl_Shadow
    {
      get { return CbxShadow.IsChecked ?? false; }
      set { CbxShadow.IsChecked = value; }
    }

    public bool Spl_Shapeable
    {
      get { return CbxShapeable.IsChecked ?? false; }
      set { CbxShapeable.IsChecked = value; }
    }

    public bool Spl_Somatic
    {
      get { return CbxSomatic.IsChecked ?? false; }
      set { CbxSomatic.IsChecked = value; }
    }

    public bool Spl_Sonic
    {
      get { return CbxSonic.IsChecked ?? false; }
      set { CbxSonic.IsChecked = value; }
    }

    public bool Spl_Verbal
    {
      get { return CbxVerbal.IsChecked ?? false; }
      set { CbxVerbal.IsChecked = value; }
    }

    public bool Spl_Water
    {
      get { return CbxWater.IsChecked ?? false; }
      set { CbxWater.IsChecked = value; }
    }

    #endregion

    #region Private Variables

    private ObservableCollection<ListItemResult> SpellList;
    private ObservableCollection<ListItemResult> SpellSchoolList;
    private ObservableCollection<ListItemResult> SpellSubSchoolList;
    private List<DisplayResult> Mag_LevelList;

    private Spell ActiveSpell = new Spell();
    private SpellDetail ActiveSpellDetail = new SpellDetail();

    #endregion

    #region Constructor

    public SpellControl()
    {

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

      InitializeComponent();

      SpellList = new ObservableCollection<ListItemResult>(DBClient.GetList("Spell"));

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
      DrpSubSchool.ItemsSource = SpellSubSchoolList;
    }

    #endregion
  }
}
