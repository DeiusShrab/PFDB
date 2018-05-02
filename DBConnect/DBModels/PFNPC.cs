using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public partial class PFNPC
  {
    public int NPCID { get; set; }
    public string Name { get; set; }
    public string Alignment { get; set; }
    public string Size { get; set; }
    public string Race { get; set; }
    public string Class { get; set; }
    public int CR { get; set; }
    public string Height { get; set; }
    public string Weight { get; set; }
    public string Hair { get; set; }
    public string Eyes { get; set; }
    public string Notes { get; set; }
    public int Init { get; set; }
    public string Senses { get; set; }
    public int AC { get; set; }
    public int ACTouch { get; set; }
    public int ACFlat { get; set; }
    public int HP { get; set; }
    public int Speed { get; set; }
    public string Space { get; set; }
    public string Reach { get; set; }
    public int Climb { get; set; }
    public int Fly { get; set; }
    public int Swim { get; set; }
    public int Burrow { get; set; }
    public string DR { get; set; }
    public int Fort { get; set; }
    public int Ref { get; set; }
    public int Will { get; set; }
    public int CL { get; set; }
    public int SpellTouch { get; set; }
    public int SpellRangeTouch { get; set; }
    public int SpellDC { get; set; }
    public string SpellsPerDay { get; set; }
    public int STR { get; set; }
    public int CON { get; set; }
    public int DEX { get; set; }
    public int INT { get; set; }
    public int WIS { get; set; }
    public int CHA { get; set; }
    public int BAB { get; set; }
    public int CMB { get; set; }
    public int CMD { get; set; }
    public List<Feat> Feats { get; set; }
    public List<Skill> Skills { get; set; }
    public List<Spell> Spells { get; set; }
    public List<string> SpecialDef { get; set; }
    public List<string> SpecialOff { get; set; }
    public List<Attack> Attacks { get; set; }
    public List<string> Items { get; set; }

    public PFNPC()
    {
      Feats = new List<Feat>();
      Skills = new List<Skill>();
      Spells = new List<Spell>();
      SpecialDef = new List<string>();
      SpecialOff = new List<string>();
      Attacks = new List<Attack>();
      Items = new List<string>();
    }
  }
}
