using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace PFAPI
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)

            .UseKestrel(options =>
            {
              options.Listen(IPAddress.Any, 5556);
#if !DEBUG
              options.Listen(IPAddress.Any, 5566, listenOptions =>
              {
                listenOptions.UseHttps("apicertificate.pfx", DBConnect.PFConfig.CERT_PASS);
              });
#endif
            })
            .UseStartup<Startup>()

            .Build();
  }
}
