using System.Collections.Generic;
using System.Net.Http;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using Newtonsoft.Json;

namespace DBConnect
{
  public static class DBClient
  {
    private const string API_ADDR = @"http://zratsewk.duckdns.org/pfdb/api/";
    private static int MAX_CACHE_SIZE = 32;
    private static readonly HttpClient client = new HttpClient();
    private static DBCache<BestiaryDetail> DetailCache = new DBCache<BestiaryDetail>();

    #region Queries

    public static RandomEncounterResult GetEncounters(RandomEncounterRequest request)
    {
      var ret = new RandomEncounterResult
      {
        Success = false
      };

      var response = client.PostAsync(API_ADDR + "RandomEncounter", new StringContent(JsonConvert.SerializeObject(request))).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<RandomEncounterResult>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    #endregion

    #region Details

    public static Bestiary GetBestiary(int bestiaryId)
    {
      Bestiary ret = null;

      var response = client.GetAsync(API_ADDR + "Bestiary/" + bestiaryId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Bestiary>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    /// <summary>
    /// Gets a BestiaryDetail object from the server
    /// </summary>
    /// <param name="bestiaryId">the id of the object</param>
    /// <param name="forceRetrieve">For PFEditor use, if true, the cache will be skipped</param>
    /// <returns></returns>
    public static BestiaryDetail GetBestiaryDetail(int bestiaryId, bool forceRetrieve = false)
    {
      BestiaryDetail ret = null;

      if (!forceRetrieve && DetailCache.ContainsKey(bestiaryId))
        ret = DetailCache[bestiaryId];
      else
      {
        var response = client.GetAsync(API_ADDR + "BestiaryDetail/" + bestiaryId.ToString()).Result;
        if (response.IsSuccessStatusCode)
        {
          var content = response.Content;
          ret = JsonConvert.DeserializeObject<BestiaryDetail>(content.ReadAsStringAsync().Result);
        }

        if (!forceRetrieve && ret != null)
          DetailCache.Add(bestiaryId, ret);
      }

      return ret;
    }

    public static Feat GetFeat(int featId)
    {
      Feat ret = null;

      var response = client.GetAsync(API_ADDR + "Feat/" + featId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Feat>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Skill GetSkill(int skillId)
    {
      Skill ret = null;

      var response = client.GetAsync(API_ADDR + "Skill/" + skillId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Skill>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Spell GetSpell(int spellId)
    {
      Spell ret = null;

      var response = client.GetAsync(API_ADDR + "Spell/" + spellId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Spell>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static SpellDetail GetSpellDetail(int spellId)
    {
      SpellDetail ret = null;

      var response = client.GetAsync(API_ADDR + "SpellDetail/" + spellId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<SpellDetail>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    #endregion

    #region Lists

    public static List<ListItemResult> GetBestiaryList()
    {
      var ret = new List<ListItemResult>();

      var response = client.GetAsync(API_ADDR + "Bestiary").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ListItemResult>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiaryEnvironment> GetBestiaryEnvironments()
    {
      List<BestiaryEnvironment> ret = new List<BestiaryEnvironment>();

      var response = client.GetAsync(API_ADDR + "BestiaryEnvironment").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiaryEnvironment>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiaryFeat> GetBestiaryFeats()
    {
      List<BestiaryFeat> ret = new List<BestiaryFeat>();

      var response = client.GetAsync(API_ADDR + "BestiaryFeat").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiaryFeat>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiaryLanguage> GetBestiaryLanguages()
    {
      List<BestiaryLanguage> ret = new List<BestiaryLanguage>();

      var response = client.GetAsync(API_ADDR + "BestiaryLanguage").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiaryLanguage>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiarySkill> GetBestiarySkills()
    {
      List<BestiarySkill> ret = new List<BestiarySkill>();

      var response = client.GetAsync(API_ADDR + "BestiarySkill").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiarySkill>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiarySubType> GetBestiarySubTypes()
    {
      List<BestiarySubType> ret = new List<BestiarySubType>();

      var response = client.GetAsync(API_ADDR + "BestiarySubType").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiarySubType>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<BestiaryType> GetBestiaryTypes()
    {
      List<BestiaryType> ret = new List<BestiaryType>();

      var response = client.GetAsync(API_ADDR + "BestiaryType").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<BestiaryType>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Continent> GetContinents()
    {
      List<Continent> ret = new List<Continent>();

      var response = client.GetAsync(API_ADDR + "Continent").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Continent>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<ContinentWeather> GetContinentWeathers()
    {
      List<ContinentWeather> ret = new List<ContinentWeather>();

      var response = client.GetAsync(API_ADDR + "ContinentWeather").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ContinentWeather>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Environment> GetEnvironments()
    {
      List<Environment> ret = new List<Environment>();

      var response = client.GetAsync(API_ADDR + "Environment").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Environment>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Faction> GetFactions()
    {
      List<Faction> ret = new List<Faction>();

      var response = client.GetAsync(API_ADDR + "Faction").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Faction>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<ListItemResult> GetFeatList()
    {
      var ret = new List<ListItemResult>();

      var response = client.GetAsync(API_ADDR + "Feat").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ListItemResult>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Language> GetLanguages()
    {
      List<Language> ret = new List<Language>();

      var response = client.GetAsync(API_ADDR + "Language").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Language>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Location> GetLocations()
    {
      List<Location> ret = new List<Location>();

      var response = client.GetAsync(API_ADDR + "Location").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Location>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<MagicItem> GetMagicItems()
    {
      List<MagicItem> ret = new List<MagicItem>();

      var response = client.GetAsync(API_ADDR + "MagicItem").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<MagicItem>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<MonsterSpawn> GetMonsterSpawns()
    {
      List<MonsterSpawn> ret = new List<MonsterSpawn>();

      var response = client.GetAsync(API_ADDR + "MonsterSpawn").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<MonsterSpawn>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Month> GetMonths()
    {
      List<Month> ret = new List<Month>();

      var response = client.GetAsync(API_ADDR + "Month").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Month>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Plane> GetPlanes()
    {
      List<Plane> ret = new List<Plane>();

      var response = client.GetAsync(API_ADDR + "Plane").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Plane>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Season> GetSeasons()
    {
      List<Season> ret = new List<Season>();

      var response = client.GetAsync(API_ADDR + "Season").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Season>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<ListItemResult> GetSkillList()
    {
      var ret = new List<ListItemResult>();

      var response = client.GetAsync(API_ADDR + "Skill").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ListItemResult>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<ListItemResult> GetSpellList()
    {
      var ret = new List<ListItemResult>();

      var response = client.GetAsync(API_ADDR + "Spell").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ListItemResult>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Terrain> GetTerrains()
    {
      List<Terrain> ret = new List<Terrain>();

      var response = client.GetAsync(API_ADDR + "Terrain").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Terrain>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Territory> GetTerritories()
    {
      List<Territory> ret = new List<Territory>();

      var response = client.GetAsync(API_ADDR + "Territory").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Territory>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Time> GetTimes()
    {
      List<Time> ret = new List<Time>();

      var response = client.GetAsync(API_ADDR + "Time").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Time>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Weather> GetWeathers()
    {
      List<Weather> ret = new List<Weather>();

      var response = client.GetAsync(API_ADDR + "Weather").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Weather>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    #endregion
  }
}
