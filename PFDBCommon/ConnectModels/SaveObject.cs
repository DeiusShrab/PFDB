using System.Collections.Generic;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class PFHelperSaveObject
  {
    public RandomWeatherResult WeatherResult { get; set; }
    public List<CombatEffectItem> CombatEffects { get; set; }
    public List<CombatGridItem> CombatGridItems { get; set; }
    public FantasyDate Date { get; set; }
    public Weather Weather { get; set; }
    public Continent Continent { get; set; }
    public Plane Plane { get; set; }
    public Time Time { get; set; }
    public Terrain Terrain { get; set; }
    public string ApiPass { get; set; }
    public int Apl { get; set; }
    public int Rations { get; set; }
    public int TimeId { get; set; }
    public int PlaneId { get; set; }
    public int EnvironmentId { get; set; }
    public int CombatRound { get; set; }
    public int CombatTurn { get; set; }
    public bool CbxInfRations { get; set; }
    public bool CbxGroup { get; set; }
    public bool CbxZone { get; set; }
    public bool CbxTime { get; set; }
    public bool CbxNpc { get; set; }
    public bool CbxEvtLocal { get; set; }
    public bool WeatherLock { get; set; }
  }
}
