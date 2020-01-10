using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PFDBSite.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace PFDBSite.Controllers
{
  public abstract class PFDBController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private static string Cookie_Player = DBConnect.PFConfig.COOKIE_PLAYER;
    public static IConfiguration Configuration { get; set; }

    public PFDBController(
      UserManager<ApplicationUser> userManager)
    {
      _userManager = userManager;

      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      Configuration = builder.Build();
    }

    protected PFDBCommon.DBModels.Player Player
    {
      get
      {
        var playerBytes = HttpContext.Session.Get(Cookie_Player);

        if (playerBytes == null || playerBytes.Length == 0)
        {
          PFDBCommon.DBModels.Player player = null;
          var tempId = new System.Random().Next(1000, 10000) * -1;
          player = new PFDBCommon.DBModels.Player
          {
            PlayerId = tempId,
            DisplayName = "Anonymous" + tempId.ToString(),
          };

          try
          {
            if (User != null && User.Identity != null && User.IsInRole("Player"))
            {
              var context = DBConnect.PFDAL.GetContext();
              var appUser = _userManager.Users.FirstOrDefault(x => x.NormalizedUserName.Equals(User.Identity.Name, StringComparison.InvariantCultureIgnoreCase));
              if (appUser != null && appUser.PlayerId > 0)
              {
                var result = context.Player.Where(x => x.PlayerId == appUser.PlayerId).Include("PlayerCampaigns.Campaign");
                if (result != null)
                {
                  player = result.FirstOrDefault();
                }
              }
            }

            var pString = JsonConvert.SerializeObject(player, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            Console.WriteLine(pString);
            playerBytes = Encoding.UTF8.GetBytes(pString);
            HttpContext.Session.Set(Cookie_Player, playerBytes);
          }
          catch (Exception ex)
          {
            Console.WriteLine(ex.Message);
          }
        }

        var playerString = Encoding.UTF8.GetString(playerBytes);
        var _player = JsonConvert.DeserializeObject<PFDBCommon.DBModels.Player>(playerString);
        return _player;
      }
    }
  }
}
