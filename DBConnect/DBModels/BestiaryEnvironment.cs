using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBConnect.DBModels
{
  public partial class BestiaryEnvironment
  {
    public int BestiaryEnvironmentId { get; set; }
    public int BestiaryId { get; set; }
    public int EnvironmentId { get; set; }
    public string Notes { get; set; }
  }
}
