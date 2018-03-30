using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFDBSite.Models
{
  public class BestiaryListModel
  {
    public BestiaryListModel(IEnumerable<BestiaryListItem> list)
    {
      ListItems = list;
    }

    public IEnumerable<BestiaryListItem> ListItems { get; set; }
  }

  public class BestiaryListItem
  {
    public int BestiaryId { get; set; }
    public int Cr { get; set; }
    public string Name { get; set; }
    public string CrDisplay { get; set; }
    public string Type { get; set; }
    public string SubType { get; set; }
  }
}
