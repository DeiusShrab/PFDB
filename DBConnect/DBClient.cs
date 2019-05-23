using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Timers;
using DBConnect.ConnectModels;
using DBConnect.DBModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DBConnect
{

  public static class DBClient
  {
    private static readonly HttpClient client = new HttpClient();
    private static DBCache<BestiaryDetail> DetailCache;
    private static string API_TOKEN;
    private static string API_ADDR;
    private static int MAX_CACHE_SIZE;
    private static System.DateTime TOKEN_DATE;
    private static Timer ApiTimer;

    static DBClient()
    {
      ReloadConfig(false);
    }

    #region Token

    // Called by applications to connect, not needed by PFDBSite
    public static bool ConnectToApi()
    {
      DetailCache = new DBCache<BestiaryDetail>(MAX_CACHE_SIZE);
      return RefreshToken();
    }

    public static void ReloadConfig(bool reconnectToApi)
    {
      API_ADDR = PFConfig.API_ADDR;
      int.TryParse(PFConfig.MAX_CACHE_SIZE, out int mcs);
      MAX_CACHE_SIZE = mcs;

      if (reconnectToApi)
        ConnectToApi();
    }

    private static bool RefreshToken()
    {
      try
      {
        var testResponse = client.GetAsync(API_ADDR + "test").Result;
        if (testResponse.IsSuccessStatusCode)
        {
          var body = "{" + $"'username': '{PFConfig.API_USER}', 'password': '{PFConfig.API_PASS}'" + "}";
          var response = client.PostAsync(API_ADDR + "GetToken/pls", new StringContent(body, Encoding.UTF8, "application/json")).Result;
          if (response.IsSuccessStatusCode)
          {

            var content = response.Content;
            var token = JsonConvert.DeserializeObject<JObject>(content.ReadAsStringAsync().Result);
            if (token.ContainsKey("token") && !string.IsNullOrWhiteSpace(token["token"].Value<string>()))
            {
              API_TOKEN = token["token"].Value<string>();
              TOKEN_DATE = System.DateTime.Now;
              client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", API_TOKEN);
              ApiTimer = new Timer(System.TimeSpan.FromHours(12).TotalMilliseconds);
              ApiTimer.Elapsed += ApiTimer_Elapsed;
              ApiTimer.Start();
            }

            return true;
          }
        }
      }
      catch(System.Exception ex)
      {

      }

      return false;
    }

    private static void ApiTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      RefreshToken();
    }

    #endregion

    #region Queries

    public static Dictionary<string, string> GetCampaignData()
    {
      Dictionary<string, string> ret = null;
      var campaign = PFConfig.CAMPAIGN_ID;

      if (!string.IsNullOrWhiteSpace(campaign))
      {
        var response = client.GetAsync(API_ADDR + "CampaignData/" + campaign).Result;
        if (response.IsSuccessStatusCode)
        {
          var content = response.Content;
          ret = JsonConvert.DeserializeObject<Dictionary<string, string>>(content.ReadAsStringAsync().Result);
        }
      }

      return ret;
    }

    public static bool UpdateCampaignData(Dictionary<string, string> campaignData)
    {
      var campaign = PFConfig.CAMPAIGN_ID;
      if (campaignData != null && campaign != null)
      {
        var body = JsonConvert.SerializeObject(campaignData);
        var response = client.PostAsync(API_ADDR + "CampaignData/" + campaign, new StringContent(body, Encoding.UTF8, "application/json")).Result;
        if (response.IsSuccessStatusCode)
          return true;
      }

      return false;
    }

    public static List<ListItemResult> GetList(string listType)
    {
      var ret = new List<ListItemResult>();

      var response = client.GetAsync(API_ADDR + "List/" + listType).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ListItemResult>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static RandomEncounterResult GetEncounters(RandomEncounterRequest request)
    {
      var ret = new RandomEncounterResult
      {
        Success = false
      };

      var body = JsonConvert.SerializeObject(request);
      var response = client.PostAsync(API_ADDR + "RandomEncounter", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<RandomEncounterResult>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static RandomWeatherResult GetRandomWeatherList(RandomWeatherRequest request)
    {
      var ret = new RandomWeatherResult
      {
        Success = false
      };

      var body = JsonConvert.SerializeObject(request);
      var response = client.PostAsync(API_ADDR + "RandomWeather", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<RandomWeatherResult>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static bool UpdateCurrentDate(string fantasyDate)
    {
      var response = client.PostAsync(API_ADDR + "FantasyDate", new StringContent(fantasyDate, Encoding.UTF8)).Result;
      return response.IsSuccessStatusCode;
    }

    public static List<MonsterSpawn> GetMonsterSpawns(int bestiaryId)
    {
      var ret = new List<MonsterSpawn>();

      var response = client.GetAsync(API_ADDR + "Spawns/" + bestiaryId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<MonsterSpawn>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static bool UpdateMonsterSpawns(SpawnUpdateRequest request)
    {
      var body = JsonConvert.SerializeObject(request);
      var response = client.PostAsync(API_ADDR + "Spawns/" + request.BestiaryId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        return true;
      else
        return false;
    }

    public static List<ContinentWeather> GetContinentWeathers(int continentId, int seasonId)
    {
      var ret = new List<ContinentWeather>();

      var response = client.GetAsync($"{API_ADDR}ContinentWeathers?continentId={continentId}&seasonId={seasonId}").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<ContinentWeather>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static bool UpdateContinentWeathers(ContinentWeatherUpdateRequest request)
    {
      var body = JsonConvert.SerializeObject(request);
      var response = client.PostAsync(API_ADDR + "ContinentWeathers", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        return true;
      else
        return false;
    }

    public static List<Environment> GetEnvironmentsForContinent(int continentId)
    {
      var ret = new List<Environment>();

      var response = client.GetAsync($"{API_ADDR}EnvironmentsForContinent?continentId={continentId}").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Environment>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<MonsterSpawnEdit> GetMonsterSpawnsForEdit()
    {
      var ret = new List<MonsterSpawnEdit>();

      var response = client.GetAsync($"{API_ADDR}MonsterSpawnEdit").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        var contentString = content.ReadAsStringAsync().Result;
        ret = JsonConvert.DeserializeObject<List<MonsterSpawnEdit>>(contentString);
      }

      return ret;
    }

    #endregion

    #region Details

    public static Bestiary GetBestiary(int BestiaryId)
    {
      Bestiary ret = null;

      var response = client.GetAsync(API_ADDR + "Bestiary/" + BestiaryId.ToString()).Result;
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
    /// <param name="BestiaryId">the id of the object</param>
    /// <param name="forceRetrieve">For PFEditor use, if true, the cache will be skipped</param>
    /// <returns></returns>
    public static BestiaryDetail GetBestiaryDetail(int BestiaryId, bool forceRetrieve = false)
    {
      BestiaryDetail ret = null;

      if (!forceRetrieve && DetailCache.ContainsKey(BestiaryId))
        ret = DetailCache[BestiaryId];
      else
      {
        var response = client.GetAsync(API_ADDR + "BestiaryDetail/" + BestiaryId.ToString()).Result;
        if (response.IsSuccessStatusCode)
        {
          var content = response.Content;
          ret = JsonConvert.DeserializeObject<BestiaryDetail>(content.ReadAsStringAsync().Result);
        }

        if (!forceRetrieve && ret != null)
          DetailCache.Add(BestiaryId, ret);
      }

      return ret;
    }

    public static BestiaryEnvironment GetBestiaryEnvironment(int BestiaryEnvironmentId)
    {
      BestiaryEnvironment ret = null;

      var response = client.GetAsync(API_ADDR + "BestiaryEnvironment/" + BestiaryEnvironmentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryEnvironment>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryFeat GetBestiaryFeat(int BestiaryFeatId)
    {
      BestiaryFeat ret = null;

      var response = client.GetAsync(API_ADDR + "BestiaryFeat/" + BestiaryFeatId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryFeat>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryLanguage GetBestiaryLanguage(int BestiaryLanguageId)
    {
      BestiaryLanguage ret = null;

      var response = client.GetAsync(API_ADDR + "BestiaryLanguage/" + BestiaryLanguageId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryLanguage>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiarySkill GetBestiarySkill(int BestiarySkillId)
    {
      BestiarySkill ret = null;

      var response = client.GetAsync(API_ADDR + "BestiarySkill/" + BestiarySkillId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiarySkill>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiarySubType GetBestiarySubType(int BestiarySubTypeId)
    {
      BestiarySubType ret = null;

      var response = client.GetAsync(API_ADDR + "BestiarySubType/" + BestiarySubTypeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiarySubType>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryType GetBestiaryType(int BestiaryTypeId)
    {
      BestiaryType ret = null;

      var response = client.GetAsync(API_ADDR + "BestiaryType/" + BestiaryTypeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryType>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Campaign GetCampaign(int CampaignId)
    {
      Campaign ret = null;

      var response = client.GetAsync(API_ADDR + "Campaign/" + CampaignId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Campaign>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Continent GetContinent(int ContinentId)
    {
      Continent ret = null;

      var response = client.GetAsync(API_ADDR + "Continent/" + ContinentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Continent>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static ContinentWeather GetContinentWeather(int ContinentWeatherId)
    {
      ContinentWeather ret = null;

      var response = client.GetAsync(API_ADDR + "ContinentWeather/" + ContinentWeatherId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<ContinentWeather>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Environment GetEnvironment(int EnvironmentId)
    {
      Environment ret = null;

      var response = client.GetAsync(API_ADDR + "Environment/" + EnvironmentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Environment>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Faction GetFaction(int FactionId)
    {
      Faction ret = null;

      var response = client.GetAsync(API_ADDR + "Faction/" + FactionId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Faction>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Feat GetFeat(int FeatId)
    {
      Feat ret = null;

      var response = client.GetAsync(API_ADDR + "Feat/" + FeatId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Feat>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Language GetLanguage(int LanguageId)
    {
      Language ret = null;

      var response = client.GetAsync(API_ADDR + "Language/" + LanguageId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Language>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Location GetLocation(int LocationId)
    {
      Location ret = null;

      var response = client.GetAsync(API_ADDR + "Location/" + LocationId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Location>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static MagicItem GetMagicItem(int MagicItemId)
    {
      MagicItem ret = null;

      var response = client.GetAsync(API_ADDR + "MagicItem/" + MagicItemId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<MagicItem>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Month GetMonth(int MonthId)
    {
      Month ret = null;

      var response = client.GetAsync(API_ADDR + "Month/" + MonthId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Month>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Plane GetPlane(int PlaneId)
    {
      Plane ret = null;

      var response = client.GetAsync(API_ADDR + "Plane/" + PlaneId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Plane>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Season GetSeason(int SeasonId)
    {
      Season ret = null;

      var response = client.GetAsync(API_ADDR + "Season/" + SeasonId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Season>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Skill GetSkill(int SkillId)
    {
      Skill ret = null;

      var response = client.GetAsync(API_ADDR + "Skill/" + SkillId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Skill>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Spell GetSpell(int SpellId)
    {
      Spell ret = null;

      var response = client.GetAsync(API_ADDR + "Spell/" + SpellId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Spell>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static SpellDetail GetSpellDetail(int SpellId)
    {
      SpellDetail ret = null;

      var response = client.GetAsync(API_ADDR + "SpellDetail/" + SpellId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<SpellDetail>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Terrain GetTerrain(int TerrainId)
    {
      Terrain ret = null;

      var response = client.GetAsync(API_ADDR + "Terrain/" + TerrainId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Terrain>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Territory GetTerritory(int TerritoryId)
    {
      Territory ret = null;

      var response = client.GetAsync(API_ADDR + "Territory/" + TerritoryId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Territory>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Time GetTime(int TimeId)
    {
      Time ret = null;

      var response = client.GetAsync(API_ADDR + "Time/" + TimeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Time>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static TrackedEvent GetTrackedEvent(int TrackedEventId)
    {
      TrackedEvent ret = null;

      var response = client.GetAsync(API_ADDR + "TrackedEvent/" + TrackedEventId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<TrackedEvent>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Weather GetWeather(int WeatherId)
    {
      Weather ret = null;

      var response = client.GetAsync(API_ADDR + "Weather/" + WeatherId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Weather>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }


    #endregion

    #region Get All

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

    public static List<Campaign> GetCampaigns()
    {
      List<Campaign> ret = new List<Campaign>();

      var response = client.GetAsync(API_ADDR + "Campaign").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Campaign>>(content.ReadAsStringAsync().Result);
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

    public static List<TrackedEvent> GetTrackedEvents()
    {
      List<TrackedEvent> ret = new List<TrackedEvent>();

      var response = client.GetAsync(API_ADDR + "TrackedEvent").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<TrackedEvent>>(content.ReadAsStringAsync().Result);
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

    #region Create

    public static Bestiary CreateBestiary(Bestiary obj)
    {
      Bestiary ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Bestiary", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Bestiary>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    // BestiaryDetail created with Bestiary

    public static BestiaryEnvironment CreateBestiaryEnvironment(BestiaryEnvironment obj)
    {
      BestiaryEnvironment ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryEnvironment", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryEnvironment>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryFeat CreateBestiaryFeat(BestiaryFeat obj)
    {
      BestiaryFeat ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryFeat", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryFeat>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryLanguage CreateBestiaryLanguage(BestiaryLanguage obj)
    {
      BestiaryLanguage ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryLanguage", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryLanguage>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiarySkill CreateBestiarySkill(BestiarySkill obj)
    {
      BestiarySkill ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiarySkill", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiarySkill>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiarySubType CreateBestiarySubType(BestiarySubType obj)
    {
      BestiarySubType ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiarySubType", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiarySubType>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static BestiaryType CreateBestiaryType(BestiaryType obj)
    {
      BestiaryType ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryType", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<BestiaryType>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Campaign CreateCampaign(Campaign obj)
    {
      Campaign ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Campaign", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Campaign>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Continent CreateContinent(Continent obj)
    {
      Continent ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Continent", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Continent>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static ContinentWeather CreateContinentWeather(ContinentWeather obj)
    {
      ContinentWeather ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "ContinentWeather", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<ContinentWeather>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Environment CreateEnvironment(Environment obj)
    {
      Environment ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Environment", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Environment>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Faction CreateFaction(Faction obj)
    {
      Faction ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Faction", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Faction>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Feat CreateFeat(Feat obj)
    {
      Feat ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Feat", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Feat>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Language CreateLanguage(Language obj)
    {
      Language ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Language", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Language>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Location CreateLocation(Location obj)
    {
      Location ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Location", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Location>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static MagicItem CreateMagicItem(MagicItem obj)
    {
      MagicItem ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "MagicItem", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<MagicItem>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static MonsterSpawn CreateMonsterSpawn(MonsterSpawn obj)
    {
      MonsterSpawn ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "MonsterSpawn", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<MonsterSpawn>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Month CreateMonth(Month obj)
    {
      Month ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Month", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Month>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Plane CreatePlane(Plane obj)
    {
      Plane ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Plane", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Plane>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Season CreateSeason(Season obj)
    {
      Season ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Season", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Season>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Skill CreateSkill(Skill obj)
    {
      Skill ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Skill", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Skill>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Spell CreateSpell(Spell obj)
    {
      Spell ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Spell", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Spell>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    // SpellDetail created with Spell

    public static SpellSchool CreateSpellSchool(SpellSchool obj)
    {
      SpellSchool ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "SpellSchool", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<SpellSchool>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static SpellSubSchool CreateSpellSubSchool(SpellSubSchool obj)
    {
      SpellSubSchool ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "SpellSubSchool", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<SpellSubSchool>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Terrain CreateTerrain(Terrain obj)
    {
      Terrain ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Terrain", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Terrain>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Territory CreateTerritory(Territory obj)
    {
      Territory ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Territory", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Territory>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Time CreateTime(Time obj)
    {
      Time ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Time", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Time>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static TrackedEvent CreateTrackedEvent(TrackedEvent obj)
    {
      TrackedEvent ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "TrackedEvent", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<TrackedEvent>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static Weather CreateWeather(Weather obj)
    {
      Weather ret = null;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Weather", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<Weather>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }


    #endregion

    #region Update

    public static bool UpdateBestiary(Bestiary obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Bestiary/" + obj.BestiaryId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiaryDetail(BestiaryDetail obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiaryDetail/" + obj.BestiaryId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiaryEnvironment(BestiaryEnvironment obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiaryEnvironment/" + obj.BestiaryEnvironmentId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiaryFeat(BestiaryFeat obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiaryFeat/" + obj.BestiaryFeatId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiaryLanguage(BestiaryLanguage obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiaryLanguage/" + obj.BestiaryLanguageId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiarySkill(BestiarySkill obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiarySkill/" + obj.BestiarySkillId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiarySubType(BestiarySubType obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiarySubType/" + obj.BestiarySubTypeId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateBestiaryType(BestiaryType obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "BestiaryType/" + obj.BestiaryTypeId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateCampaign(Campaign obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Campaign/" + obj.CampaignId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateContinent(Continent obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Continent/" + obj.ContinentId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateContinentWeather(ContinentWeather obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "ContinentWeather/" + obj.CWID.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateEnvironment(Environment obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Environment/" + obj.EnvironmentId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateFaction(Faction obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Faction/" + obj.FactionId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateFeat(Feat obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Feat/" + obj.FeatId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateLanguage(Language obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Language/" + obj.LanguageId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateLocation(Location obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Location/" + obj.LocationId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateMagicItem(MagicItem obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "MagicItem/" + obj.MagicItemId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateMonsterSpawn(MonsterSpawn obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "MonsterSpawn/" + obj.SpawnId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateMonth(Month obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Month/" + obj.MonthId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateSeason(Season obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Season/" + obj.SeasonId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateSpell(Spell obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Spell/" + obj.SpellId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateSpellDetail(SpellDetail obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "SpellDetail/" + obj.SpellId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateTerritory(Territory obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Territory/" + obj.TerritoryId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateWeather(Weather obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Weather/" + obj.WeatherId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateTerrain(Terrain obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Terrain/" + obj.TerrainId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateTime(Time obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Time/" + obj.TimeId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateTrackedEvent(TrackedEvent obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "TrackedEvent/" + obj.TrackedEventId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdatePlane(Plane obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Plane/" + obj.PlaneId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool UpdateSkill(Skill obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PutAsync(API_ADDR + "Skill/" + obj.SkillId.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    #endregion

    #region Delete

    public static bool DeleteBestiary(int BestiaryId)
    {
      var response = client.DeleteAsync(API_ADDR + "Bestiary/" + BestiaryId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    // BestiaryDetail deleted with Bestiary

    public static bool DeleteBestiaryEnvironment(int BestiaryEnvironmentId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiaryEnvironment/" + BestiaryEnvironmentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteBestiaryFeat(int BestiaryFeatId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiaryFeat/" + BestiaryFeatId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteBestiaryLanguage(int BestiaryLanguageId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiaryLanguage/" + BestiaryLanguageId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteBestiarySkill(int BestiarySkillId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiarySkill/" + BestiarySkillId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteBestiarySubType(int BestiarySubTypeId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiarySubType/" + BestiarySubTypeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteBestiaryType(int BestiaryTypeId)
    {
      var response = client.DeleteAsync(API_ADDR + "BestiaryType/" + BestiaryTypeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteCampaign(int CampaignId)
    {
      var response = client.DeleteAsync(API_ADDR + "Campaign/" + CampaignId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteContinent(int ContinentId)
    {
      var response = client.DeleteAsync(API_ADDR + "Continent/" + ContinentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteContinentWeather(int ContinentWeatherId)
    {
      var response = client.DeleteAsync(API_ADDR + "ContinentWeather/" + ContinentWeatherId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteEnvironment(int EnvironmentId)
    {
      var response = client.DeleteAsync(API_ADDR + "Environment/" + EnvironmentId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteFaction(int FactionId)
    {
      var response = client.DeleteAsync(API_ADDR + "Faction/" + FactionId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteFeat(int FeatId)
    {
      var response = client.DeleteAsync(API_ADDR + "Feat/" + FeatId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteLanguage(int LanguageId)
    {
      var response = client.DeleteAsync(API_ADDR + "Language/" + LanguageId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteLocation(int LocationId)
    {
      var response = client.DeleteAsync(API_ADDR + "Location/" + LocationId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteMagicItem(int MagicItemId)
    {
      var response = client.DeleteAsync(API_ADDR + "MagicItem/" + MagicItemId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteMonsterSpawn(int MonsterSpawnId)
    {
      var response = client.DeleteAsync(API_ADDR + "MonsterSpawn/" + MonsterSpawnId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteMonth(int MonthId)
    {
      var response = client.DeleteAsync(API_ADDR + "Month/" + MonthId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeletePlane(int PlaneId)
    {
      var response = client.DeleteAsync(API_ADDR + "Plane/" + PlaneId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteSeason(int SeasonId)
    {
      var response = client.DeleteAsync(API_ADDR + "Season/" + SeasonId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteSkill(int SkillId)
    {
      var response = client.DeleteAsync(API_ADDR + "Skill/" + SkillId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteSpell(int SpellId)
    {
      var response = client.DeleteAsync(API_ADDR + "Spell/" + SpellId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    // SpellDetail deleted with Spell

    public static bool DeleteSpellSchool(int SpellSchoolId)
    {
      var response = client.DeleteAsync(API_ADDR + "SpellSchool/" + SpellSchoolId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteSpellSubSchool(int SpellSubSchoolId)
    {
      var response = client.DeleteAsync(API_ADDR + "SpellSubSchool/" + SpellSubSchoolId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteTerrain(int TerrainId)
    {
      var response = client.DeleteAsync(API_ADDR + "Terrain/" + TerrainId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteTerritory(int TerritoryId)
    {
      var response = client.DeleteAsync(API_ADDR + "Territory/" + TerritoryId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteTime(int TimeId)
    {
      var response = client.DeleteAsync(API_ADDR + "Time/" + TimeId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteTrackedEvent(int TrackedEventId)
    {
      var response = client.DeleteAsync(API_ADDR + "TrackedEvent/" + TrackedEventId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    public static bool DeleteWeather(int WeatherId)
    {
      var response = client.DeleteAsync(API_ADDR + "Weather/" + WeatherId.ToString()).Result;
      if (response.IsSuccessStatusCode)
        return true;

      return false;
    }

    #endregion
  }
}
