﻿using System;
using System.Collections.Generic;
using System.Text;
using PFDAL.Models;

namespace PFDAL.ConnectModels
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
    public List<Weather> WeatherList { get; set; }
  }
}
