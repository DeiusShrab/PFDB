using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class PlayerCampaign
  {
    public int PlayerId { get; set; }
    public int CampaignId { get; set; }
    public bool IsDM { get; set; }
  }
}
