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

    public static PFDBContext GetContext()
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

    public static PFDBContext GetContext(bool isTest)
    {
      if (isTest)
        return new TestContext();

      return new PFDBContext();
    }
  }
}
