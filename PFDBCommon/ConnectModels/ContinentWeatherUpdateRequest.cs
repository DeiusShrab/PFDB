using System.Collections.Generic;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class ContinentWeatherUpdateRequest
  {
    public int ContinentId { get; set; }
    public int SeasonId { get; set; }
    public List<ContinentWeather> UpdateList { get; set; }
  }
}
