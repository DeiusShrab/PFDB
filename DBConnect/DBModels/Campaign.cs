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

    public virtual ICollection<CampaignData> CampaignData { get; } = new List<CampaignData>();
    public virtual ICollection<SaveData> SaveData { get; } = new List<SaveData>();
    public virtual ICollection<PlayerCampaign> PlayerCampaigns { get; } = new List<PlayerCampaign>();
    public virtual ICollection<TrackedEvent> TrackedEvents { get; } = new List<TrackedEvent>();
    public virtual ICollection<Character> Characters { get; } = new List<Character>();
  }
}
