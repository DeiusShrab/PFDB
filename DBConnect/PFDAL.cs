using System;
using System.Linq;
using System.Security.Cryptography;
using DBConnect.DBModels;

namespace DBConnect
{
  public static class PFDAL
  {
    private static int ITERATIONS = 999;
    private static int SALTSIZE = 16;
    private static int KEYSIZE = 32;

    public static IPFDBContext GetContext()
    {
      if (PFConfig.ConfigExists())
      {
        // Only return live config if APP_MODE == "LIVE"
        return GetContext(!PFConfig.APP_MODE.Equals("LIVE", System.StringComparison.InvariantCultureIgnoreCase));
      }

#if DEBUG
      return GetContext(true);
#else
      return GetContext(false);
#endif
    }

    public static IPFDBContext GetContext(bool isTest)
    {
      if (isTest)
        return new TestContext();

      return new PFDBContext();
    }

    public static bool UpdatePassword(string username, string oldPass, string newPass)
    {
      var ret = false;

      var context = GetContext();
      var player = context.Player.Find(username);
      if (player != null && ValidatePassword(player, oldPass))
      {
        var rand = new Random();
        var salt = new byte[SALTSIZE];
        rand.NextBytes(salt);

        var key = new Rfc2898DeriveBytes(newPass, salt, ITERATIONS);
        var hash = key.GetBytes(KEYSIZE);

        player.Salt = Convert.ToBase64String(salt);
        player.Password = Convert.ToBase64String(hash);

        if (context.SaveChanges() > 0)
          ret = true;
      }

      return ret;
    }

    public static bool ValidatePassword(Player player, string password)
    {
      if (string.IsNullOrWhiteSpace(player.Password))
        return true;

      var ret = false;

      var salt = System.Text.Encoding.UTF8.GetBytes(player.Salt);
      var hash = System.Text.Encoding.UTF8.GetBytes(player.Password);

      var key = new Rfc2898DeriveBytes(password, salt, ITERATIONS);
      var testHash = key.GetBytes(KEYSIZE);

      if (testHash.SequenceEqual(hash))
        ret = true;

      return ret;
    }

    public static bool ValidatePassword(string username, string password)
    {
      var ret = false;

      var context = GetContext();
      var player = context.Player.Find(username);
      if (player != null)
      {
        return ValidatePassword(player, password);
      }

      return ret;
    }
  }
}
