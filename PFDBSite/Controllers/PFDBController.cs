using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PFDBSite.Controllers
{
  public abstract class PFDBController : Controller
  {
    public static IConfiguration Configuration { get; set; }

    public PFDBController()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      Configuration = builder.Build();
    }
  }
}
