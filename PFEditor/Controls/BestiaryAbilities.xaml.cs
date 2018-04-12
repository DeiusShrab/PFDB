using System;
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

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for BestiaryAbilities.xaml
  /// </summary>
  public partial class BestiaryAbilities : UserControl
  {
    #region Interface Variables

    public string Bes_ACMods
    {
      get { return Txt_Bes_ACMods.Text; }
      set { Txt_Bes_ACMods.Text = value; }
    }

    public string Bes_Aura
    {
      get { return Txt_Bes_Aura.Text; }
      set { Txt_Bes_Aura.Text = value; }
    }

    public string Bes_Defense
    {
      get { return Txt_Bes_Defense.Text; }
      set { Txt_Bes_Defense.Text = value; }
    }

    public string Bes_Immune
    {
      get { return Txt_Bes_Immune.Text; }
      set { Txt_Bes_Immune.Text = value; }
    }

    public string Bes_Languages
    {
      get { return Txt_Bes_Languages.Text; }
      set { Txt_Bes_Languages.Text = value; }
    }

    public string Bes_Offense
    {
      get { return Txt_Bes_Offense.Text; }
      set { Txt_Bes_Offense.Text = value; }
    }

    public string Bes_OffenseNote
    {
      get { return Txt_Bes_OffenseNote.Text; }
      set { Txt_Bes_OffenseNote.Text = value; }
    }

    public string Bes_RacialMods
    {
      get { return Txt_Bes_RacialMods.Text; }
      set { Txt_Bes_RacialMods.Text = value; }
    }

    public string Bes_Reach
    {
      get { return Txt_Bes_Reach.Text; }
      set { Txt_Bes_Reach.Text = value; }
    }

    public string Bes_Resist
    {
      get { return Txt_Bes_Resist.Text; }
      set { Txt_Bes_Resist.Text = value; }
    }

    public string Bes_SaveMods
    {
      get { return Txt_Bes_SaveMods.Text; }
      set { Txt_Bes_SaveMods.Text = value; }
    }

    public string Bes_Senses
    {
      get { return Txt_Bes_Senses.Text; }
      set { Txt_Bes_Senses.Text = value; }
    }

    public string Bes_Space
    {
      get { return Txt_Bes_Space.Text; }
      set { Txt_Bes_Space.Text = value; }
    }

    public string Bes_SpecialAbilities
    {
      get { return Txt_Bes_SpecialAbilities.Text; }
      set { Txt_Bes_SpecialAbilities.Text = value; }
    }

    public string Bes_SpecialQualities
    {
      get { return Txt_Bes_SpecialQualities.Text; }
      set { Txt_Bes_SpecialQualities.Text = value; }
    }

    public string Bes_SpeedMods
    {
      get { return Txt_Bes_SpeedMods.Text; }
      set { Txt_Bes_SpeedMods.Text = value; }
    }

    #endregion

    public BestiaryAbilities()
    {
      InitializeComponent();
    }
  }
}
