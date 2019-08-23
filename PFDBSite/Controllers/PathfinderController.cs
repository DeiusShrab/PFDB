using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using DBConnect;
using DBConnect.ConnectModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  [Authorize]
  public class PathfinderController : PFDBController
  {
    public PathfinderController(UserManager<ApplicationUser> userManager) : base(userManager)
    {

    }

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
      var model = new CampaignsModel();

      if (Player == null || !Player.PlayerCampaigns.Any(x => x.Campaign.IsActive))
      {
        model.CampaignList.Add(-1, "NO ACTIVE CAMPAIGNS");
      }
      else if (redirectToActive)
      {
        var campaign = Player.PlayerCampaigns.Where(x => x.Campaign.IsActive).FirstOrDefault()?.CampaignId;
        if (campaign.HasValue)
          return CampaignLiveDisplay(campaign.Value);
      }
      else
      {
        foreach (var campaign in Player.PlayerCampaigns)
        {
          model.CampaignList.Add(campaign.CampaignId, campaign.Campaign.CampaignName);
        }
      }

      return View(model);
    }

    [HttpGet("Campaigns/{campaignId}")]
    public IActionResult CampaignLiveDisplay(int campaignId)
    {
      var context = PFDAL.GetContext();
      var campaign = context.Campaign.Find(campaignId);

      if (campaign == null || campaign.CampaignId == 0)
        return NotFound();

      var model = new LiveDisplayModel()
      {
        ActivePlayer = Player,
        CampaignId = campaignId,
        CampaignName = campaign.CampaignName,
        FantasyDate = "{}",
        ChatRoom = new DBConnect.DBModels.ChatRoom
        {
          ChatRoomId = -1,
          ChatRoomName = "NONE"
        }
      };
      
      var campaignData = context.CampaignData.Where(x => x.CampaignId == campaign.CampaignId);
      if (campaignData != null)
      {
        var campaignDate = campaignData.FirstOrDefault(x => x.Key == PFConfig.STR_FANTASYDATE);
        if (campaignDate != null)
        {
          var seasons = context.Season.ToList();
          var months = context.Month.ToList();
          var CurrentDate = new FantasyDate(campaignDate.Value);
          var CurrentMonth = months.FirstOrDefault(x => x.MonthOrder == CurrentDate.Month);
          var grandDate = $"YEAR {CurrentDate.Year} AA, Season of {seasons.FirstOrDefault(x => x.SeasonId == CurrentMonth.SeasonId)?.Name}, Month of {CurrentMonth?.Name}, Day {CurrentDate.Day}";
          model.FantasyDate = JsonConvert.SerializeObject(new { grandDate, date = campaignDate.Value, monthName = CurrentMonth.Name, day = CurrentDate.Day, campaignId });
        }

        var displayMap = campaignData.FirstOrDefault(x => x.Key == PFConfig.STR_LIVEDISPLAYMAP);
        if (displayMap != null)
          model.MapPath = displayMap.Value;

        var mapData = campaignData.FirstOrDefault(x => x.Key == PFConfig.STR_LIVEDISPLAYMAPDATA);
        if (mapData != null)
          model.MapSaveData = mapData.Value;
      }
      return View("LiveDisplay", model);
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