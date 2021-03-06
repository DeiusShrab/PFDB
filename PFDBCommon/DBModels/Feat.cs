﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PFDBCommon.DBModels
{
  public partial class Feat
  {
    public int FeatId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string Prerequisites { get; set; }
    public string PrerequisiteFeats { get; set; } // TODO: convert to relationship
    public string Benefit { get; set; }
    public string Normal { get; set; }
    public string Special { get; set; }
    public string Source { get; set; }
    public string Fulltext { get; set; }
    public bool Teamwork { get; set; }
    public bool Critical { get; set; }
    public bool Grit { get; set; }
    public bool Style { get; set; }
    public bool Performance { get; set; }
    public bool Racial { get; set; }
    public bool CompanionFamiliar { get; set; }
    public string RaceName { get; set; } // TODO: convert to relationship
    public string Note { get; set; }
    public string Goal { get; set; }
    public string CompletionBenefit { get; set; }
    public bool Multiples { get; set; }
    public string SuggestedTraits { get; set; } // TODO: convert to relationship
    public string PrerequisiteSkills { get; set; } // TODO: convert to relationship
    public bool Panache { get; set; }
    public bool Betrayal { get; set; }
    public bool Targeting { get; set; }
    public bool Esoteric { get; set; }
    public bool Stare { get; set; }
    public bool WeaponMastery { get; set; }
    public bool ItemMastery { get; set; }
    public bool ArmorMastery { get; set; }
    public bool ShieldMastery { get; set; }
    public bool BloodHex { get; set; }

    public virtual ICollection<BestiaryFeat> BestiaryFeats { get; } = new List<BestiaryFeat>();
    public virtual ICollection<CharacterFeat> CharacterFeats { get; } = new List<CharacterFeat>();
  }
}
