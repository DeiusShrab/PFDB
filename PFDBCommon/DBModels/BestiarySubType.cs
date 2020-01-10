using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PFDBCommon.DBModels
{
  public partial class BestiarySubType
  {
    public int BestiarySubTypeId { get; set; }
    public int BestiaryId { get; set; }
    public int BestiaryTypeId { get; set; }

    public virtual Bestiary Bestiary { get; set; }
    public virtual BestiaryType BestiaryType { get; set; }
  }
}
