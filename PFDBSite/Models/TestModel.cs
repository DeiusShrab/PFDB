using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFDBSite.Models
{
    public class TestModel
    {
        public int numValue { get; set; }
        public string strValue { get; set; }

        public override string ToString()
        {
            return $"{strValue}, {numValue}";
        }
    }
}
