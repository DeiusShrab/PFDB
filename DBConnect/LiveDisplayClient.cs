using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Timers;

namespace DBConnect
{
  public static class LiveDisplayClient
  {
    private static readonly HttpClient client = new HttpClient();
    private static string API_TOKEN;
    private static string API_ADDR;
    private static System.DateTime TOKEN_DATE;
    private static Timer ApiTimer;

    static LiveDisplayClient()
    {
      ReloadConfig(true);
    }

    #region Token

    public static void ReloadConfig(bool reconnectToApi)
    {
      API_ADDR = PFConfig.SITE_ADDR + "api/v1/LiveDisplay/";

      if (reconnectToApi)
        RefreshToken();
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
      catch (System.Exception ex)
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

    public static bool UpdateDate(int campaignId)
    {
      var ret = false;

      var response = client.GetAsync(API_ADDR + "UpdateDate/" + campaignId).Result;
      if (response.IsSuccessStatusCode)
      {
        ret = true;
      }
      else
      {
        Console.WriteLine(response.ReasonPhrase);
      }

      return ret;
    }

    public static bool GetMessageHistory(int chatRoomId, int playerId)
    {
      // TODO: actual message history
      var ret = false;

      var response = client.GetAsync(API_ADDR + "MessageHistory/" + chatRoomId + "/" + playerId).Result;
      if (response.IsSuccessStatusCode)
      {
        ret = true;
      }
      else
      {
        Console.WriteLine(response.ReasonPhrase);
      }

      return ret;
    }

    #endregion
  }
}
