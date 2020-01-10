using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class PlayerPermission
  {
    public int PlayerId { get; set; }
    public int PermissionId { get; set; }
    public string Data { get; set; }
  }
}
