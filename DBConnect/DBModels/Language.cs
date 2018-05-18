using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
  public partial class Language
  {
    public int LanguageId { get; set; }
    public string Name { get; set; }
    public string Notes { get; set; }

    public virtual ICollection<BestiaryLanguage> BestiaryLanguages { get; } = new List<BestiaryLanguage>();
    public virtual ICollection<CharacterLanguage> CharacterLanguages { get; } = new List<CharacterLanguage>();
    public virtual ICollection<Continent> ContinentPrimaryLanguages { get; } = new List<Continent>();
    public virtual ICollection<Faction> FactionPrimaryLanguages { get; } = new List<Faction>();
  }
}
