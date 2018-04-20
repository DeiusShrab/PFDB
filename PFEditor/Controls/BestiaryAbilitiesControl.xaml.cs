using System.Windows.Controls;

namespace PFEditor.Controls
{
  /// <summary>
  /// Interaction logic for BestiaryAbilities.xaml
  /// </summary>
  public partial class BestiaryAbilitiesControl : UserControl
  {
    #region Interface Properties

    public string ACMods
    {
      get { return Txt_ACMods.Text; }
      set { Txt_ACMods.Text = value; }
    }

    public string Aura
    {
      get { return Txt_Aura.Text; }
      set { Txt_Aura.Text = value; }
    }

    public string Defense
    {
      get { return Txt_Defense.Text; }
      set { Txt_Defense.Text = value; }
    }

    public string Immune
    {
      get { return Txt_Immune.Text; }
      set { Txt_Immune.Text = value; }
    }

    public string Languages
    {
      get { return Txt_Languages.Text; }
      set { Txt_Languages.Text = value; }
    }

    public string Offense
    {
      get { return Txt_Offense.Text; }
      set { Txt_Offense.Text = value; }
    }

    public string OffenseNote
    {
      get { return Txt_OffenseNote.Text; }
      set { Txt_OffenseNote.Text = value; }
    }

    public string RacialMods
    {
      get { return Txt_RacialMods.Text; }
      set { Txt_RacialMods.Text = value; }
    }

    public string Reach
    {
      get { return Txt_Reach.Text; }
      set { Txt_Reach.Text = value; }
    }

    public string Resist
    {
      get { return Txt_Resist.Text; }
      set { Txt_Resist.Text = value; }
    }

    public string SaveMods
    {
      get { return Txt_SaveMods.Text; }
      set { Txt_SaveMods.Text = value; }
    }

    public string Senses
    {
      get { return Txt_Senses.Text; }
      set { Txt_Senses.Text = value; }
    }

    public string Space
    {
      get { return Txt_Space.Text; }
      set { Txt_Space.Text = value; }
    }

    public string SpecialAbilities
    {
      get { return Txt_SpecialAbilities.Text; }
      set { Txt_SpecialAbilities.Text = value; }
    }

    public string SpecialQualities
    {
      get { return Txt_SpecialQualities.Text; }
      set { Txt_SpecialQualities.Text = value; }
    }

    public string SpeedMods
    {
      get { return Txt_SpeedMods.Text; }
      set { Txt_SpeedMods.Text = value; }
    }

    #endregion

    public BestiaryAbilitiesControl()
    {
      InitializeComponent();
      if (System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
        return;

      this.DataContext = this;
    }
  }
}
