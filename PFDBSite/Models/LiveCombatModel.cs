using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFDBSite.Models
{
  public class LiveCombatModel
  {
    public Player ActivePlayer { get; set; }
    public List<CombatGridItem> CombatGridItems { get; set; }

  }
}
