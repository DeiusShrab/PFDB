﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PFDAL.Models
{
    public partial class Time
    {
        public int TimeId { get; set; }
        public int TimeOrder { get; set; }
        public string Name { get; set; }
        public bool IsNight { get; set; }
    }
}
