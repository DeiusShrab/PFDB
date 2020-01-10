using System.Collections.Generic;
using PFDBCommon.DBModels;

namespace PFDBCommon.ConnectModels
{
  public class RandomWeatherRequest
  {
    public int ContinentId { get; set; }
    public int SeasonId { get; set; }
  }

  public class RandomWeatherResult
  {
    public bool Success { get; set; }
    public int ContinentId { get; set; }
    public int SeasonId { get; set; }
    public List<ContinentWeather> WeatherList { get; set; }
  }
}
