using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class RaceSubType
  {
    public int RaceId { get; set; }
    public int BestiaryTypeId { get; set; }

    public virtual Race Race { get; set; }
    public virtual BestiaryType BestiaryType { get; set; }
  }
}
