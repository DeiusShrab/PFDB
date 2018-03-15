using System;
using System.Collections.Generic;

namespace DBConnect.DBModels
{
    public partial class Month
    {
        public int MonthId { get; set; }
        public string Name { get; set; }
        public int? Season { get; set; }
        public int? Days { get; set; }
    }
}
