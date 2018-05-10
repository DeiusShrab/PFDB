using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class CharacterClass
  {
    public int ClassId { get; set; }
    public int ArchetypeForClassId { get; set; }
    public string Name { get; set; }
    public int HD { get; set; }
    public int SkillPts { get; set; }
    public int StartingGold { get; set; }
    public string Alignment { get; set; }
    public int BAB { get; set; }
    public int Fort { get; set; }
    public int Ref { get; set; }
    public int Will { get; set; }
    public string SpellsPerDay { get; set; }
    public string SpellsKnown { get; set; }
    public string CastingStat { get; set; }

    public virtual List<Skill> ClassSkills { get; set; }
  }
}
