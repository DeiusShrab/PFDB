using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Player
  {
    public int PlayerId { get; set; }
    public string DisplayName { get; set; }

    public virtual ICollection<PlayerCampaign> PlayerCampaigns { get; } = new List<PlayerCampaign>();
  }
}
