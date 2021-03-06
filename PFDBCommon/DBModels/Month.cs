﻿using System;
using System.Collections.Generic;

namespace PFDBCommon.DBModels
{
  public partial class Month
  {
    public int MonthId { get; set; }
    public string Name { get; set; }
    public int? SeasonId { get; set; }
    public int Days { get; set; }
    public int MonthOrder { get; set; }

    public virtual Season Season { get; set; }
  }
}
