using DBConnect;
using PFDBCommon.ConnectModels;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace PFDBSite.ApiController
{
  [Produces("application/json")]
  [Route("api/v1/LiveDisplay")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
#if !DEBUG
  [RequireHttps]
#endif
  [ApiController]
  public class LiveDisplayApiController : ControllerBase
  {
    private readonly IHubContext<Services.LiveDisplayHub> _hubContext;

    public LiveDisplayApiController(IHubContext<Services.LiveDisplayHub> hubContext)
    {
      _hubContext = hubContext;
    }

    [AllowAnonymous]
    [Route("test")]
    public IActionResult Test()
    {
      var data = PFDAL.GetContext().CampaignData;
      Console.WriteLine($"LiveDisplayAPI - {data.Count()} entries in CampaignData");
      return Ok($"API is listening! {data.Count()}");
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("GetToken/pls")]
    public IActionResult GetToken([FromBody] JObject info)
    {
      if (info.ContainsKey("username") && info.ContainsKey("password")
          && info["username"].Value<string>() == PFConfig.API_USER && info["password"].Value<string>() == PFConfig.API_PASS)
      {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(PFConfig.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(PFConfig.JWT_ISSUER,
          PFConfig.JWT_ISSUER,
          expires: DateTime.Now.AddHours(24),
          signingCredentials: creds);

        var content = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = content });
      }

      return NotFound();
    }

    [HttpGet]
    [Route("MessageHistory/{chatRoomId:int}/{playerId:int}")]
    public IActionResult GetMessageHistory(int chatRoomId, int playerId)
    {
      // Load previous messages from DB
      // Compile them into list, json it
      // Return it

      return NotFound();
    }

    [HttpGet]
    [Route("UpdateDate/{campaignId:int}")]
    public IActionResult UpdateDate(int campaignId)
    {
      var context = PFDAL.GetContext();
      var data = context.CampaignData.Where(x => x.CampaignId == campaignId);
      var date = data.FirstOrDefault(x => x.Key.Equals(PFConfig.STR_FANTASYDATE))?.Value;

      if (!string.IsNullOrWhiteSpace(date))
      {
        var seasons = context.Season.ToList();
        var months = context.Month.ToList();
        var CurrentDate = new FantasyDate(date);
        var CurrentMonth = months.FirstOrDefault(x => x.MonthOrder == CurrentDate.Month);
        var grandDate = $"YEAR {CurrentDate.Year} AA, Season of {seasons.FirstOrDefault(x => x.SeasonId == CurrentMonth.SeasonId)?.Name}, Month of {CurrentMonth?.Name}, Day {CurrentDate.Day}";
        _hubContext.Clients.All.SendAsync("ReceiveDate", new { grandDate, date, monthName = CurrentMonth.Name, day = CurrentDate.Day, campaignId });
        // TODO - only send update to clients connected to the campaignId

        return Ok();
      }

      return BadRequest("No date found for campaign " + campaignId);
    }

    public IActionResult UpdateCombat(int campaignId, string combatJson)
    {
      // Combat update from SERVER to CLIENTS
      // HP changes, add/remove combatants, the whole works

      return NotFound();
    }
  }
}