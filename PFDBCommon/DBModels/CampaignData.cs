using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class CampaignData
  {
    public int CampaignId { get; set; }
    public string Key { get; set; }
    public string Value { get; set; }

    public virtual Campaign Campaign { get; set; }
  }
}
