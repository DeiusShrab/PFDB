﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class Class
  {
    public int ClassId { get; set; }
    public int ArchetypeForClassId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int HD { get; set; }
    public int SkillPts { get; set; }
    public string StartingGold { get; set; }
    public string Alignment { get; set; }
    // Redo all these as ClassAbilities since they scale with level
    //public int BAB { get; set; }
    //public int Fort { get; set; }
    //public int Ref { get; set; }
    //public int Will { get; set; }
    //public string SpellsPerDay { get; set; }
    //public string SpellsKnown { get; set; }
    public Stat CastingStat { get; set; }
    public ClassType ClassType { get; set; }

    public virtual ICollection<ClassSkill> ClassSkills { get; } = new List<ClassSkill>();
    public virtual ICollection<ClassAbility> ClassAbilities { get; } = new List<ClassAbility>();
    public virtual ICollection<CharacterClassLevel> CharacterClassLevels { get; } = new List<CharacterClassLevel>();
    public virtual ICollection<FavoredClass> FavoredClasses { get; } = new List<FavoredClass>();
  }
}
