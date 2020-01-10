using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class CharacterLanguage
  {
    public int CharacterId { get; set; }
    public int LanguageId { get; set; }

    public virtual Character Character { get; set; }
    public virtual Language Language { get; set; }
  }
}
