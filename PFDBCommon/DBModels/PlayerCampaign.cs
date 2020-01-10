using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class PlayerCampaign
  {
    public int PlayerId { get; set; }
    public int CampaignId { get; set; }
    public bool IsDM { get; set; }

    public virtual Player Player { get; set; }
    public virtual Campaign Campaign { get; set; }
  }
}
