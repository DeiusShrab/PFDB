using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnect.DBModels
{
  public partial class BestiaryFeat
  {
    public int BestiaryFeatId { get; set; }
    public int BestiaryId { get; set; }
    public int FeatId { get; set; }
    public string Notes { get; set; }
  }
}
