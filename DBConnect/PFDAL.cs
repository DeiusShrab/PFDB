﻿namespace DBConnect
{
  public static class PFDAL
  {
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
  }
}
