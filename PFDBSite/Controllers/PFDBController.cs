using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  public abstract class PFDBController : Controller
  {
    private static string Cookie_Player = DBConnect.PFConfig.COOKIE_PLAYER;
    public static IConfiguration Configuration { get; set; }

    protected DBConnect.DBModels.Player Player
    {
      get
      {
        var playerBytes = HttpContext.Session.Get(Cookie_Player);

        if (playerBytes == null || playerBytes.Length == 0)
        {
          DBConnect.DBModels.Player player = null;

          if (User == null || User.Identity == null || !User.IsInRole("Player"))
          {
            var tempId = new System.Random().Next(1000, 10000) * -1;
            player = new DBConnect.DBModels.Player
            {
              PlayerId = tempId,
              DisplayName = "Anonymous" + tempId.ToString(),
            };            
          }
          else
          {
            var appUser = User.Identity as ApplicationUser;
            player = DBConnect.PFDAL.GetContext().Player.Find(appUser.PlayerId);
          }

          var pString = JsonConvert.SerializeObject(player);
          playerBytes = Encoding.UTF8.GetBytes(pString);
          HttpContext.Session.Set(Cookie_Player, playerBytes);
        }

        var playerString = Encoding.UTF8.GetString(playerBytes);
        var _player = JsonConvert.DeserializeObject<DBConnect.DBModels.Player>(playerString);
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
