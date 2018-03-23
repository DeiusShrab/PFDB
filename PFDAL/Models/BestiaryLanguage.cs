using System;
using System.Collections.Generic;

namespace PFDAL.Models
{
  public partial class BestiaryLanguage
  {
    public int BestiaryLanguageId { get; set; }
    public int BestiaryId { get; set; }
    public int LanguageId { get; set; }
    public string Notes { get; set; }
  }
}
