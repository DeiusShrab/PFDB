using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DBConnect;
using DBConnect.ConnectModels;
using PFDBSite.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace PFDBSite.Controllers
{
  [Produces("application/json")]
  [Route("api")]
  [Authorize]
  public class ApiController : PFDBController
  {
    public IPFDBContext context = DBConnect.PFDAL.GetContext(Configuration["WorkingEnvironment"]);
    private RandomHelper helperRandom;

    public ApiController()
    {
      helperRandom = new RandomHelper(context);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("GetToken/pls")]
    public IActionResult GetToken()
    {
      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DBClient.JWT_KEY));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(DBClient.JWT_ISSUER,
        DBClient.JWT_ISSUER,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);

      var content = new JwtSecurityTokenHandler().WriteToken(token);

      return Ok(new { token = content });
    }

    [HttpGet("Continent")]
    public IActionResult ContinentAll()
    {
      return new JsonResult(context.Continent.ToList());
    }

    [HttpGet("Season")]
    public IActionResult SeasonAll()
    {
      return new JsonResult(context.Season.ToList());
    }

    [HttpPost("RandomEncounter")]
    public IActionResult RandomEncounter([FromBody] RandomEncounterRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperRandom.GenerateRandomEncounters(request));
    }

    public IActionResult RandomWeatherTable([FromBody] RandomWeatherRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperRandom.GenerateRandomWeatherTable(request));
    }

    [HttpGet("bestiary/{bestiaryId:int}")]
    public IActionResult Bestiary(int bestiaryId)
    {
      return new JsonResult(context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId));
    }

    [HttpGet("bestiarydetail/{bestiaryId:int}")]
    public IActionResult BestiaryDetail(int bestiaryId)
    {
      return new JsonResult(context.BestiaryDetail.FirstOrDefault(x => x.BestiaryId == bestiaryId));
    }

    [HttpGet("List/{listType:string")]
    public IActionResult ObjectList(string listType)
    {
      var type = listType.ToLower();

      if (type == "Bestiary")
        return new JsonResult(context.Bestiary.Select(x => new ListItemResult() { Id = x.BestiaryId, Name = x.Name }));
      else if (type == "BestiaryType")
        return new JsonResult(context.BestiaryType.Select(x => new ListItemResult() { Id = x.BestiaryTypeId, Name = x.Name }));
      else if (type == "Continent")
        return new JsonResult(context.Continent.Select(x => new ListItemResult() { Id = x.ContinentId, Name = x.Name }));
      else if (type == "Environment")
        return new JsonResult(context.Environment.Select(x => new ListItemResult() { Id = x.EnvironmentId, Name = x.Name }));
      else if (type == "Faction")
        return new JsonResult(context.Faction.Select(x => new ListItemResult() { Id = x.FactionId, Name = x.Name }));
      else if (type == "Feat")
        return new JsonResult(context.Feat.Select(x => new ListItemResult() { Id = x.FeatId, Name = x.Name }));
      else if (type == "Language")
        return new JsonResult(context.Language.Select(x => new ListItemResult() { Id = x.LanguageId, Name = x.Name }));
      else if (type == "Location")
        return new JsonResult(context.Location.Select(x => new ListItemResult() { Id = x.LocationId, Name = x.Name }));
      else if (type == "MagicItem")
        return new JsonResult(context.MagicItem.Select(x => new ListItemResult() { Id = x.MagicItemId, Name = x.Name }));
      else if (type == "Month")
        return new JsonResult(context.Month.Select(x => new ListItemResult() { Id = x.MonthId, Name = x.Name }));
      else if (type == "Season")
        return new JsonResult(context.Season.Select(x => new ListItemResult() { Id = x.SeasonId, Name = x.Name }));
      else if (type == "Spell")
        return new JsonResult(context.Spell.Select(x => new ListItemResult() { Id = x.SpellId, Name = x.Name }));
      else if (type == "Territory")
        return new JsonResult(context.Territory.Select(x => new ListItemResult() { Id = x.TerritoryId, Name = x.Name }));
      else if (type == "Weather")
        return new JsonResult(context.Weather.Select(x => new ListItemResult() { Id = x.WeatherId, Name = x.Name }));
      else if (type == "Terrain")
        return new JsonResult(context.Terrain.Select(x => new ListItemResult() { Id = x.TerrainId, Name = x.Name }));
      else if (type == "Time")
        return new JsonResult(context.Time.Select(x => new ListItemResult() { Id = x.TimeId, Name = x.Name }));
      else if (type == "TrackedEvent")
        return new JsonResult(context.TrackedEvent.Select(x => new ListItemResult() { Id = x.TrackedEventId, Name = x.Name }));
      else if (type == "Plane")
        return new JsonResult(context.Plane.Select(x => new ListItemResult() { Id = x.PlaneId, Name = x.Name }));
      else if (type == "Skill")
        return new JsonResult(context.Skill.Select(x => new ListItemResult() { Id = x.SkillId, Name = x.Name }));
      else
        return new NotFoundResult();
    }
  }
}