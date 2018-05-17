﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBConnect.DBModels
{
  public partial class BestiaryType
  {
    public int BestiaryTypeId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Bestiary> Bestiaries { get; } = new List<Bestiary>();
    public virtual ICollection<CharacterRace> CharacterRaces { get; } = new List<CharacterRace>();
    public virtual ICollection<BestiarySubType> BestiarySubTypes { get; } = new List<BestiarySubType>();
  }
}
