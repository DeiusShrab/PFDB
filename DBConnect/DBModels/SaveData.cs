using System;
using System.Collections.Generic;
using System.Text;

namespace DBConnect.DBModels
{
  public class SaveData
  {
    public SaveDataType SaveDataType { get; set; }
    public int CampaignId { get; set; }
    public string Data { get; set; }

    public virtual Campaign Campaign { get; set; }
  }
}
