using System;
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
    private static int MAX_CACHE_SIZE = 10;
    private static readonly HttpClient client = new HttpClient();
    private static Dictionary<int, BestiaryDetail> DetailCache = new Dictionary<int, BestiaryDetail>();

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

    public static BestiaryDetail GetBestiaryDetail(int bestiaryId)
    {
      BestiaryDetail ret = null;

      if (DetailCache.ContainsKey(bestiaryId))
        ret = DetailCache[bestiaryId];
      else
      {
        if (DetailCache.Count >= MAX_CACHE_SIZE)
          DetailCache.Clear();

        var response = client.GetAsync(API_ADDR + "BestiaryDetail/" + bestiaryId.ToString()).Result;
        if (response.IsSuccessStatusCode)
        {
          var content = response.Content;
          ret = JsonConvert.DeserializeObject<BestiaryDetail>(content.ReadAsStringAsync().Result);
        }

        if (ret != null)
          DetailCache.Add(bestiaryId, ret);
      }

      return ret;
    }

    public static List<Continent> GetContinents()
    {
      List<Continent> ret = new List<Continent>();

      var response = client.GetAsync(API_ADDR + "Continents").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Continent>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }

    public static List<Season> GetSeasons()
    {
      List<Season> ret = new List<Season>();

      var response = client.GetAsync(API_ADDR + "Seasons").Result;
      if (response.IsSuccessStatusCode)
      {
        var content = response.Content;
        ret = JsonConvert.DeserializeObject<List<Season>>(content.ReadAsStringAsync().Result);
      }

      return ret;
    }
  }
}
