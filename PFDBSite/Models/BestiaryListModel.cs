using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFDBSite.Models
{
    public class BestiaryListModel
    {
        public BestiaryListModel(List<BestiaryListItem> list)
        {
            ListItems = list;
        }

        public List<BestiaryListItem> ListItems { get; set; }
    }

    public class BestiaryListItem
    {
        public int BestiaryId { get; set; }
        public string Name { get; set; }
        public string Cr { get; set; }
        public string Ac { get; set; }
    }
}
