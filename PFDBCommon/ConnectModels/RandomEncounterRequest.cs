using System.Collections.Generic;

namespace PFDBCommon.ConnectModels
{
  public class RandomEncounterRequest
  {
    public int[] Crs { get; set; }
    public bool Group { get; set; }
    public bool Npc { get; set; }
    public int ContinentId { get; set; }
    public int EnvironmentId { get; set; }
    public int TimeId { get; set; }
    public int SeasonId { get; set; }
    public int PlaneId { get; set; }
  }
  public class RandomEncounterResult
  {
    public List<RandomEncounterItem> EncounterItems { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
  }

  public class RandomEncounterItem
  {
    public string Name { get; set; }
    public int Cr { get; set; }
    public int BestiaryId { get; set; }
  }
}
