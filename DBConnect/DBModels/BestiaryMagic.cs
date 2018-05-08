using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DBConnect.DBModels
{
  public partial class BestiaryMagic
  {
    public int BestiaryMagicId { get; set; }
    public int BestiaryId { get; set; }
    public int SpellId { get; set; }
    public string Notes { get; set; }
    public int CasterLevel { get; set; }
    public int UsesPerDay { get; set; }
    public MagicType MagicTypeId { get; set; }
  }

  public enum MagicType
  {
    Su = 1,
    Sq = 2,
    Ex = 3,
    Prep = 4,
    Spont = 5,
  }
}
