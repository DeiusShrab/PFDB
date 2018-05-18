﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Character
  {
    public int CharacterId { get; set; }
    public int PlayerId { get; set; } // 0 for NPCs and DM-created characters
    public int CampaignId { get; set; } // For campaign-specific characters like PCs
    public int RaceId { get; set; }
    public string Name { get; set; }
    public string Deity { get; set; }

    public virtual Campaign Campaign { get; set; }
    public virtual Player Player { get; set; }
    public virtual CharacterRace Race { get; set; }
    public virtual ICollection<CharacterClassLevel> CharacterClassLevels { get; } = new List<CharacterClassLevel>();
    public virtual ICollection<CharacterClassAbility> CharacterClassAbilities { get; } = new List<CharacterClassAbility>();
    public virtual ICollection<CharacterGear> CharacterGear { get; } = new List<CharacterGear>();
    public virtual ICollection<CharacterFeat> CharacterFeats { get; } = new List<CharacterFeat>();
    public virtual ICollection<CharacterSkill> CharacterSkills { get; } = new List<CharacterSkill>();
    public virtual ICollection<CharacterMagic> CharacterMagics { get; } = new List<CharacterMagic>();
  }
}