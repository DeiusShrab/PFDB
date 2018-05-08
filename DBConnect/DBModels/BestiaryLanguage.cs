using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnect.DBModels
{
  public partial class BestiaryLanguage
  {
    public int BestiaryLanguageId { get; set; }
    public int BestiaryId { get; set; }
    public int LanguageId { get; set; }
    public string Notes { get; set; }
  }
}
