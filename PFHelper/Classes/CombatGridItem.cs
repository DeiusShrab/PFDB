using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFHelper.Classes
{
    public class CombatGridItem
    {
        public int Init { get; set; }
        public string Name { get; set; }
        public bool PC { get; set; }
        public int HP { get; set; }
        public int AC { get; set; }
        public int ACTouch { get; set; }
        public int ACFlat { get; set; }
        public int Fort { get; set; }
        public int Ref { get; set; }
        public int Will { get; set; }
        public int Subd { get; set; }
        public string Note { get; set; }
        public string ACTooltip
        {
            get { return $"Touch {ACTouch}\nFlat {ACFlat}"; }
        }
    }
}
