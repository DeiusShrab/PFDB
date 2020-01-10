using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PFDBCommon.DBModels
{
  public partial class Campaign
  {
    public int CampaignId { get; set; }
    public string CampaignNotes { get; set; }
    public string CampaignName { get; set; }
    public bool IsActive { get; set; }
    public string LiveDisplayMapPath { get; set; }

    public virtual ICollection<CampaignData> CampaignData { get; } = new List<CampaignData>();
    public virtual ICollection<PlayerCampaign> PlayerCampaigns { get; } = new List<PlayerCampaign>();
    public virtual ICollection<TrackedEvent> TrackedEvents { get; } = new List<TrackedEvent>();
    public virtual ICollection<Character> Characters { get; } = new List<Character>();
  }
}
