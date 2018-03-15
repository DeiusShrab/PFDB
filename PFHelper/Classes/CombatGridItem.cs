using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnect.DBModels;

namespace PFHelper.Classes
{
    public class CombatGridItem
    {
        public int BestiaryId { get; set; }
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

        public CombatGridItem() { }

        public CombatGridItem(Bestiary b)
        {
            BestiaryId = b.BestiaryId;
            Init = b.Init ?? 0;
            Name = b.Name;
            PC = false;
            HP = b.Hp ?? 0;
            AC = 0; // TODO: Replace this after DB update
            ACTouch = b.Actouch ?? 0;
            ACFlat = b.Acflat ?? 0;
            Fort = b.Fortitude ?? 0;
            Ref = b.Reflex ?? 0;
            Will = b.Will ?? 0;
            Subd = 0;
            Note = string.Empty;
        }
    }
}
