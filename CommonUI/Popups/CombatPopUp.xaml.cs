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
using System.Windows.Shapes;
using PFDBCommon.ConnectModels;

namespace CommonUI.Popups
{
  /// <summary>
  /// Interaction logic for CombatPopUp.xaml
  /// </summary>
  public partial class CombatPopUp : Window
  {
    #region Interface Variables

    public int BestiaryId
    {
      get
      {
        int.TryParse(TxtBestiaryId.Text, out int ret);
        return ret;
      }
      set { TxtBestiaryId.Text = value.ToString(); }
    }
    public int Init
    {
      get
      {
        int.TryParse(TxtInit.Text, out int ret);
        return ret;
      }
      set { TxtInit.Text = value.ToString(); }
    }
    public string CGName
    {
      get { return TxtName.Text; }
      set { TxtName.Text = value; }
    }
    public bool PC
    {
      get { return CbxPC.IsChecked ?? false; }
      set { CbxPC.IsChecked = value; }
    }
    public int HP
    {
      get
      {
        int.TryParse(TxtHP.Text, out int ret);
        return ret;
      }
      set { TxtHP.Text = value.ToString(); }
    }
    public int MaxHP
    {
      get
      {
        int.TryParse(TxtHPMax.Text, out int ret);
        return ret;
      }
      set { TxtHPMax.Text = value.ToString(); }
    }
    public int AC
    {
      get
      {
        int.TryParse(TxtAC.Text, out int ret);
        return ret;
      }
      set { TxtAC.Text = value.ToString(); }
    }
    public int ACTouch
    {
      get
      {
        int.TryParse(TxtACTouch.Text, out int ret);
        return ret;
      }
      set { TxtACTouch.Text = value.ToString(); }
    }
    public int ACFlat
    {
      get
      {
        int.TryParse(TxtACFlat.Text, out int ret);
        return ret;
      }
      set { TxtACFlat.Text = value.ToString(); }
    }
    public int CMB
    {
      get
      {
        int.TryParse(TxtCMB.Text, out int ret);
        return ret;
      }
      set { TxtCMB.Text = value.ToString(); }
    }
    public int CMD
    {
      get
      {
        int.TryParse(TxtCMD.Text, out int ret);
        return ret;
      }
      set { TxtCMD.Text = value.ToString(); }
    }
    public int Fort
    {
      get
      {
        int.TryParse(TxtFort.Text, out int ret);
        return ret;
      }
      set { TxtFort.Text = value.ToString(); }
    }
    public int Ref
    {
      get
      {
        int.TryParse(TxtRef.Text, out int ret);
        return ret;
      }
      set { TxtRef.Text = value.ToString(); }
    }
    public int Will
    {
      get
      {
        int.TryParse(TxtWill.Text, out int ret);
        return ret;
      }
      set { TxtWill.Text = value.ToString(); }
    }
    public int Subd
    {
      get
      {
        int.TryParse(TxtSubdual.Text, out int ret);
        return ret;
      }
      set { TxtSubdual.Text = value.ToString(); }
    }
    public string Note
    {
      get { return TxtNotes.Text; }
      set { TxtNotes.Text = value; }
    }

    #endregion

    public CombatGridItem CombatGridItem;

    public CombatPopUp()
    {
      InitializeComponent();

      CombatGridItem = new CombatGridItem();
    }

    public CombatPopUp(CombatGridItem item)
    {
      InitializeComponent();

      CombatGridItem = new CombatGridItem(item);
      BestiaryId = CombatGridItem.BestiaryId;
      Init = CombatGridItem.Init;
      CGName = CombatGridItem.Name;
      PC = CombatGridItem.PC;
      HP = CombatGridItem.HP;
      MaxHP = CombatGridItem.MaxHP;
      AC = CombatGridItem.AC;
      ACTouch = CombatGridItem.ACTouch;
      ACFlat = CombatGridItem.ACFlat;
      CMB = CombatGridItem.CMB;
      CMD = CombatGridItem.CMD;
      Fort = CombatGridItem.Fort;
      Ref = CombatGridItem.Ref;
      Will = CombatGridItem.Will;
      Subd = CombatGridItem.Subd;
      Note = CombatGridItem.Note;
    }

    private void BtnOK_Click(object sender, RoutedEventArgs e)
    {
      CombatGridItem.BestiaryId = BestiaryId;
      CombatGridItem.Init = Init;
      CombatGridItem.Name = CGName;
      CombatGridItem.PC = PC;
      CombatGridItem.HP = HP;
      CombatGridItem.MaxHP = MaxHP;
      CombatGridItem.AC = AC;
      CombatGridItem.ACTouch = ACTouch;
      CombatGridItem.ACFlat = ACFlat;
      CombatGridItem.CMB = CMB;
      CombatGridItem.CMD = CMD;
      CombatGridItem.Fort = Fort;
      CombatGridItem.Ref = Ref;
      CombatGridItem.Will = Will;
      CombatGridItem.Subd = Subd;
      CombatGridItem.Note = Note;
      DialogResult = true;
      Close();
    }
  }
}
