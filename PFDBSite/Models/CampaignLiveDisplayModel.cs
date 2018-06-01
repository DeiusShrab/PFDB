using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBConnect.DBModels;

namespace PFDBSite.Models
{
  public class CampaignLiveDisplayModel
  {
    public string FantasyDate { get; set; }
    public string MapPath { get; set; }
    public string MapSaveData { get; set; }
    public List<Character> Characters { get; set; }
  }
}
