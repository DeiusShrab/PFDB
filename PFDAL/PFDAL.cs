using System;
using System.Collections.Generic;
using System.Text;

namespace PFDAL
{
  public static class PFDAL
  {
    public static IPFDBContext GetContext(bool isTest)
    {
      //if (isTest)
      //  return new TestContext();

      return new PFDBContext();
    }

    public static IPFDBContext GetContext(string environment)
    {
      return GetContext(!environment.ToLower().Equals(Env.LIVE.ToString()));
    }

    public enum Env
    {
      LIVE,
      TEST
    }
  }
}
