using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class CombatGridItem : ICloneable, INotifyPropertyChanged
  {
    private static Random rand = new Random();

    public event PropertyChangedEventHandler PropertyChanged;

    public int BestiaryId
    {
      get { return this._bestiaryid; }
      set
      {
        if (value != this._bestiaryid)
        {
          this._bestiaryid = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int Init
    {
      get { return this._init; }
      set
      {
        if (value != this._init)
        {
          this._init = value;
          NotifyPropertyChanged();
        }
      }
    }
    public string Name
    {
      get { return this._name; }
      set
      {
        if (value != this._name)
        {
          this._name = value;
          NotifyPropertyChanged();
        }
      }
    }
    public bool PC
    {
      get { return this._pc; }
      set
      {
        if (value != this._pc)
        {
          this._pc = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int HP
    {
      get { return this._hp; }
      set
      {
        if (value != this._hp)
        {
          this._hp = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int MaxHP
    {
      get { return this._maxhp; }
      set
      {
        if (value != this._maxhp)
        {
          this._maxhp = value;
          NotifyPropertyChanged();
          NotifyPropertyChanged("HPTooltip");
        }
      }
    }
    public int AC
    {
      get { return this._ac; }
      set
      {
        if (value != this._ac)
        {
          this._ac = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int ACTouch
    {
      get { return this._actouch; }
      set
      {
        if (value != this._actouch)
        {
          this._actouch = value;
          NotifyPropertyChanged();
          NotifyPropertyChanged("ACTooltip");
        }
      }
    }
    public int ACFlat
    {
      get { return this._acflat; }
      set
      {
        if (value != this._acflat)
        {
          this._acflat = value;
          NotifyPropertyChanged();
          NotifyPropertyChanged("ACTooltip");
        }
      }
    }
    public int CMB
    {
      get { return this._cmb; }
      set
      {
        if (value != this._cmb)
        {
          this._cmb = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int CMD
    {
      get { return this._cmd; }
      set
      {
        if (value != this._cmd)
        {
          this._cmd = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int Fort
    {
      get { return this._fort; }
      set
      {
        if (value != this._fort)
        {
          this._fort = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int Ref
    {
      get { return this._ref; }
      set
      {
        if (value != this._ref)
        {
          this._ref = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int Will
    {
      get { return this._will; }
      set
      {
        if (value != this._will)
        {
          this._will = value;
          NotifyPropertyChanged();
        }
      }
    }
    public int Subd
    {
      get { return this._subd; }
      set
      {
        if (value != this._subd)
        {
          this._subd = value;
          NotifyPropertyChanged();
          NotifyPropertyChanged("HPTooltip");
        }
      }
    }
    public string Note
    {
      get { return this._note; }
      set
      {
        if (value != this._note)
        {
          this._note = value;
          NotifyPropertyChanged();
        }
      }
    }

    public bool ActiveRow
    {
      get { return this._activeRow; }
      set
      {
        if (value != this._activeRow)
        {
          this._activeRow = value;
          NotifyPropertyChanged();
        }
      }
    }

    public string ACTooltip
    {
      get { return $"Touch {ACTouch}\nFlat {ACFlat}"; }
    }

    public string HPTooltip
    {
      get { return $"Max {MaxHP}\nSubd {Subd}"; }
    }

    private int _bestiaryid;
    private int _init;
    private string _name;
    private bool _pc;
    private int _hp;
    private int _maxhp;
    private int _ac;
    private int _actouch;
    private int _acflat;
    private int _cmb;
    private int _cmd;
    private int _fort;
    private int _ref;
    private int _will;
    private int _subd;
    private string _note;

    private bool _activeRow;

    public CombatGridItem() { }

    public CombatGridItem(Bestiary b)
    {
      BestiaryId = b.BestiaryId;
      Init = rand.Next(1,21) + b.Init;
      Name = b.Name;
      PC = false;
      HP = b.Hp;
      MaxHP = b.Hp;
      AC = b.Ac;
      ACTouch = b.Actouch;
      ACFlat = b.Acflat;
      CMB = b.Cmb;
      CMD = b.Cmd;
      Fort = b.Fortitude;
      Ref = b.Reflex;
      Will = b.Will;
      Subd = 0;
      Note = string.Empty;
    }

    public CombatGridItem(CombatGridItem copy)
    {
      BestiaryId = copy.BestiaryId;
      Init = copy.Init;
      Name = copy.Name;
      PC = copy.PC;
      HP = copy.HP;
      MaxHP = copy.MaxHP;
      AC = copy.AC;
      ACTouch = copy.ACTouch;
      ACFlat = copy.ACFlat;
      CMB = copy.CMB;
      CMD = copy.CMD;
      Fort = copy.Fort;
      Ref = copy.Ref;
      Will = copy.Will;
      Subd = copy.Subd;
      Note = copy.Note;
    }

    public object Clone()
    {
      return new CombatGridItem(this);
    }

    private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
