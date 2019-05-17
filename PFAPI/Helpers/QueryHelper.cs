using System.Collections.Generic;
using System.Linq;
using DBConnect;
using DBConnect.ConnectModels;
using DBConnect.DBModels;

namespace PFAPI.Helpers
{
  public class QueryHelper
  {
    public SpellLookupResponse SpellLookup(SpellLookupRequest request)
    {
      var response = new SpellLookupResponse() { SpellDetails = new List<SpellDetail>(), Spells = new List<Spell>() };

      var context = PFDAL.GetContext();
      var levelList = new List<int>();
      if (request.Lv0)
        levelList.Add(0);
      if (request.Lv1)
        levelList.Add(1);
      if (request.Lv2)
        levelList.Add(2);
      if (request.Lv3)
        levelList.Add(3);
      if (request.Lv4)
        levelList.Add(4);
      if (request.Lv5)
        levelList.Add(5);
      if (request.Lv6)
        levelList.Add(6);
      if (request.Lv7)
        levelList.Add(7);
      if (request.Lv8)
        levelList.Add(8);
      if (request.Lv9)
        levelList.Add(9);

      var spellList = from s in context.Spell
                      where (
                        (request.Sor && levelList.Contains(s.Sor ?? -1))
                        || (request.Wiz && levelList.Contains(s.Wiz ?? -1))
                        || (request.Cleric && levelList.Contains(s.Cleric ?? -1))
                        || (request.Druid && levelList.Contains(s.Druid ?? -1))
                        || (request.Ranger && levelList.Contains(s.Ranger ?? -1))
                        || (request.Bard && levelList.Contains(s.Bard ?? -1))
                        || (request.Paladin && levelList.Contains(s.Paladin ?? -1))
                        || (request.Alchemist && levelList.Contains(s.Alchemist ?? -1))
                        || (request.Summoner && levelList.Contains(s.Summoner ?? -1))
                        || (request.Witch && levelList.Contains(s.Witch ?? -1))
                        || (request.Inquisitor && levelList.Contains(s.Inquisitor ?? -1))
                        || (request.Oracle && levelList.Contains(s.Oracle ?? -1))
                        || (request.Antipaladin && levelList.Contains(s.Antipaladin ?? -1))
                        || (request.Magus && levelList.Contains(s.Magus ?? -1))
                        || (request.Adept && levelList.Contains(s.Adept ?? -1))
                        || (request.BloodRager && levelList.Contains(s.BloodRager ?? -1))
                        || (request.Shaman && levelList.Contains(s.Shaman ?? -1))
                        || (request.Psychic && levelList.Contains(s.Psychic ?? -1))
                        || (request.Medium && levelList.Contains(s.Medium ?? -1))
                        || (request.Mesmerist && levelList.Contains(s.Mesmerist ?? -1))
                        || (request.Occultist && levelList.Contains(s.Occultist ?? -1))
                        || (request.Spiritualist && levelList.Contains(s.Spiritualist ?? -1))
                        || (request.Skald && levelList.Contains(s.Skald ?? -1))
                        || (request.Investigator && levelList.Contains(s.Investigator ?? -1))
                        || (request.Hunter && levelList.Contains(s.Hunter ?? -1))
                        )
                      select s;

      if (spellList != null)
      {
        var spellDetails = from sd in context.SpellDetail
                           where spellList.Select(x => x.SpellId).Contains(sd.SpellId)
                           select sd;

        response.SpellDetails = spellDetails.ToList();
        response.Spells = spellList.ToList();
      }

      return response;
    }

    public void UpdateSpawns(SpawnUpdateRequest request)
    {
      if (request.BestiaryId > 0)
      {
        var context = PFDAL.GetContext();
        var removeSpawns = context.MonsterSpawn.Where(x => x.BestiaryId == request.BestiaryId).ToList();
        foreach (var item in removeSpawns)
        {
          context.MonsterSpawn.Remove(item);
          context.SaveChanges();
        }
      }

      if (request.SpawnList != null)
      {
        var context = PFDAL.GetContext();

        foreach (var item in request.SpawnList)
        {
          context.MonsterSpawn.Add(item);
          context.SaveChanges();
        }
      }
    }

    public void UpdateContinentWeathers(ContinentWeatherUpdateRequest request)
    {
      if (request.ContinentId > 0 && request.SeasonId > 0)
      {
        var context = PFDAL.GetContext();
        var removeCWs = context.ContinentWeather.Where(x => x.ContinentId == request.ContinentId && x.SeasonId == request.SeasonId);
        foreach (var item in removeCWs)
        {
          context.ContinentWeather.Remove(item);
          context.SaveChanges();
        }
      }

      if (request.UpdateList != null)
      {
        var context = PFDAL.GetContext();
        foreach (var item in request.UpdateList)
        {
          context.ContinentWeather.Add(item);
          context.SaveChanges();
        }
      }
    }

    public List<Environment> EnvironmentsForContinent(int continentId)
    {
      var ret = new List<Environment>();
      var context = PFDAL.GetContext();

      if (continentId == 0)
      {
        ret.AddRange(context.Environment);
      }
      else
      {
        var envList = from ce in context.ContinentEnvironment
                      join e in context.Environment on ce.EnvironmentId equals e.EnvironmentId
                      where ce.ContinentId == continentId
                      select e;

        ret.AddRange(envList);
      }

      return ret;
    }

    public List<MonsterSpawnEdit> GetMonsterSpawnEdit()
    {
      var ret = new List<MonsterSpawnEdit>();
      var context = PFDAL.GetContext();

      var retList = from ms in context.MonsterSpawn
                    join b in context.Bestiary on ms.BestiaryId equals b.BestiaryId
                    join t in context.BestiaryType on b.Type equals t.BestiaryTypeId
                    select new MonsterSpawnEdit
                    {
                      BestiaryId = ms.BestiaryId,
                      ContinentId = ms.ContinentId,
                      CR = b.Cr,
                      Name = b.Name,
                      PlaneId = ms.PlaneId,
                      SeasonId = ms.SeasonId,
                      TimeId = ms.TimeId,
                      Type = t.Name
                    };

      foreach (var item in retList)
      {
        var types = from st in context.BestiarySubType
                    join t in context.BestiaryType on st.BestiaryTypeId equals t.BestiaryTypeId
                    where st.BestiaryId == item.BestiaryId
                    select t.Name;

        if (types.Count() > 0)
        {
          item.Type += " (";
          foreach (var type in types.OrderBy(x => x))
          {
            item.Type += type + ", ";
          }
          item.Type.Remove(item.Type.Length - 2);
          item.Type += ")";
        }
      }

      return ret;
    }

    public void UpdateMonsterSpawn(UpdateMonsterSpawnRequest request)
    {
      var context = PFDAL.GetContext();

      var delList = from ms in context.MonsterSpawn
                    where ms.BestiaryId == request.BestiaryId
                      && ms.ContinentId == request.ContinentId
                      && ms.SeasonId == request.SeasonId
                    select ms;

      foreach (var item in delList)
      {
        context.MonsterSpawn.Remove(item);
      }

      foreach (var item in request.Spawns)
      {
        context.MonsterSpawn.Add(item);
      }

      context.SaveChanges();
    }
  }
}
