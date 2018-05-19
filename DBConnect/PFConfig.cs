using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DBConnect
{
  public enum ConfigValues // Values found in "PFConfig.json"
  {
    APP_MODE, // If the application is in Debug mode
    JWT_KEY, // JWT Key to be used in authentication
    JWT_ISSUER, // JWT Issuer (and audience)
    API_USER, // Username for API authentication
    API_PASS, // Password for API authentication
    API_ADDR, // API Address
    MAX_CACHE_SIZE, // Max cache size for DBClient cache
    CAMPAIGN_ID, // ID of the Campaign
    DB_ADDR, // Database Address (set to localhost if the site is hosted on the same server as the DB)
    DB_USER, // Database Username
    DB_PASS, // Database Password
    DB_DB, // Database DB to use
    CERT_PASS, // Password for SSL Cert
  }

  public static class PFConfig
  {
    public static string APP_MODE => GetConfig(ConfigValues.APP_MODE);
    public static string JWT_KEY => GetConfig(ConfigValues.JWT_KEY);
    public static string JWT_ISSUER => GetConfig(ConfigValues.JWT_ISSUER);
    public static string API_USER => GetConfig(ConfigValues.API_USER);
    public static string API_PASS => GetConfig(ConfigValues.API_PASS);
    public static string API_ADDR => GetConfig(ConfigValues.API_ADDR);
    public static string MAX_CACHE_SIZE => GetConfig(ConfigValues.MAX_CACHE_SIZE);
    public static string CAMPAIGN_ID => GetConfig(ConfigValues.CAMPAIGN_ID);
    public static string DB_ADDR => GetConfig(ConfigValues.DB_ADDR);
    public static string DB_USER => GetConfig(ConfigValues.DB_USER);
    public static string DB_PASS => GetConfig(ConfigValues.DB_PASS);
    public static string DB_DB => GetConfig(ConfigValues.DB_DB);
    public static string CERT_PASS => GetConfig(ConfigValues.CERT_PASS);

    private static string CONFIG_FILE = "PFConfig.json";
    private static Dictionary<string, string> Configuration;

    static PFConfig()
    {
      if (ConfigExists())
        Configuration = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(CONFIG_FILE));
      else
      {
        Configuration = new Dictionary<string, string>();
      }

      foreach (ConfigValues item in System.Enum.GetValues(typeof(ConfigValues)))
      {
        if (!Configuration.ContainsKey(item.ToString()))
          Configuration[item.ToString()] = string.Empty;
      }
    }

    public static bool ConfigExists()
    {
      return File.Exists(CONFIG_FILE);
    }

    public static void UpdateConfigValues(IDictionary<string, string> newValues)
    {
      foreach (var key in newValues.Keys)
      {
        Configuration[key] = newValues[key];
      }

      File.WriteAllText(CONFIG_FILE, JsonConvert.SerializeObject(Configuration));
    }

    public static Dictionary<string, string> GetAllConfigValues()
    {
      return Configuration;
    }

    public static string GetConfig(string config)
    {
      if (Configuration.ContainsKey(config))
        return Configuration[config];

      return null;
    }

    public static string GetConfig(ConfigValues config)
    {
      return GetConfig(config.ToString());
    }
  }
}
