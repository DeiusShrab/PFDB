﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class SpellSubSchool
  {
    public int SpellSubSchoolId { get; set; }
    public int SpellSchoolId { get; set; }
    public string Name { get; set; }
  }
}
