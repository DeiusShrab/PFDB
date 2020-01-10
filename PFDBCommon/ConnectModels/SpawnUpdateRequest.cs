using System.Collections.Generic;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class SpawnUpdateRequest
  {
    public int BestiaryId { get; set; }
    public List<MonsterSpawn> SpawnList { get; set; }
  }
}
