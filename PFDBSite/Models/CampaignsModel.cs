using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFDBSite.Models
{
  public class CampaignsModel
  {
    public Dictionary<int, string> CampaignList { get; set; } = new Dictionary<int, string>();
  }
}
