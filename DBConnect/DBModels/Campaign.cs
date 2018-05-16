using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBConnect.DBModels
{
  public partial class Campaign
  {
    public int CampaignId { get; set; }
    public string CampaignNotes { get; set; }
    public string CampaignName { get; set; }
    public bool IsActive { get; set; }
  }
}
