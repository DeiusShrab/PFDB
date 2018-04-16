using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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

    public int Bes_AC
    {
      get { return Convert.ToInt32(Txt_Bes_AC.Text); }
      set { Txt_Bes_AC.Text = value.ToString(); }
    }

    public int Bes_ACFlat
    {
      get { return Convert.ToInt32(Txt_Bes_ACFlat.Text); }
      set { Txt_Bes_ACFlat.Text = value.ToString(); }
    }

    public int Bes_ACTouch
    {
      get { return Convert.ToInt32(Txt_Bes_ACTouch.Text); }
      set { Txt_Bes_ACTouch.Text = value.ToString(); }
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

    public int Bes_BAB
    {
      get { return Convert.ToInt32(Txt_Bes_BAB.Text); }
      set { Txt_Bes_BAB.Text = value.ToString(); }
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

    public int Bes_CHA
    {
      get { return Convert.ToInt32(Txt_Bes_CHA.Text); }
      set { Txt_Bes_CHA.Text = value.ToString(); }
    }

    public string Bes_Class
    {
      get { return Txt_Bes_Class.Text; }
      set { Txt_Bes_Class.Text = value; }
    }

    public int Bes_CMB
    {
      get { return Convert.ToInt32(Txt_Bes_CMB.Text); }
      set { Txt_Bes_CMB.Text = value.ToString(); }
    }

    public int Bes_CMD
    {
      get { return Convert.ToInt32(Txt_Bes_CMD.Text); }
      set { Txt_Bes_CMD.Text = value.ToString(); }
    }

    public int Bes_CON
    {
      get { return Convert.ToInt32(Txt_Bes_CON.Text); }
      set { Txt_Bes_CON.Text = value.ToString(); }
    }

    public int Bes_CR
    {
      get { return (int)Drp_Bes_CR.SelectedValue; }
      set { Drp_Bes_CR.SelectedValue = value; }
    }

    public string Bes_CreatureSource
    {
      get { return Txt_Bes_CreatureSource.Text; }
      set { Txt_Bes_CreatureSource.Text = value; }
    }

    public int Bes_DEX
    {
      get { return Convert.ToInt32(Txt_Bes_DEX.Text); }
      set { Txt_Bes_DEX.Text = value.ToString(); }
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

    public int Bes_Fort
    {
      get { return Convert.ToInt32(Txt_Bes_Fort.Text); }
      set { Txt_Bes_Fort.Text = value.ToString(); }
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

    public int Bes_HP
    {
      get { return Convert.ToInt32(Txt_Bes_HP.Text); }
      set { Txt_Bes_HP.Text = value.ToString(); }
    }

    public int Bes_Init
    {
      get { return Convert.ToInt32(Txt_Bes_Init.Text); }
      set { Txt_Bes_Init.Text = value.ToString(); }
    }

    public int Bes_INT
    {
      get { return Convert.ToInt32(Txt_Bes_INT.Text); }
      set { Txt_Bes_INT.Text = value.ToString(); }
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

    public int Bes_Ref
    {
      get { return Convert.ToInt32(Txt_Bes_Ref.Text); }
      set { Txt_Bes_Ref.Text = value.ToString(); }
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

    public int Bes_SpdBurrow
    {
      get { return Convert.ToInt32(Txt_Bes_SpdBurrow.Text); }
      set { Txt_Bes_SpdBurrow.Text = value.ToString(); }
    }

    public int Bes_SpdClimb
    {
      get { return Convert.ToInt32(Txt_Bes_SpdClimb.Text); }
      set { Txt_Bes_SpdClimb.Text = value.ToString(); }
    }

    public int Bes_SpdFly
    {
      get { return Convert.ToInt32(Txt_Bes_SpdFly.Text); }
      set { Txt_Bes_SpdFly.Text = value.ToString(); }
    }

    public int Bes_SpdLand
    {
      get { return Convert.ToInt32(Txt_Bes_SpdLand.Text); }
      set { Txt_Bes_SpdLand.Text = value.ToString(); }
    }

    public int Bes_SpdSwim
    {
      get { return Convert.ToInt32(Txt_Bes_SpdSwim.Text); }
      set { Txt_Bes_SpdSwim.Text = value.ToString(); }
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

    public int Bes_STR
    {
      get { return Convert.ToInt32(Txt_Bes_STR.Text); }
      set { Txt_Bes_STR.Text = value.ToString(); }
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

    public int Bes_Will
    {
      get { return Convert.ToInt32(Txt_Bes_Will.Text); }
      set { Txt_Bes_Will.Text = value.ToString(); }
    }

    public int Bes_WIS
    {
      get { return Convert.ToInt32(Txt_Bes_WIS.Text); }
      set { Txt_Bes_WIS.Text = value.ToString(); }
    }

    public int Bes_XP
    {
      get { return Convert.ToInt32(Txt_Bes_XP.Text); }
      set { Txt_Bes_XP.Text = value.ToString(); }
    }
    public string Msc_MonthName
    {
      get { return Txt_Msc_MonthName.Text; }
      set { Txt_Msc_MonthName.Text = value; }
    }

    public string Msc_PlaneName
    {
      get { return Txt_Msc_PlaneName.Text; }
      set { Txt_Msc_PlaneName.Text = value; }
    }

    public string Msc_SeasonName
    {
      get { return Txt_Msc_SeasonName.Text; }
      set { Txt_Msc_SeasonName.Text = value; }
    }

    public string Msc_TimeName
    {
      get { return Txt_Msc_TimeName.Text; }
      set { Txt_Msc_TimeName.Text = value; }
    }

    public string Spl_Bloodline
    {
      get { return Txt_Spl_Bloodline.Text; }
      set { Txt_Spl_Bloodline.Text = value; }
    }

    public string Spl_CastingTime
    {
      get { return Txt_Spl_CastingTime.Text; }
      set { Txt_Spl_CastingTime.Text = value; }
    }

    public string Spl_Duration
    {
      get { return Txt_Spl_Duration.Text; }
      set { Txt_Spl_Duration.Text = value; }
    }

    public string Spl_Effect
    {
      get { return Txt_Spl_Effect.Text; }
      set { Txt_Spl_Effect.Text = value; }
    }

    public string Spl_Fulltext
    {
      get { return Txt_Spl_Fulltext.Text; }
      set { Txt_Spl_Fulltext.Text = value; }
    }

    public string Spl_Materials
    {
      get { return Txt_Spl_Materials.Text; }
      set { Txt_Spl_Materials.Text = value; }
    }

    public string Spl_Name
    {
      get { return Txt_Spl_Name.Text; }
      set { Txt_Spl_Name.Text = value; }
    }

    public string Spl_Patron
    {
      get { return Txt_Spl_Patron.Text; }
      set { Txt_Spl_Patron.Text = value; }
    }

    public string Spl_Range
    {
      get { return Txt_Spl_Range.Text; }
      set { Txt_Spl_Range.Text = value; }
    }

    public string Spl_Save
    {
      get { return Txt_Spl_Save.Text; }
      set { Txt_Spl_Save.Text = value; }
    }

    public string Spl_Search
    {
      get { return Txt_Spl_Search.Text; }
      set { Txt_Spl_Search.Text = value; }
    }

    public string Spl_ShortDesc
    {
      get { return Txt_Spl_ShortDesc.Text; }
      set { Txt_Spl_ShortDesc.Text = value; }
    }

    public string Spl_SR
    {
      get { return Txt_Spl_SR.Text; }
      set { Txt_Spl_SR.Text = value; }
    }

    public string Spl_Targets
    {
      get { return Txt_Spl_Targets.Text; }
      set { Txt_Spl_Targets.Text = value; }
    }

    public int Bes_MR
    {
      get { return Int_Bes_MR.Value ?? 0; }
      set { Int_Bes_MR.Value = value; }
    }

    public int Bes_MT
    {
      get { return Int_Bes_MT.Value ?? 0; }
      set { Int_Bes_MT.Value = value; }
    }

    public int Bes_SkillBonus
    {
      get { return Int_Bes_SkillBonus.Value ?? 0; }
      set { Int_Bes_SkillBonus.Value = value; }
    }

    public int Msc_MonthDays
    {
      get { return Int_Msc_MonthDays.Value ?? 0; }
      set { Int_Msc_MonthDays.Value = value; }
    }

    public int Msc_MonthOrder
    {
      get { return Int_Msc_MonthOrder.Value ?? 0; }
      set { Int_Msc_MonthOrder.Value = value; }
    }

    public int Msc_SeasonOrder
    {
      get { return Int_Msc_SeasonOrder.Value ?? 0; }
      set { Int_Msc_SeasonOrder.Value = value; }
    }

    public int Msc_TimeOrder
    {
      get { return Int_Msc_TimeOrder.Value ?? 0; }
      set { Int_Msc_TimeOrder.Value = value; }
    }


    public int Bes_Type
    {
      get { return Convert.ToInt32(Drp_Bes_Type.SelectedValue); }
      set { Drp_Bes_Type.SelectedValue = value; }
    }

    public int Msc_MonthSeasonId
    {
      get { return Convert.ToInt32(Drp_Msc_MonthSeasonId.SelectedValue); }
      set { Drp_Msc_MonthSeasonId.SelectedValue = value; }
    }

    public int Spl_LevelAdept
    {
      get { return Convert.ToInt32(Drp_Spl_LevelAdept.SelectedValue); }
      set { Drp_Spl_LevelAdept.SelectedValue = value; }
    }

    public int Spl_LevelAlchemist
    {
      get { return Convert.ToInt32(Drp_Spl_LevelAlchemist.SelectedValue); }
      set { Drp_Spl_LevelAlchemist.SelectedValue = value; }
    }

    public int Spl_LevelAntipaladin
    {
      get { return Convert.ToInt32(Drp_Spl_LevelAntipaladin.SelectedValue); }
      set { Drp_Spl_LevelAntipaladin.SelectedValue = value; }
    }

    public int Spl_LevelBard
    {
      get { return Convert.ToInt32(Drp_Spl_LevelBard.SelectedValue); }
      set { Drp_Spl_LevelBard.SelectedValue = value; }
    }

    public int Spl_LevelBloodRager
    {
      get { return Convert.ToInt32(Drp_Spl_LevelBloodRager.SelectedValue); }
      set { Drp_Spl_LevelBloodRager.SelectedValue = value; }
    }

    public int Spl_LevelCleric
    {
      get { return Convert.ToInt32(Drp_Spl_LevelCleric.SelectedValue); }
      set { Drp_Spl_LevelCleric.SelectedValue = value; }
    }

    public int Spl_LevelDruid
    {
      get { return Convert.ToInt32(Drp_Spl_LevelDruid.SelectedValue); }
      set { Drp_Spl_LevelDruid.SelectedValue = value; }
    }

    public int Spl_LevelHunter
    {
      get { return Convert.ToInt32(Drp_Spl_LevelHunter.SelectedValue); }
      set { Drp_Spl_LevelHunter.SelectedValue = value; }
    }

    public int Spl_LevelInquisitor
    {
      get { return Convert.ToInt32(Drp_Spl_LevelInquisitor.SelectedValue); }
      set { Drp_Spl_LevelInquisitor.SelectedValue = value; }
    }

    public int Spl_LevelInvestigator
    {
      get { return Convert.ToInt32(Drp_Spl_LevelInvestigator.SelectedValue); }
      set { Drp_Spl_LevelInvestigator.SelectedValue = value; }
    }

    public int Spl_LevelMagus
    {
      get { return Convert.ToInt32(Drp_Spl_LevelMagus.SelectedValue); }
      set { Drp_Spl_LevelMagus.SelectedValue = value; }
    }

    public int Spl_LevelMedium
    {
      get { return Convert.ToInt32(Drp_Spl_LevelMedium.SelectedValue); }
      set { Drp_Spl_LevelMedium.SelectedValue = value; }
    }

    public int Spl_LevelMesmerist
    {
      get { return Convert.ToInt32(Drp_Spl_LevelMesmerist.SelectedValue); }
      set { Drp_Spl_LevelMesmerist.SelectedValue = value; }
    }

    public int Spl_LevelOccultist
    {
      get { return Convert.ToInt32(Drp_Spl_LevelOccultist.SelectedValue); }
      set { Drp_Spl_LevelOccultist.SelectedValue = value; }
    }

    public int Spl_LevelOracle
    {
      get { return Convert.ToInt32(Drp_Spl_LevelOracle.SelectedValue); }
      set { Drp_Spl_LevelOracle.SelectedValue = value; }
    }

    public int Spl_LevelPaladin
    {
      get { return Convert.ToInt32(Drp_Spl_LevelPaladin.SelectedValue); }
      set { Drp_Spl_LevelPaladin.SelectedValue = value; }
    }

    public int Spl_LevelPsychic
    {
      get { return Convert.ToInt32(Drp_Spl_LevelPsychic.SelectedValue); }
      set { Drp_Spl_LevelPsychic.SelectedValue = value; }
    }

    public int Spl_LevelRanger
    {
      get { return Convert.ToInt32(Drp_Spl_LevelRanger.SelectedValue); }
      set { Drp_Spl_LevelRanger.SelectedValue = value; }
    }

    public int Spl_LevelShaman
    {
      get { return Convert.ToInt32(Drp_Spl_LevelShaman.SelectedValue); }
      set { Drp_Spl_LevelShaman.SelectedValue = value; }
    }

    public int Spl_LevelSkald
    {
      get { return Convert.ToInt32(Drp_Spl_LevelSkald.SelectedValue); }
      set { Drp_Spl_LevelSkald.SelectedValue = value; }
    }

    public int Spl_LevelSor
    {
      get { return Convert.ToInt32(Drp_Spl_LevelSor.SelectedValue); }
      set { Drp_Spl_LevelSor.SelectedValue = value; }
    }

    public int Spl_LevelSpiritualist
    {
      get { return Convert.ToInt32(Drp_Spl_LevelSpiritualist.SelectedValue); }
      set { Drp_Spl_LevelSpiritualist.SelectedValue = value; }
    }

    public int Spl_LevelSummoner
    {
      get { return Convert.ToInt32(Drp_Spl_LevelSummoner.SelectedValue); }
      set { Drp_Spl_LevelSummoner.SelectedValue = value; }
    }

    public int Spl_LevelWitch
    {
      get { return Convert.ToInt32(Drp_Spl_LevelWitch.SelectedValue); }
      set { Drp_Spl_LevelWitch.SelectedValue = value; }
    }

    public int Spl_LevelWiz
    {
      get { return Convert.ToInt32(Drp_Spl_LevelWiz.SelectedValue); }
      set { Drp_Spl_LevelWiz.SelectedValue = value; }
    }

    public int Spl_School
    {
      get { return Convert.ToInt32(Drp_Spl_School.SelectedValue); }
      set { Drp_Spl_School.SelectedValue = value; }
    }

    public int Spl_SubSchool
    {
      get { return Convert.ToInt32(Drp_Spl_SubSchool.SelectedValue); }
      set { Drp_Spl_SubSchool.SelectedValue = value; }
    }

    public bool Bes_Companion
    {
      get { return Cbx_Bes_Companion.IsChecked ?? false; }
      set { Cbx_Bes_Companion.IsChecked = value; }
    }

    public bool Bes_DontUseRacialHD
    {
      get { return Cbx_Bes_DontUseRacialHD.IsChecked ?? false; }
      set { Cbx_Bes_DontUseRacialHD.IsChecked = value; }
    }

    public bool Bes_Mythic
    {
      get { return Cbx_Bes_Mythic.IsChecked ?? false; }
      set { Cbx_Bes_Mythic.IsChecked = value; }
    }

    public bool Bes_NPC
    {
      get { return Cbx_Bes_NPC.IsChecked ?? false; }
      set { Cbx_Bes_NPC.IsChecked = value; }
    }

    public bool Bes_Template
    {
      get { return Cbx_Bes_Template.IsChecked ?? false; }
      set { Cbx_Bes_Template.IsChecked = value; }
    }

    public bool Bes_UniqueCreature
    {
      get { return Cbx_Bes_UniqueCreature.IsChecked ?? false; }
      set { Cbx_Bes_UniqueCreature.IsChecked = value; }
    }

    public bool Msc_TimeNight
    {
      get { return Cbx_Msc_TimeNight.IsChecked ?? false; }
      set { Cbx_Msc_TimeNight.IsChecked = value; }
    }

    public bool Spl_Acid
    {
      get { return Cbx_Spl_Acid.IsChecked ?? false; }
      set { Cbx_Spl_Acid.IsChecked = value; }
    }

    public bool Spl_Air
    {
      get { return Cbx_Spl_Air.IsChecked ?? false; }
      set { Cbx_Spl_Air.IsChecked = value; }
    }

    public bool Spl_Chaotic
    {
      get { return Cbx_Spl_Chaotic.IsChecked ?? false; }
      set { Cbx_Spl_Chaotic.IsChecked = value; }
    }

    public bool Spl_Cold
    {
      get { return Cbx_Spl_Cold.IsChecked ?? false; }
      set { Cbx_Spl_Cold.IsChecked = value; }
    }

    public bool Spl_CostlyComponents
    {
      get { return Cbx_Spl_CostlyComponents.IsChecked ?? false; }
      set { Cbx_Spl_CostlyComponents.IsChecked = value; }
    }

    public bool Spl_Curse
    {
      get { return Cbx_Spl_Curse.IsChecked ?? false; }
      set { Cbx_Spl_Curse.IsChecked = value; }
    }

    public bool Spl_Darkness
    {
      get { return Cbx_Spl_Darkness.IsChecked ?? false; }
      set { Cbx_Spl_Darkness.IsChecked = value; }
    }

    public bool Spl_Death
    {
      get { return Cbx_Spl_Death.IsChecked ?? false; }
      set { Cbx_Spl_Death.IsChecked = value; }
    }

    public bool Spl_Disease
    {
      get { return Cbx_Spl_Disease.IsChecked ?? false; }
      set { Cbx_Spl_Disease.IsChecked = value; }
    }

    public bool Spl_Dismissable
    {
      get { return Cbx_Spl_Dismissable.IsChecked ?? false; }
      set { Cbx_Spl_Dismissable.IsChecked = value; }
    }

    public bool Spl_DivineFocus
    {
      get { return Cbx_Spl_DivineFocus.IsChecked ?? false; }
      set { Cbx_Spl_DivineFocus.IsChecked = value; }
    }

    public bool Spl_Earth
    {
      get { return Cbx_Spl_Earth.IsChecked ?? false; }
      set { Cbx_Spl_Earth.IsChecked = value; }
    }

    public bool Spl_Electricity
    {
      get { return Cbx_Spl_Electricity.IsChecked ?? false; }
      set { Cbx_Spl_Electricity.IsChecked = value; }
    }

    public bool Spl_Emotion
    {
      get { return Cbx_Spl_Emotion.IsChecked ?? false; }
      set { Cbx_Spl_Emotion.IsChecked = value; }
    }

    public bool Spl_Evil
    {
      get { return Cbx_Spl_Evil.IsChecked ?? false; }
      set { Cbx_Spl_Evil.IsChecked = value; }
    }

    public bool Spl_Fear
    {
      get { return Cbx_Spl_Fear.IsChecked ?? false; }
      set { Cbx_Spl_Fear.IsChecked = value; }
    }

    public bool Spl_Fire
    {
      get { return Cbx_Spl_Fire.IsChecked ?? false; }
      set { Cbx_Spl_Fire.IsChecked = value; }
    }

    public bool Spl_Focus
    {
      get { return Cbx_Spl_Focus.IsChecked ?? false; }
      set { Cbx_Spl_Focus.IsChecked = value; }
    }

    public bool Spl_Force
    {
      get { return Cbx_Spl_Force.IsChecked ?? false; }
      set { Cbx_Spl_Force.IsChecked = value; }
    }

    public bool Spl_Good
    {
      get { return Cbx_Spl_Good.IsChecked ?? false; }
      set { Cbx_Spl_Good.IsChecked = value; }
    }

    public bool Spl_LanguageDependent
    {
      get { return Cbx_Spl_LanguageDependent.IsChecked ?? false; }
      set { Cbx_Spl_LanguageDependent.IsChecked = value; }
    }

    public bool Spl_Lawful
    {
      get { return Cbx_Spl_Lawful.IsChecked ?? false; }
      set { Cbx_Spl_Lawful.IsChecked = value; }
    }

    public bool Spl_Light
    {
      get { return Cbx_Spl_Light.IsChecked ?? false; }
      set { Cbx_Spl_Light.IsChecked = value; }
    }

    public bool Spl_Material
    {
      get { return Cbx_Spl_Material.IsChecked ?? false; }
      set { Cbx_Spl_Material.IsChecked = value; }
    }

    public bool Spl_MindAffecting
    {
      get { return Cbx_Spl_MindAffecting.IsChecked ?? false; }
      set { Cbx_Spl_MindAffecting.IsChecked = value; }
    }

    public bool Spl_Mythic
    {
      get { return Cbx_Spl_Mythic.IsChecked ?? false; }
      set { Cbx_Spl_Mythic.IsChecked = value; }
    }

    public bool Spl_Pain
    {
      get { return Cbx_Spl_Pain.IsChecked ?? false; }
      set { Cbx_Spl_Pain.IsChecked = value; }
    }

    public bool Spl_Poison
    {
      get { return Cbx_Spl_Poison.IsChecked ?? false; }
      set { Cbx_Spl_Poison.IsChecked = value; }
    }

    public bool Spl_Shadow
    {
      get { return Cbx_Spl_Shadow.IsChecked ?? false; }
      set { Cbx_Spl_Shadow.IsChecked = value; }
    }

    public bool Spl_Shapeable
    {
      get { return Cbx_Spl_Shapeable.IsChecked ?? false; }
      set { Cbx_Spl_Shapeable.IsChecked = value; }
    }

    public bool Spl_Somatic
    {
      get { return Cbx_Spl_Somatic.IsChecked ?? false; }
      set { Cbx_Spl_Somatic.IsChecked = value; }
    }

    public bool Spl_Sonic
    {
      get { return Cbx_Spl_Sonic.IsChecked ?? false; }
      set { Cbx_Spl_Sonic.IsChecked = value; }
    }

    public bool Spl_Verbal
    {
      get { return Cbx_Spl_Verbal.IsChecked ?? false; }
      set { Cbx_Spl_Verbal.IsChecked = value; }
    }

    public bool Spl_Water
    {
      get { return Cbx_Spl_Water.IsChecked ?? false; }
      set { Cbx_Spl_Water.IsChecked = value; }
    }

    public int Bes_BestiaryId
    {
      get { return (int)Lbl_Bes_BestiaryId.Content; }
      set { Lbl_Bes_BestiaryId.Content = value; }
    }

    public int Msc_MonthId
    {
      get { return Convert.ToInt32(Lbl_Msc_MonthId.Content); }
      set { Lbl_Msc_MonthId.Content = value; }
    }

    public int Msc_PlaneId
    {
      get { return Convert.ToInt32(Lbl_Msc_PlaneId.Content); }
      set { Lbl_Msc_PlaneId.Content = value; }
    }

    public int Msc_SeasonId
    {
      get { return Convert.ToInt32(Lbl_Msc_SeasonId.Content); }
      set { Lbl_Msc_SeasonId.Content = value; }
    }

    public int Msc_TimeId
    {
      get { return Convert.ToInt32(Lbl_Msc_TimeId.Content); }
      set { Lbl_Msc_TimeId.Content = value; }
    }

    #endregion

    #region Private Variables

    private List<DisplayResult> Mag_LevelList;
    private List<DisplayResult> CR_List;

    private ObservableCollection<ListItemResult> BestiaryList;
    private ObservableCollection<ListItemResult> BestiaryEnvironmentList;
    private ObservableCollection<ListItemResult> BestiaryFeatList;
    private ObservableCollection<ListItemResult> BestiaryLanguageList;
    private ObservableCollection<ListItemResult> BestiarySkillList;
    private ObservableCollection<ListItemResult> BestiaryTypeList;
    private ObservableCollection<ListItemResult> ContinentList;
    private ObservableCollection<ListItemResult> ContinentWeatherList;
    private ObservableCollection<ListItemResult> EnvironmentList;
    private ObservableCollection<ListItemResult> FactionList;
    private ObservableCollection<ListItemResult> FeatList;
    private ObservableCollection<ListItemResult> LanguageList;
    private ObservableCollection<ListItemResult> LocationList;
    private ObservableCollection<ListItemResult> MagicItemList;
    private ObservableCollection<ListItemResult> MonsterSpawnList;
    private ObservableCollection<ListItemResult> MonthList;
    private ObservableCollection<ListItemResult> SeasonList;
    private ObservableCollection<ListItemResult> SpellList;
    private ObservableCollection<ListItemResult> SpellDetailList;
    private ObservableCollection<ListItemResult> TerritoryList;
    private ObservableCollection<ListItemResult> WeatherList;
    private ObservableCollection<ListItemResult> TerrainList;
    private ObservableCollection<ListItemResult> TimeList;
    private ObservableCollection<ListItemResult> TrackedEventList;
    private ObservableCollection<ListItemResult> PlaneList;
    private ObservableCollection<ListItemResult> SkillList;
    private ObservableCollection<ListItemResult> SpellSchoolList;
    private ObservableCollection<ListItemResult> SpellSubSchoolList;

    private ObservableCollection<ListItemResult> BestiaryList_Filter;
    private ObservableCollection<ListItemResult> MonsterSpawnList_Filter;

    private Bestiary ActiveBestiary = new Bestiary();
    private BestiaryDetail ActiveBestiaryDetail = new BestiaryDetail();
    private BestiaryEnvironment ActiveBestiaryEnvironment = new BestiaryEnvironment();
    private BestiaryFeat ActiveBestiaryFeat = new BestiaryFeat();
    private BestiaryLanguage ActiveBestiaryLanguage = new BestiaryLanguage();
    private BestiarySkill ActiveBestiarySkill = new BestiarySkill();
    private BestiarySubType ActiveBestiarySubType = new BestiarySubType();
    private BestiaryType ActiveBestiaryType = new BestiaryType();
    private Continent ActiveContinent = new Continent();
    private ContinentWeather ActiveContinentWeather = new ContinentWeather();
    private DBConnect.DBModels.Environment ActiveEnvironment = new DBConnect.DBModels.Environment();
    private Faction ActiveFaction = new Faction();
    private Feat ActiveFeat = new Feat();
    private Language ActiveLanguage = new Language();
    private Location ActiveLocation = new Location();
    private MagicItem ActiveMagicItem = new MagicItem();
    private Month ActiveMonth = new Month();
    private Plane ActivePlane = new Plane();
    private Season ActiveSeason = new Season();
    private Skill ActiveSkill = new Skill();
    private Spell ActiveSpell = new Spell();
    private SpellDetail ActiveSpellDetail = new SpellDetail();
    private Terrain ActiveTerrain = new Terrain();
    private Territory ActiveTerritory = new Territory();
    private Time ActiveTime = new Time();
    private TrackedEvent ActiveTrackedEvent = new TrackedEvent();
    private Weather ActiveWeather = new Weather();
    private List<MonsterSpawn> ActiveSpawns = new List<MonsterSpawn>();
    private int MonsterSpawnBestiaryId;

    private bool bes_sortNameAsc = true;
    private bool bes_sortCrAsc = true;
    private bool mon_sortNameAsc = true;
    private bool mon_sortTypeAsc = true;

    #endregion

    #region Constructor

    public EditorWindow()
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

      CR_List = new List<DisplayResult>();
      for (int i = -4; i < 32; i++)
      {
        CR_List.Add(new DisplayResult() { Display = ParseCR(i), Result = i });
      }

      InitializeComponent();

      LoadDBData();

      Lbx_Bes_Bestiary.DisplayMemberPath = Lbx_Bes_FeatsAll.DisplayMemberPath = Lbx_Bes_FeatsAssigned.DisplayMemberPath = Lbx_Bes_SkillsAll.DisplayMemberPath =
        Lbx_Bes_SkillsAssigned.DisplayMemberPath = Lbx_Bes_SpellAbilities.DisplayMemberPath = Lbx_Bes_SpellsKnown.DisplayMemberPath = Lbx_Bes_SpellsPrepared.DisplayMemberPath =
        Lbx_Con_Continent.DisplayMemberPath = Lbx_Mon_SpawnList.DisplayMemberPath = Lbx_Msc_Month.DisplayMemberPath = Lbx_Msc_Plane.DisplayMemberPath = Lbx_Msc_Season.DisplayMemberPath =
        Lbx_Msc_Time.DisplayMemberPath = Lbx_Spl_Spell.DisplayMemberPath = Lbx_Ter_Terrain.DisplayMemberPath = Lbx_Wth_Weather.DisplayMemberPath = "Name";

      Lbx_Bes_Bestiary.SelectedValuePath = Lbx_Bes_FeatsAll.SelectedValuePath = Lbx_Bes_FeatsAssigned.SelectedValuePath = Lbx_Bes_SkillsAll.SelectedValuePath =
        Lbx_Bes_SkillsAssigned.SelectedValuePath = Lbx_Bes_SpellAbilities.SelectedValuePath = Lbx_Bes_SpellsKnown.SelectedValuePath = Lbx_Bes_SpellsPrepared.SelectedValuePath =
        Lbx_Con_Continent.SelectedValuePath = Lbx_Mon_SpawnList.SelectedValuePath = Lbx_Msc_Month.SelectedValuePath = Lbx_Msc_Plane.SelectedValuePath = Lbx_Msc_Season.SelectedValuePath =
        Lbx_Msc_Time.SelectedValuePath = Lbx_Spl_Spell.SelectedValuePath = Lbx_Ter_Terrain.SelectedValuePath = Lbx_Wth_Weather.SelectedValuePath = "Id";

      Drp_Bes_CR.DisplayMemberPath = Drp_Spl_LevelAdept.DisplayMemberPath = Drp_Spl_LevelAlchemist.DisplayMemberPath = Drp_Spl_LevelAntipaladin.DisplayMemberPath =
        Drp_Spl_LevelBard.DisplayMemberPath = Drp_Spl_LevelBloodRager.DisplayMemberPath = Drp_Spl_LevelCleric.DisplayMemberPath = Drp_Spl_LevelDruid.DisplayMemberPath =
        Drp_Spl_LevelHunter.DisplayMemberPath = Drp_Spl_LevelInquisitor.DisplayMemberPath = Drp_Spl_LevelInvestigator.DisplayMemberPath = Drp_Spl_LevelMagus.DisplayMemberPath =
        Drp_Spl_LevelMedium.DisplayMemberPath = Drp_Spl_LevelMesmerist.DisplayMemberPath = Drp_Spl_LevelOccultist.DisplayMemberPath = Drp_Spl_LevelOracle.DisplayMemberPath =
        Drp_Spl_LevelPaladin.DisplayMemberPath = Drp_Spl_LevelPsychic.DisplayMemberPath = Drp_Spl_LevelRanger.DisplayMemberPath = Drp_Spl_LevelShaman.DisplayMemberPath =
        Drp_Spl_LevelSkald.DisplayMemberPath = Drp_Spl_LevelSor.DisplayMemberPath = Drp_Spl_LevelSpiritualist.DisplayMemberPath = Drp_Spl_LevelSummoner.DisplayMemberPath =
        Drp_Spl_LevelWitch.DisplayMemberPath = Drp_Spl_LevelWiz.DisplayMemberPath = Drp_Bes_Type.DisplayMemberPath = "Display";

      Drp_Bes_CR.SelectedValuePath = Drp_Spl_LevelAdept.SelectedValuePath = Drp_Spl_LevelAlchemist.SelectedValuePath = Drp_Spl_LevelAntipaladin.SelectedValuePath =
        Drp_Spl_LevelBard.SelectedValuePath = Drp_Spl_LevelBloodRager.SelectedValuePath = Drp_Spl_LevelCleric.SelectedValuePath = Drp_Spl_LevelDruid.SelectedValuePath =
        Drp_Spl_LevelHunter.SelectedValuePath = Drp_Spl_LevelInquisitor.SelectedValuePath = Drp_Spl_LevelInvestigator.SelectedValuePath = Drp_Spl_LevelMagus.SelectedValuePath =
        Drp_Spl_LevelMedium.SelectedValuePath = Drp_Spl_LevelMesmerist.SelectedValuePath = Drp_Spl_LevelOccultist.SelectedValuePath = Drp_Spl_LevelOracle.SelectedValuePath =
        Drp_Spl_LevelPaladin.SelectedValuePath = Drp_Spl_LevelPsychic.SelectedValuePath = Drp_Spl_LevelRanger.SelectedValuePath = Drp_Spl_LevelShaman.SelectedValuePath =
        Drp_Spl_LevelSkald.SelectedValuePath = Drp_Spl_LevelSor.SelectedValuePath = Drp_Spl_LevelSpiritualist.SelectedValuePath = Drp_Spl_LevelSummoner.SelectedValuePath =
        Drp_Spl_LevelWitch.SelectedValuePath = Drp_Spl_LevelWiz.SelectedValuePath = Drp_Bes_Type.SelectedValuePath = "Result";

      Lbx_Bes_Bestiary.ItemsSource = BestiaryList;
      Lbx_Bes_FeatsAll.ItemsSource = FeatList;
      Lbx_Bes_SkillsAll.ItemsSource = SkillList;
      Lbx_Con_Continent.ItemsSource = ContinentList;
      Lbx_Mon_SpawnList.ItemsSource = BestiaryList;
      Lbx_Msc_Month.ItemsSource = MonthList;
      Lbx_Msc_Plane.ItemsSource = PlaneList;
      Lbx_Msc_Season.ItemsSource = SeasonList;
      Lbx_Msc_Time.ItemsSource = TimeList;
      Lbx_Spl_Spell.ItemsSource = SpellList;
      Lbx_Ter_Terrain.ItemsSource = TerrainList;
      Lbx_Wth_Weather.ItemsSource = WeatherList;

      Drp_Msc_MonthSeasonId.ItemsSource = SeasonList;
      Drp_Bes_Type.ItemsSource = BestiaryTypeList;
      Drp_Spl_LevelAdept.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelAlchemist.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelAntipaladin.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelBard.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelBloodRager.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelCleric.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelDruid.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelHunter.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelInquisitor.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelInvestigator.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelMagus.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelMedium.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelMesmerist.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelOccultist.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelOracle.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelPaladin.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelPsychic.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelRanger.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelShaman.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelSkald.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelSor.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelSpiritualist.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelSummoner.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelWitch.ItemsSource = Mag_LevelList;
      Drp_Spl_LevelWiz.ItemsSource = Mag_LevelList;

      Drp_Spl_School.ItemsSource = SpellSchoolList;
      Drp_Spl_SubSchool.ItemsSource = SpellSubSchoolList;
      Drp_Bes_Type.ItemsSource = BestiaryTypeList;
      Drp_Bes_CR.ItemsSource = CR_List;
    }

    private void LoadDBData()
    {
      try
      {
        BestiaryList = new ObservableCollection<ListItemResult>(DBClient.GetList("Bestiary"));
        BestiaryEnvironmentList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiaryEnvironment"));
        BestiaryFeatList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiaryFeat"));
        BestiaryLanguageList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiaryLanguage"));
        BestiarySkillList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiarySkill"));
        BestiaryTypeList = new ObservableCollection<ListItemResult>(DBClient.GetList("BestiaryType"));
        ContinentList = new ObservableCollection<ListItemResult>(DBClient.GetList("Continent"));
        ContinentWeatherList = new ObservableCollection<ListItemResult>(DBClient.GetList("ContinentWeather"));
        EnvironmentList = new ObservableCollection<ListItemResult>(DBClient.GetList("Environment"));
        FactionList = new ObservableCollection<ListItemResult>(DBClient.GetList("Faction"));
        FeatList = new ObservableCollection<ListItemResult>(DBClient.GetList("Feat"));
        LanguageList = new ObservableCollection<ListItemResult>(DBClient.GetList("Language"));
        LocationList = new ObservableCollection<ListItemResult>(DBClient.GetList("Location"));
        MagicItemList = new ObservableCollection<ListItemResult>(DBClient.GetList("MagicItem"));
        MonsterSpawnList = new ObservableCollection<ListItemResult>(DBClient.GetList("MonsterSpawn"));
        MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
        SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
        SpellList = new ObservableCollection<ListItemResult>(DBClient.GetList("Spell"));
        SpellDetailList = new ObservableCollection<ListItemResult>(DBClient.GetList("SpellDetail"));
        TerritoryList = new ObservableCollection<ListItemResult>(DBClient.GetList("Territory"));
        WeatherList = new ObservableCollection<ListItemResult>(DBClient.GetList("Weather"));
        TerrainList = new ObservableCollection<ListItemResult>(DBClient.GetList("Terrain"));
        TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
        TrackedEventList = new ObservableCollection<ListItemResult>(DBClient.GetList("TrackedEvent"));
        PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));
        SkillList = new ObservableCollection<ListItemResult>(DBClient.GetList("Skill"));
      }
      catch (System.Exception ex)
      {
        MessageBox.Show("Unable to load DB data. Error:\n" + ex.Message);
      }
    }

    #endregion

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

    #endregion

    #region Misc Tab

    private void Btn_Misc_TimeAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveTime = new Time();
      Msc_TimeId = 0;
      Msc_TimeName = string.Empty;
      Msc_TimeNight = false;
      Msc_TimeOrder = 0;
    }

    private void Btn_Misc_SeasonAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveSeason = new Season();
      Msc_SeasonId = 0;
      Msc_SeasonName = string.Empty;
      Msc_SeasonOrder = 0;
    }

    private void Btn_Misc_PlaneAdd_Click(object sender, RoutedEventArgs e)
    {
      ActivePlane = new Plane();
      Msc_PlaneId = 0;
      Msc_PlaneName = string.Empty;
    }

    private void Btn_Misc_MonthAdd_Click(object sender, RoutedEventArgs e)
    {
      ActiveMonth = new Month();
      Msc_MonthDays = 0;
      Msc_MonthId = 0;
      Msc_MonthName = string.Empty;
      Msc_MonthOrder = 0;
      Msc_MonthSeasonId = 0;
    }

    private void Lbx_Msc_Time_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Msc_Time.SelectedItem != null)
      {
        ActiveTime = DBClient.GetTime((int)Lbx_Msc_Time.SelectedValue);
        Msc_TimeId = ActiveTime.TimeId;
        Msc_TimeName = ActiveTime.Name;
        Msc_TimeNight = ActiveTime.IsNight;
        Msc_TimeOrder = ActiveTime.TimeOrder;
      }
    }

    private void Lbx_Msc_Season_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Msc_Season.SelectedItem != null)
      {
        ActiveSeason = DBClient.GetSeason((int)Lbx_Msc_Season.SelectedValue);
        Msc_SeasonId = ActiveSeason.SeasonId;
        Msc_SeasonName = ActiveSeason.Name;
        Msc_SeasonOrder = ActiveSeason.Order;
      }
    }

    private void Lbx_Msc_Plane_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Msc_Plane.SelectedItem != null)
      {
        ActivePlane = DBClient.GetPlane((int)Lbx_Msc_Plane.SelectedValue);
        Msc_PlaneId = ActivePlane.PlaneId;
        Msc_PlaneName = ActivePlane.Name;
      }
    }

    private void Lbx_Msc_Month_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Msc_Month.SelectedItem != null)
      {
        ActiveMonth = DBClient.GetMonth((int)Lbx_Msc_Month.SelectedValue);
        Msc_MonthDays = ActiveMonth.Days;
        Msc_MonthId = ActiveMonth.MonthId;
        Msc_MonthName = ActiveMonth.Name;
        Msc_MonthOrder = ActiveMonth.Order;
        Msc_MonthSeasonId = ActiveMonth.SeasonId;
      }
    }

    private void Btn_Misc_TimeSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveTime.TimeId == 0)
        ActiveTime.TimeId = DBClient.CreateTime(ActiveTime);
      else
        DBClient.UpdateTime(ActiveTime);

      Msc_TimeId = ActiveTime.TimeId;
      TimeList = new ObservableCollection<ListItemResult>(DBClient.GetList("Time"));
    }

    private void Btn_Misc_SeasonSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveSeason.SeasonId == 0)
        ActiveSeason.SeasonId = DBClient.CreateSeason(ActiveSeason);
      else
        DBClient.UpdateSeason(ActiveSeason);

      Msc_SeasonId = ActiveSeason.SeasonId;
      SeasonList = new ObservableCollection<ListItemResult>(DBClient.GetList("Season"));
    }

    private void Btn_Misc_PlaneSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActivePlane.PlaneId == 0)
        ActivePlane.PlaneId = DBClient.CreatePlane(ActivePlane);
      else
        DBClient.UpdatePlane(ActivePlane);

      Msc_PlaneId = ActivePlane.PlaneId;
      PlaneList = new ObservableCollection<ListItemResult>(DBClient.GetList("Plane"));
    }

    private void Btn_Misc_MonthSave_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveMonth.MonthId == 0)
        ActiveMonth.MonthId = DBClient.CreateMonth(ActiveMonth);
      else
        DBClient.UpdateMonth(ActiveMonth);

      Msc_MonthId = ActiveMonth.MonthId;
      MonthList = new ObservableCollection<ListItemResult>(DBClient.GetList("Month"));
    }


    #endregion

    #region Bestiary Tab

    private void Lbx_Bes_Bestiary_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Bes_Bestiary.SelectedItem != null)
      {
        ActiveBestiary = DBClient.GetBestiary((int)Lbx_Bes_Bestiary.SelectedValue);
        ActiveBestiaryDetail = DBClient.GetBestiaryDetail((int)Lbx_Bes_Bestiary.SelectedValue);

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
    }

    private void Btn_Bes_AddNew_Click(object sender, RoutedEventArgs e)
    {
      ActiveBestiary = new Bestiary();
      ActiveBestiaryDetail = new BestiaryDetail();

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

    private void Btn_Bes_Save_Click(object sender, RoutedEventArgs e)
    {
      if (ActiveBestiary.BestiaryId == 0)
        ActiveBestiary.BestiaryId = DBClient.CreateBestiary(ActiveBestiary);
      else
        DBClient.UpdateBestiary(ActiveBestiary);

      ActiveBestiaryDetail.BestiaryId = ActiveBestiary.BestiaryId;
      DBClient.UpdateBestiaryDetail(ActiveBestiaryDetail);

      BestiaryList = new ObservableCollection<ListItemResult>(DBClient.GetList("Bestiary"));
    }

    private void Btn_Bes_SortName_Click(object sender, RoutedEventArgs e)
    {
      if (bes_sortNameAsc)
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Name));
      else
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Name));

      bes_sortNameAsc = !bes_sortNameAsc;
      bes_sortCrAsc = true;
    }

    private void Btn_Bes_SortCR_Click(object sender, RoutedEventArgs e)
    {
      if (bes_sortCrAsc)
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderBy(x => x.Notes));
      else
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList_Filter.OrderByDescending(x => x.Notes));

      bes_sortCrAsc = !bes_sortCrAsc;
      bes_sortNameAsc = true;
    }

    #endregion

    private void UpdateMonsterSpawnGrid()
    {
      var spawnList = DBClient.GetMonsterSpawns(MonsterSpawnBestiaryId);

      var dt = new DataTable();
      foreach (var item in ContinentList)
      {
        dt.Columns.Add(item.Name, typeof(bool));
      }

    }

    
    private void Txt_Mon_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(Txt_Mon_Search.Text))
      {
        MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList);
      }
      else if (Txt_Mon_Search.Text.Length >= 3)
      {
        if (Rad_Mon_SearchName.IsChecked == true)
          MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList.Where(x => x.Name.ToLower().Contains(Txt_Mon_Search.Text.ToLower())));
        else
          MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList.Where(x => x.Notes.ToLower().Contains(Txt_Mon_Search.Text.ToLower())));
      }
    }

    private void Btn_Mon_SortName_Click(object sender, RoutedEventArgs e)
    {
      if (mon_sortNameAsc)
        MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList_Filter.OrderBy(x => x.Name));
      else
        MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList_Filter.OrderByDescending(x => x.Name));

      mon_sortNameAsc = !mon_sortNameAsc;
      mon_sortTypeAsc = true;
    }

    private void Txt_Bes_Search_TextChanged(object sender, TextChangedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(Txt_Bes_Search.Text))
      {
        BestiaryList_Filter = new ObservableCollection<ListItemResult>(BestiaryList);
      }
      else if (Txt_Bes_Search.Text.Length >= 3)
      {
        MonsterSpawnList_Filter = new ObservableCollection<ListItemResult>(MonsterSpawnList_Filter.Where(x => x.Name.ToLower().Contains(Txt_Bes_Search.Text.ToLower())));
      }
    }

    private void Lbx_Mon_SpawnList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (Lbx_Mon_SpawnList.SelectedItem != null)
      {
        MonsterSpawnBestiaryId = (int)Lbx_Mon_SpawnList.SelectedValue;
        UpdateMonsterSpawnGrid();
      }
    }
  }
}
