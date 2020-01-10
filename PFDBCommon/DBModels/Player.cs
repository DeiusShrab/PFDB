using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class Player
  {
    public int PlayerId { get; set; }
    public string DisplayName { get; set; }

    public virtual ICollection<PlayerCampaign> PlayerCampaigns { get; } = new List<PlayerCampaign>();
    public virtual ICollection<Character> Characters { get; } = new List<Character>();
  }
}
