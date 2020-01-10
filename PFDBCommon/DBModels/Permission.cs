using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.DBModels
{
  public class Permission
  {
    public int PermissionId { get; set; }
    public string PermissionName { get; set; }
    public int PermissionDataType { get; set; }
    public string Notes { get; set; }
  }
}
