using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class Character
  {
    public int CharacterId { get; set; }
    public int PlayerId { get; set; } // 0 for NPCs and DM-created characters
    public int CampaignId { get; set; } // For campaign-specific characters like PCs
    public string Name { get; set; }
    public string Deity { get; set; }

  }
}
