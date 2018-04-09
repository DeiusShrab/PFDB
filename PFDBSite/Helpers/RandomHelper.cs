using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFDAL;
using PFDAL.ConnectModels;
using PFDAL.Models;

namespace PFDBSite.Helpers
{
  public class RandomHelper
  {
    public IPFDBContext context;
    private Random random = new Random();

    public RandomHelper(IPFDBContext _context)
    {
      context = _context;
    }

    public RandomEncounterResult GenerateRandomEncounters(RandomEncounterRequest request)
    {
      var encounterList = new List<RandomEncounterItem>();
      var groupMon = new List<Bestiary>();
      var validSpawns = from s in context.MonsterSpawn
                        join b in context.Bestiary on s.BestiaryId equals b.BestiaryId
                        where (s.ContinentId == request.ContinentId || request.ContinentId == 0)
                            && (s.SeasonId == request.SeasonId || request.SeasonId == 0)
                            && (s.TimeId == request.TimeId || request.TimeId == 0)
                            && (s.TerrainId == request.TerrainId || request.TerrainId == 0)
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
          encounterList.Add(new RandomEncounterItem() { BestiaryId = mon.BestiaryId, Cr = mon.Cr ?? 0, Name = mon.Name });
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
      var ret = new RandomWeatherResult()
      {
        ContinentId = request.ContinentId,
        SeasonId = request.SeasonId
      };

      var validWeathers = from c in context.ContinentWeather
                          join w in context.Weather on c.WeatherId equals w.WeatherId
                          where (c.ContinentId == request.ContinentId || c.ContinentId == 0)
                              && (c.SeasonId == request.SeasonId || c.SeasonId == 0)
                          select w;

      ret.WeatherList = validWeathers.ToList();
      ret.Success = true;

      return ret;
    }
  }
}
