using System;
using System.Collections.Generic;
using System.Linq;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFAPI.Helpers
{
  public class RandomHelper
  {
    private Random random = new Random();

    public RandomEncounterResult GenerateRandomEncounters(RandomEncounterRequest request)
    {
      var context = PFDAL.GetContext();
      var encounterList = new List<RandomEncounterItem>();
      var groupMon = new List<Bestiary>();
      var validSpawns = from s in context.MonsterSpawn
                        join b in context.Bestiary on s.BestiaryId equals b.BestiaryId
                        where (s.ContinentId == request.ContinentId || request.ContinentId == 0)
                            && (s.SeasonId == request.SeasonId || request.SeasonId == 0)
                            && (s.TimeId == request.TimeId || request.TimeId == 0)
                            && b.CharacterFlag == request.Npc
                        select b;

      foreach (var cr in request.Crs)
      {
        Bestiary mon = new Bestiary();
        if (request.Group && groupMon.Select(x => x.Cr).Contains(cr))
        {
          mon = groupMon.First(x => x.Cr == cr);
        }
        else
        {
          var monList = validSpawns.Where(x => x.Cr == cr).ToList();
          mon = monList.ElementAt(random.Next(monList.Count));
        }

        if (mon != null)
        {
          groupMon.Add(mon);
          encounterList.Add(new RandomEncounterItem() { BestiaryId = mon.BestiaryId, Cr = mon.Cr, Name = mon.Name });
        }
      }

      return new RandomEncounterResult()
      {
        EncounterItems = encounterList,
        Message = $"Generated {encounterList.Count} items",
        Success = encounterList.Any()
      };
    }

    public RandomWeatherResult GenerateRandomWeatherTable(RandomWeatherRequest request)
    {
      var context = PFDAL.GetContext();
      var ret = new RandomWeatherResult()
      {
        ContinentId = request.ContinentId,
        SeasonId = request.SeasonId
      };

      var validWeathers = from c in context.ContinentWeather
                          where (c.ContinentId == request.ContinentId || c.ContinentId == 0)
                              && (c.SeasonId == request.SeasonId || c.SeasonId == 0)
                          select c;

      ret.WeatherList = validWeathers.ToList();
      ret.Success = true;

      return ret;
    }
  }
}
