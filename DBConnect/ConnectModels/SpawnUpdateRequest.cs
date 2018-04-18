using System.Collections.Generic;
using DBConnect.DBModels;

namespace DBConnect.ConnectModels
{
  public class SpawnUpdateRequest
  {
    public int BestiaryId { get; set; }
    public List<MonsterSpawn> SpawnList { get; set; }
  }
}
