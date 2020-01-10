using PFDBCommon.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFDBCommon.ConnectModels
{
  public class MonsterSpawnEdit : MonsterSpawn
  {
    public string Name { get; set; }
    public string Type { get; set; }
    public int CR { get; set; }
    public bool IsDirty { get; set; }
  }
}
