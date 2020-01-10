using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PFDBCommon.ConnectModels;
using PFDBCommon.DBModels;

namespace PFDBSite.Models
{
  public class LiveCombatModel
  {
    public Player ActivePlayer { get; set; }
    public List<CombatGridItem> CombatGridItems { get; set; }

  }
}
