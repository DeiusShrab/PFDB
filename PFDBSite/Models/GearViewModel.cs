﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect.DBModels;
using DBConnect;

namespace PFDBSite.Models
{
  public class GearViewModel
  {
    public Gear Gear { get; set; }
    public GearType GearType { get; set; }
  }
}
