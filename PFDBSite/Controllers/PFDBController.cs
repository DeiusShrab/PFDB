using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  public abstract class PFDBController : Controller
  {
    public static IConfiguration Configuration { get; set; }

    private DBConnect.DBModels.Player _player = null;

    protected DBConnect.DBModels.Player Player
    {
      get
      {
        if (_player != null)
          return _player;

        if (User == null || !User.IsInRole("Player"))
          return null;
        
        var appUser = User.Identity as ApplicationUser;

        _player = DBConnect.PFDAL.GetContext().Player.Find(appUser.PlayerId);
        return _player;
      }
    }

    public PFDBController()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      Configuration = builder.Build();
    }
  }
}
