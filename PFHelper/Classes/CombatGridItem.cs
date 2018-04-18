using System;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
  public class CombatGridItem : ICloneable
  {
    public int BestiaryId { get; set; }
    public int Init { get; set; }
    public string Name { get; set; }
    public bool PC { get; set; }
    public int HP { get; set; }
    public int AC { get; set; }
    public int ACTouch { get; set; }
    public int ACFlat { get; set; }
    public int Fort { get; set; }
    public int Ref { get; set; }
    public int Will { get; set; }
    public int Subd { get; set; }
    public string Note { get; set; }
    public string ACTooltip
    {
      get { return $"Touch {ACTouch}\nFlat {ACFlat}"; }
    }

    public CombatGridItem() { }

    public CombatGridItem(Bestiary b)
    {
      BestiaryId = b.BestiaryId;
      Init = b.Init;
      Name = b.Name;
      PC = false;
      HP = b.Hp;
      AC = b.Ac;
      ACTouch = b.Actouch;
      ACFlat = b.Acflat;
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
      AC = copy.AC;
      ACTouch = copy.ACTouch;
      ACFlat = copy.ACFlat;
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
  }
}
