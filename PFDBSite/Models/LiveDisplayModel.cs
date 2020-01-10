using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PFDBCommon.DBModels;

namespace PFDBSite.Models
{
  public class LiveDisplayModel
  {
    public Player ActivePlayer { get; set; }
    public string FantasyDate { get; set; }
    public string MapPath { get; set; }
    public string MapSaveData { get; set; }
    public string CampaignName { get; set; }
    public int CampaignId { get; set; }
    public ChatRoom ChatRoom { get; set; }
  }
}
