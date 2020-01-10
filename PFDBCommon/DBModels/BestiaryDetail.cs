using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFDBCommon.DBModels
{
  [Table("BestiaryDetail")]
  public partial class BestiaryDetail
  {
    public int BestiaryId { get; set; }
    public string MonsterSource { get; set; }
    public string Source { get; set; }
    public string Description { get; set; }
    public string FullText { get; set; }

    public virtual Bestiary Bestiary { get; set; }
  }
}
