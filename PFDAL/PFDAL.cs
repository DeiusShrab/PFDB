using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace PFDAL
{
  public static class PFDAL
  {
    public static IPFDBContext GetContext()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      var Configuration = builder.Build();

      return GetContext(Configuration["WorkingEnvironment"]);
    }

    public static IPFDBContext GetContext(bool isTest)
    {
      if (isTest)
        return new TestContext();

      return new PFDBContext();
    }

    public static IPFDBContext GetContext(string environment)
    {
      return GetContext(!environment.Equals(Env.LIVE.ToString()));
    }

    public enum Env
    {
      LIVE,
      TEST
    }
  }
}
