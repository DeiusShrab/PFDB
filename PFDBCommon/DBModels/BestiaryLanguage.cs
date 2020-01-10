using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFDBCommon.DBModels
{
  public partial class BestiaryLanguage
  {
    public int BestiaryLanguageId { get; set; }
    public int BestiaryId { get; set; }
    public int LanguageId { get; set; }
    public string Notes { get; set; }

    public virtual Bestiary Bestiary { get; set; }
    public virtual Language Language { get; set; }
  }
}
