﻿namespace DBConnect.DBModels
{
  public partial class ContinentWeather
  {
    public int CWID { get; set; }
    public string Name { get; set; }
    public int ContinentId { get; set; }
    public int WeatherId { get; set; }
    public int SeasonId { get; set; }
    public int Weight { get; set; }
    public int Duration { get; set; }
    public bool RandomDuration { get; set; }
    public int NextCWID { get; set; }
    public int ParentCWID { get; set; }
  }
}
