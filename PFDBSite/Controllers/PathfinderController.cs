using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using DBConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  [Authorize]
  public class PathfinderController : PFDBController
  {
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Maps()
    {
      return View();
    }

    public IActionResult Campaigns(bool redirectToActive = true)
    {
      if (redirectToActive && Player != null)
      {
        var campaign = Player.PlayerCampaigns.Where(x => x.Campaign.IsActive).FirstOrDefault()?.CampaignId;
        if (campaign.HasValue)
          return CampaignLiveDisplay(campaign.Value);
      }

      return View();
    }

    [HttpGet("Campaigns/{campaignId}")]
    public IActionResult CampaignLiveDisplay(int campaignId)
    {
      return View();
    }

    public IActionResult LiveDisplay()
    {
      var model = new LiveDisplayModel
      {
        ActivePlayer = Player
      };
      return View();
    }

    [Authorize(Roles = "Player")]
    public IActionResult Characters()
    {
        // List all player-owned characters and equipment, plus options to make new ones

        return View();
    }

    [Authorize(Roles = "Player")]
    [HttpGet("Characters/{characterId:int}")]
    public IActionResult CharacterSheet(int characterId)
    {
      var context = PFDAL.GetContext();
      var character = context.Character.Find(characterId);
      if (character != null)
      {
        var model = new CharacterSheetViewModel();
        model.Character = character;

        return View(model);
      }

      return NotFound();
    }

    private async void SignIn(ClaimsIdentity claimsIdentity, AuthenticationProperties authProperties)
    {
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    private async void SignOut()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
  }
}