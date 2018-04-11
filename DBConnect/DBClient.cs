using System.Collections.Generic;
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
    public static string JWT_KEY = "ayy lmao ayy lmao ayy lmao ayy lmao";
    public static string JWT_ISSUER = "https://zratsewk.duckdns.org";
    public static string API_USER = "PFHelper";
    public static string API_PASS = "PFHelper";
    private static readonly string API_ADDR;
    private static int MAX_CACHE_SIZE = 32;
    private static readonly HttpClient client = new HttpClient();
    private static DBCache<BestiaryDetail> DetailCache = new DBCache<BestiaryDetail>(MAX_CACHE_SIZE);

    private static string API_TOKEN;
    private static System.DateTime TOKEN_DATE;
    private static Timer ApiTimer;

    #region Constructor and Token

    static DBClient()
    {
#if DEBUG
      API_ADDR = @"http://localhost:51923/api/";
#else
      API_ADDR = @"https://zratsewk.duckdns.org/pfdb/api/";
#endif
      RefreshToken();
    }

    private static void RefreshToken()
    {
      var body = "{" + $"'username': '{API_USER}', 'password': '{API_PASS}'" + "}";
      var response = client.PostAsync(API_ADDR + "GetToken/pls", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        var token = JsonConvert.DeserializeObject<JObject>(content.ReadAsStringAsync().Result);
        if (token.ContainsKey("token") && token["token"].HasValues)
        {
          API_TOKEN = token["token"].Value<string>();
          TOKEN_DATE = System.DateTime.Now;
          client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", API_TOKEN);
          ApiTimer = new Timer(System.TimeSpan.FromHours(12).TotalMilliseconds);
          ApiTimer.Elapsed += ApiTimer_Elapsed;
          ApiTimer.Start();
        }
      }
    }

    private static void ApiTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      RefreshToken();
    }

    #endregion

    #region Queries

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

    public static MonsterSpawn GetMonsterSpawn(int SpawnId)
    {
      MonsterSpawn ret = null;

      var response = client.GetAsync(API_ADDR + "MonsterSpawn/" + SpawnId.ToString()).Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<MonsterSpawn>(content.ReadAsStringAsync().Result);
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

    public static bool CreateBestiary(Bestiary obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Bestiary", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiaryDetail(BestiaryDetail obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryDetail", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiaryEnvironment(BestiaryEnvironment obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryEnvironment", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiaryFeat(BestiaryFeat obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryFeat", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiaryLanguage(BestiaryLanguage obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryLanguage", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiarySkill(BestiarySkill obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiarySkill", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiarySubType(BestiarySubType obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiarySubType", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateBestiaryType(BestiaryType obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "BestiaryType", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateContinent(Continent obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Continent", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateContinentWeather(ContinentWeather obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "ContinentWeather", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateEnvironment(Environment obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Environment", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateFaction(Faction obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Faction", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateFeat(Feat obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Feat", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateLanguage(Language obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Language", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateLocation(Location obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Location", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateMagicItem(MagicItem obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "MagicItem", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateMonsterSpawn(MonsterSpawn obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "MonsterSpawn", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateMonth(Month obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Month", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateSeason(Season obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Season", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateSpell(Spell obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Spell", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateSpellDetail(SpellDetail obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "SpellDetail", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateTerritory(Territory obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Territory", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateWeather(Weather obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Weather", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateTerrain(Terrain obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Terrain", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateTime(Time obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Time", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateTrackedEvent(TrackedEvent obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "TrackedEvent", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreatePlane(Plane obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Plane", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

      return ret;
    }

    public static bool CreateSkill(Skill obj)
    {
      var ret = false;

      var body = JsonConvert.SerializeObject(obj);
      var response = client.PostAsync(API_ADDR + "Skill", new StringContent(body, Encoding.UTF8, "application/json")).Result;
      if (response.IsSuccessStatusCode)
        ret = true;

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
      var response = client.PutAsync(API_ADDR + "ContinentWeather/" + obj.Cwid.ToString(), new StringContent(body, Encoding.UTF8, "application/json")).Result;
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
  }
}
