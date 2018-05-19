﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Net;

namespace PFDBSite
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
#if DEBUG
            .UseUrls("http://localhost:5555")
#else
            .UseKestrel(options =>
            {
              options.Listen(IPAddress.Any, 5555);
              options.Listen(IPAddress.Any, 5565, listenOptions =>
              {
                listenOptions.UseHttps("certificate.pfx", DBConnect.PFConfig.CERT_PASS);
              });
            })
#endif
            .UseStartup<Startup>()
        
            .Build();
  }
}
