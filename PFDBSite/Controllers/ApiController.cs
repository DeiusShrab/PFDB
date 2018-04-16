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
using Newtonsoft.Json.Linq;
using DBConnect.DBModels;

namespace PFDBSite.Controllers
{
  [Produces("application/json")]
  [Route("api")]
  [Authorize]
  public class ApiController : PFDBController
  {
    private RandomHelper helperRandom;
    private QueryHelper helperQuery;

    public ApiController()
    {
      helperRandom = new RandomHelper();
      helperQuery = new QueryHelper();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("GetToken/pls")]
    public IActionResult GetToken([FromBody] JObject info)
    {
      if (info.ContainsKey("username") && info.ContainsKey("password")
          && info["username"].Value<string>() == DBClient.API_USER && info["password"].Value<string>() == DBClient.API_PASS)
      {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DBClient.JWT_KEY));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(DBClient.JWT_ISSUER,
          DBClient.JWT_ISSUER,
          expires: DateTime.Now.AddHours(24),
          signingCredentials: creds);

        var content = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = content });
      }

      return NotFound();
    }

    #region Queries

    [HttpGet("List/{listType}")]
    public IActionResult ObjectList(string listType)
    {
      var context = PFDAL.GetContext();
      var type = listType.ToLower();

      if (type == "Bestiary")
        return new JsonResult(context.Bestiary.Select(x => new ListItemResult() { Id = x.BestiaryId, Name = x.Name, Notes = x.Cr.ToString() }));
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
      else if (type == "MonsterSpawn")
        return new JsonResult(context.Bestiary.Select(x => new ListItemResult() { Id = x.BestiaryId, Name = x.Name, Notes = GetTypesForBestiary(x.BestiaryId) }));
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

    [HttpPost("RandomEncounter")]
    public IActionResult RandomEncounter([FromBody] RandomEncounterRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperRandom.GenerateRandomEncounters(request));
    }

    [HttpPost("RandomWeather")]
    public IActionResult RandomWeatherTable([FromBody] RandomWeatherRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperRandom.GenerateRandomWeatherTable(request));
    }

    [HttpPost("FantasyDate")]
    public IActionResult UpdateFantasyDate([FromBody] string newDate)
    {
      if (string.IsNullOrWhiteSpace(newDate))
        return BadRequest();

      // Store the current date in the DB or something
      // Campaign table?
      return Ok();
    }

    [HttpPost("LookupSpell")]
    public IActionResult LookupSpells([FromBody] SpellLookupRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperQuery.SpellLookup(request));
    }

    [HttpGet("FantasyDate")]
    public IActionResult GetFantasyDate()
    {
      return Ok("11110101");
    }

    [HttpPost("Spawns/{bestiaryId:int}")]
    public IActionResult UpdateSpawns([FromBody] SpawnUpdateRequest request, int bestiaryId)
    {
      if (request == null || request.BestiaryId != bestiaryId)
        return BadRequest();

      helperQuery.UpdateSpawns(request);

      return Ok();
    }

    [HttpGet("Spawns/{bestiaryId:int}")]
    public IActionResult GetSpawns(int bestiaryId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.MonsterSpawn.Where(x => x.BestiaryId == bestiaryId));
    }

    #endregion

    #region All

    // Bestiary not implemented due to size of return, use List instead

    // BestiaryDetail not implemented due to size of return, use List instead

    [HttpGet("BestiaryEnvironment")]
    public IActionResult BestiaryEnvironment_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryEnvironment.ToList());
    }

    [HttpGet("BestiaryFeat")]
    public IActionResult BestiaryFeat_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryFeat.ToList());
    }

    [HttpGet("BestiaryLanguage")]
    public IActionResult BestiaryLanguage_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryLanguage.ToList());
    }

    [HttpGet("BestiarySkill")]
    public IActionResult BestiarySkill_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiarySkill.ToList());
    }

    [HttpGet("BestiarySubType")]
    public IActionResult BestiarySubType_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiarySubType.ToList());
    }

    [HttpGet("BestiaryType")]
    public IActionResult BestiaryType_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryType.ToList());
    }

    [HttpGet("Continent")]
    public IActionResult Continent_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Continent.ToList());
    }

    [HttpGet("ContinentWeather")]
    public IActionResult ContinentWeather_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentWeather.ToList());
    }

    [HttpGet("Environment")]
    public IActionResult Environment_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Environment.ToList());
    }

    [HttpGet("Faction")]
    public IActionResult Faction_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Faction.ToList());
    }

    // Feat not implemented due to size of return, use List instead

    [HttpGet("Language")]
    public IActionResult Language_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Language.ToList());
    }

    [HttpGet("Location")]
    public IActionResult Location_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Location.ToList());
    }

    [HttpGet("MagicItem")]
    public IActionResult MagicItem_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.MagicItem.ToList());
    }

    [HttpGet("MonsterSpawn")]
    public IActionResult MonsterSpawn_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.MonsterSpawn.ToList());
    }

    [HttpGet("Month")]
    public IActionResult Month_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Month.ToList());
    }

    [HttpGet("Plane")]
    public IActionResult Plane_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Plane.ToList());
    }

    [HttpGet("Season")]
    public IActionResult Season_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Season.ToList());
    }

    // Skill not implemented due to size of return, use List instead

    // Spell not implemented due to size of return, use List instead

    // SpellDetail not implemented due to size of return, use List instead

    [HttpGet("Terrain")]
    public IActionResult Terrain_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Terrain.ToList());
    }

    [HttpGet("Territory")]
    public IActionResult Territory_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Territory.ToList());
    }

    [HttpGet("Time")]
    public IActionResult Time_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Time.ToList());
    }

    [HttpGet("TrackedEvent")]
    public IActionResult TrackedEvent_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.TrackedEvent.ToList());
    }

    [HttpGet("Weather")]
    public IActionResult Weather_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weather.ToList());
    }


    #endregion

    #region Details

    [HttpGet("Bestiary/{BestiaryId:int}")]
    public IActionResult Bestiary_Detail(int BestiaryId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Bestiary.FirstOrDefault(x => x.BestiaryId == BestiaryId));
    }

    [HttpGet("BestiaryDetail/{BestiaryId:int}")]
    public IActionResult BestiaryDetail_Detail(int BestiaryId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryDetail.FirstOrDefault(x => x.BestiaryId == BestiaryId));
    }

    [HttpGet("BestiaryEnvironment/{BestiaryEnvironmentId:int}")]
    public IActionResult BestiaryEnvironment_Detail(int BestiaryEnvironmentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryEnvironment.FirstOrDefault(x => x.BestiaryEnvironmentId == BestiaryEnvironmentId));
    }

    [HttpGet("BestiaryFeat/{BestiaryFeatId:int}")]
    public IActionResult BestiaryFeat_Detail(int BestiaryFeatId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryFeat.FirstOrDefault(x => x.BestiaryFeatId == BestiaryFeatId));
    }

    [HttpGet("BestiaryLanguage/{BestiaryLanguageId:int}")]
    public IActionResult BestiaryLanguage_Detail(int BestiaryLanguageId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryLanguage.FirstOrDefault(x => x.BestiaryLanguageId == BestiaryLanguageId));
    }

    [HttpGet("BestiarySkill/{BestiarySkillId:int}")]
    public IActionResult BestiarySkill_Detail(int BestiarySkillId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiarySkill.FirstOrDefault(x => x.BestiarySkillId == BestiarySkillId));
    }

    [HttpGet("BestiarySubType/{BestiarySubTypeId:int}")]
    public IActionResult BestiarySubType_Detail(int BestiarySubTypeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiarySubType.FirstOrDefault(x => x.BestiarySubTypeId == BestiarySubTypeId));
    }

    [HttpGet("BestiaryType/{BestiaryTypeId:int}")]
    public IActionResult BestiaryType_Detail(int BestiaryTypeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryType.FirstOrDefault(x => x.BestiaryTypeId == BestiaryTypeId));
    }

    [HttpGet("Continent/{ContinentId:int}")]
    public IActionResult Continent_Detail(int ContinentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Continent.FirstOrDefault(x => x.ContinentId == ContinentId));
    }

    [HttpGet("ContinentWeather/{ContinentWeatherId:int}")]
    public IActionResult ContinentWeather_Detail(int ContinentWeatherId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentWeather.FirstOrDefault(x => x.Cwid == ContinentWeatherId));
    }

    [HttpGet("Environment/{EnvironmentId:int}")]
    public IActionResult Environment_Detail(int EnvironmentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Environment.FirstOrDefault(x => x.EnvironmentId == EnvironmentId));
    }

    [HttpGet("Faction/{FactionId:int}")]
    public IActionResult Faction_Detail(int FactionId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Faction.FirstOrDefault(x => x.FactionId == FactionId));
    }

    [HttpGet("Feat/{FeatId:int}")]
    public IActionResult Feat_Detail(int FeatId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Feat.FirstOrDefault(x => x.FeatId == FeatId));
    }

    [HttpGet("Language/{LanguageId:int}")]
    public IActionResult Language_Detail(int LanguageId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Language.FirstOrDefault(x => x.LanguageId == LanguageId));
    }

    [HttpGet("Location/{LocationId:int}")]
    public IActionResult Location_Detail(int LocationId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Location.FirstOrDefault(x => x.LocationId == LocationId));
    }

    [HttpGet("MagicItem/{MagicItemId:int}")]
    public IActionResult MagicItem_Detail(int MagicItemId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.MagicItem.FirstOrDefault(x => x.MagicItemId == MagicItemId));
    }

    [HttpGet("MonsterSpawn/{MonsterSpawnId:int}")]
    public IActionResult MonsterSpawn_Detail(int MonsterSpawnId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.MonsterSpawn.FirstOrDefault(x => x.SpawnId == MonsterSpawnId));
    }

    [HttpGet("Month/{MonthId:int}")]
    public IActionResult Month_Detail(int MonthId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Month.FirstOrDefault(x => x.MonthId == MonthId));
    }

    [HttpGet("Plane/{PlaneId:int}")]
    public IActionResult Plane_Detail(int PlaneId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Plane.FirstOrDefault(x => x.PlaneId == PlaneId));
    }

    [HttpGet("Season/{SeasonId:int}")]
    public IActionResult Season_Detail(int SeasonId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Season.FirstOrDefault(x => x.SeasonId == SeasonId));
    }

    [HttpGet("Skill/{SkillId:int}")]
    public IActionResult Skill_Detail(int SkillId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Skill.FirstOrDefault(x => x.SkillId == SkillId));
    }

    [HttpGet("Spell/{SpellId:int}")]
    public IActionResult Spell_Detail(int SpellId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Spell.FirstOrDefault(x => x.SpellId == SpellId));
    }

    [HttpGet("SpellDetail/{SpellId:int}")]
    public IActionResult SpellDetail_Detail(int SpellId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellDetail.FirstOrDefault(x => x.SpellId == SpellId));
    }

    [HttpGet("Terrain/{TerrainId:int}")]
    public IActionResult Terrain_Detail(int TerrainId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Terrain.FirstOrDefault(x => x.TerrainId == TerrainId));
    }

    [HttpGet("Territory/{TerritoryId:int}")]
    public IActionResult Territory_Detail(int TerritoryId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Territory.FirstOrDefault(x => x.TerritoryId == TerritoryId));
    }

    [HttpGet("Time/{TimeId:int}")]
    public IActionResult Time_Detail(int TimeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Time.FirstOrDefault(x => x.TimeId == TimeId));
    }

    [HttpGet("TrackedEvent/{TrackedEventId:int}")]
    public IActionResult TrackedEvent_Detail(int TrackedEventId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.TrackedEvent.FirstOrDefault(x => x.TrackedEventId == TrackedEventId));
    }

    [HttpGet("Weather/{WeatherId:int}")]
    public IActionResult Weather_Detail(int WeatherId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weather.FirstOrDefault(x => x.WeatherId == WeatherId));
    }

    #endregion

    #region Create

    [HttpPost("Bestiary")]
    public IActionResult Bestiary_Create([FromBody] Bestiary obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiaryId != 0)
        return BadRequest();

      try
      {
        context.Bestiary.Add(obj);
        context.SaveChanges();
        context.BestiaryDetail.Add(new BestiaryDetail() { BestiaryId = obj.BestiaryId });
        context.SaveChanges();
        return Created("Bestiary/" + obj.BestiaryId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // BestiaryDetail created as part of Bestiary

    [HttpPost("BestiaryEnvironment")]
    public IActionResult BestiaryEnvironment_Create([FromBody] BestiaryEnvironment obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiaryEnvironmentId != 0)
        return BadRequest();

      try
      {
        context.BestiaryEnvironment.Add(obj);
        context.SaveChanges();
        return Created("BestiaryEnvironment/" + obj.BestiaryEnvironmentId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("BestiaryFeat")]
    public IActionResult BestiaryFeat_Create([FromBody] BestiaryFeat obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiaryFeatId != 0)
        return BadRequest();

      try
      {
        context.BestiaryFeat.Add(obj);
        context.SaveChanges();
        return Created("BestiaryFeat/" + obj.BestiaryFeatId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("BestiaryLanguage")]
    public IActionResult BestiaryLanguage_Create([FromBody] BestiaryLanguage obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiaryLanguageId != 0)
        return BadRequest();

      try
      {
        context.BestiaryLanguage.Add(obj);
        context.SaveChanges();
        return Created("BestiaryLanguage/" + obj.BestiaryLanguageId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("BestiarySkill")]
    public IActionResult BestiarySkill_Create([FromBody] BestiarySkill obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiarySkillId != 0)
        return BadRequest();

      try
      {
        context.BestiarySkill.Add(obj);
        context.SaveChanges();
        return Created("BestiarySkill/" + obj.BestiarySkillId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("BestiarySubType")]
    public IActionResult BestiarySubType_Create([FromBody] BestiarySubType obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiarySubTypeId != 0)
        return BadRequest();

      try
      {
        context.BestiarySubType.Add(obj);
        context.SaveChanges();
        return Created("BestiarySubType/" + obj.BestiarySubTypeId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("BestiaryType")]
    public IActionResult BestiaryType_Create([FromBody] BestiaryType obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.BestiaryTypeId != 0)
        return BadRequest();

      try
      {
        context.BestiaryType.Add(obj);
        context.SaveChanges();
        return Created("BestiaryType/" + obj.BestiaryTypeId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Continent")]
    public IActionResult Continent_Create([FromBody] Continent obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.ContinentId != 0)
        return BadRequest();

      try
      {
        context.Continent.Add(obj);
        context.SaveChanges();
        return Created("Continent/" + obj.ContinentId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("ContinentWeather")]
    public IActionResult ContinentWeather_Create([FromBody] ContinentWeather obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.Cwid != 0)
        return BadRequest();

      try
      {
        context.ContinentWeather.Add(obj);
        context.SaveChanges();
        return Created("ContinentWeather/" + obj.Cwid.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Environment")]
    public IActionResult Environment_Create([FromBody] DBConnect.DBModels.Environment obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.EnvironmentId != 0)
        return BadRequest();

      try
      {
        context.Environment.Add(obj);
        context.SaveChanges();
        return Created("Environment/" + obj.EnvironmentId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Faction")]
    public IActionResult Faction_Create([FromBody] Faction obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.FactionId != 0)
        return BadRequest();

      try
      {
        context.Faction.Add(obj);
        context.SaveChanges();
        return Created("Faction/" + obj.FactionId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Feat")]
    public IActionResult Feat_Create([FromBody] Feat obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.FeatId != 0)
        return BadRequest();

      try
      {
        context.Feat.Add(obj);
        context.SaveChanges();
        return Created("Feat/" + obj.FeatId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Language")]
    public IActionResult Language_Create([FromBody] Language obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.LanguageId != 0)
        return BadRequest();

      try
      {
        context.Language.Add(obj);
        context.SaveChanges();
        return Created("Language/" + obj.LanguageId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Location")]
    public IActionResult Location_Create([FromBody] Location obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.LocationId != 0)
        return BadRequest();

      try
      {
        context.Location.Add(obj);
        context.SaveChanges();
        return Created("Location/" + obj.LocationId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("MagicItem")]
    public IActionResult MagicItem_Create([FromBody] MagicItem obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.MagicItemId != 0)
        return BadRequest();

      try
      {
        context.MagicItem.Add(obj);
        context.SaveChanges();
        return Created("MagicItem/" + obj.MagicItemId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("MonsterSpawn")]
    public IActionResult MonsterSpawn_Create([FromBody] MonsterSpawn obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.SpawnId != 0)
        return BadRequest();

      try
      {
        context.MonsterSpawn.Add(obj);
        context.SaveChanges();
        return Created("MonsterSpawn/" + obj.SpawnId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Month")]
    public IActionResult Month_Create([FromBody] Month obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.MonthId != 0)
        return BadRequest();

      try
      {
        context.Month.Add(obj);
        context.SaveChanges();
        return Created("Month/" + obj.MonthId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Plane")]
    public IActionResult Plane_Create([FromBody] Plane obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.PlaneId != 0)
        return BadRequest();

      try
      {
        context.Plane.Add(obj);
        context.SaveChanges();
        return Created("Plane/" + obj.PlaneId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Season")]
    public IActionResult Season_Create([FromBody] Season obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.SeasonId != 0)
        return BadRequest();

      try
      {
        context.Season.Add(obj);
        context.SaveChanges();
        return Created("Season/" + obj.SeasonId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Skill")]
    public IActionResult Skill_Create([FromBody] Skill obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.SkillId != 0)
        return BadRequest();

      try
      {
        context.Skill.Add(obj);
        context.SaveChanges();
        return Created("Skill/" + obj.SkillId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Spell")]
    public IActionResult Spell_Create([FromBody] Spell obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.SpellId != 0)
        return BadRequest();

      try
      {
        context.Spell.Add(obj);
        context.SaveChanges();
        context.SpellDetail.Add(new SpellDetail() { SpellId = obj.SpellId });
        context.SaveChanges();
        return Created("Spell/" + obj.SpellId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    // SpellDetail not implemented, part of Spell

    [HttpPost("Terrain")]
    public IActionResult Terrain_Create([FromBody] Terrain obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.TerrainId != 0)
        return BadRequest();

      try
      {
        context.Terrain.Add(obj);
        context.SaveChanges();
        return Created("Terrain/" + obj.TerrainId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Territory")]
    public IActionResult Territory_Create([FromBody] Territory obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.TerritoryId != 0)
        return BadRequest();

      try
      {
        context.Territory.Add(obj);
        context.SaveChanges();
        return Created("Territory/" + obj.TerritoryId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Time")]
    public IActionResult Time_Create([FromBody] Time obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.TimeId != 0)
        return BadRequest();

      try
      {
        context.Time.Add(obj);
        context.SaveChanges();
        return Created("Time/" + obj.TimeId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("TrackedEvent")]
    public IActionResult TrackedEvent_Create([FromBody] TrackedEvent obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.TrackedEventId != 0)
        return BadRequest();

      try
      {
        context.TrackedEvent.Add(obj);
        context.SaveChanges();
        return Created("TrackedEvent/" + obj.TrackedEventId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPost("Weather")]
    public IActionResult Weather_Create([FromBody] Weather obj)
    {
      var context = PFDAL.GetContext();
      if (obj == null || obj.WeatherId != 0)
        return BadRequest();

      try
      {
        context.Weather.Add(obj);
        context.SaveChanges();
        return Created("Weather/" + obj.WeatherId.ToString(), obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }


    #endregion

    #region Update

    [HttpPut("Bestiary/{BestiaryId:int}")]
    public IActionResult Bestiary_Update([FromBody] Bestiary obj, int BestiaryId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryId == 0 || obj.BestiaryId != BestiaryId)
        return BadRequest();

      try
      {
        context.Bestiary.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiaryDetail/{BestiaryDetailId:int}")]
    public IActionResult BestiaryDetail_Update([FromBody] BestiaryDetail obj, int BestiaryDetailId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryId == 0 || obj.BestiaryId != BestiaryDetailId)
        return BadRequest();

      try
      {
        context.BestiaryDetail.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiaryEnvironment/{BestiaryEnvironmentId:int}")]
    public IActionResult BestiaryEnvironment_Update([FromBody] BestiaryEnvironment obj, int BestiaryEnvironmentId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryEnvironmentId == 0 || obj.BestiaryEnvironmentId != BestiaryEnvironmentId)
        return BadRequest();

      try
      {
        context.BestiaryEnvironment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiaryFeat/{BestiaryFeatId:int}")]
    public IActionResult BestiaryFeat_Update([FromBody] BestiaryFeat obj, int BestiaryFeatId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryFeatId == 0 || obj.BestiaryFeatId != BestiaryFeatId)
        return BadRequest();

      try
      {
        context.BestiaryFeat.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiaryLanguage/{BestiaryLanguageId:int}")]
    public IActionResult BestiaryLanguage_Update([FromBody] BestiaryLanguage obj, int BestiaryLanguageId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryLanguageId == 0 || obj.BestiaryLanguageId != BestiaryLanguageId)
        return BadRequest();

      try
      {
        context.BestiaryLanguage.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiarySkill/{BestiarySkillId:int}")]
    public IActionResult BestiarySkill_Update([FromBody] BestiarySkill obj, int BestiarySkillId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiarySkillId == 0 || obj.BestiarySkillId != BestiarySkillId)
        return BadRequest();

      try
      {
        context.BestiarySkill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiarySubType/{BestiarySubTypeId:int}")]
    public IActionResult BestiarySubType_Update([FromBody] BestiarySubType obj, int BestiarySubTypeId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiarySubTypeId == 0 || obj.BestiarySubTypeId != BestiarySubTypeId)
        return BadRequest();

      try
      {
        context.BestiarySubType.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("BestiaryType/{BestiaryTypeId:int}")]
    public IActionResult BestiaryType_Update([FromBody] BestiaryType obj, int BestiaryTypeId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.BestiaryTypeId == 0 || obj.BestiaryTypeId != BestiaryTypeId)
        return BadRequest();

      try
      {
        context.BestiaryType.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Continent/{ContinentId:int}")]
    public IActionResult Continent_Update([FromBody] Continent obj, int ContinentId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.ContinentId == 0 || obj.ContinentId != ContinentId)
        return BadRequest();

      try
      {
        context.Continent.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("ContinentWeather/{ContinentWeatherId:int}")]
    public IActionResult ContinentWeather_Update([FromBody] ContinentWeather obj, int ContinentWeatherId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.Cwid == 0 || obj.Cwid != ContinentWeatherId)
        return BadRequest();

      try
      {
        context.ContinentWeather.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Environment/{EnvironmentId:int}")]
    public IActionResult Environment_Update([FromBody] DBConnect.DBModels.Environment obj, int EnvironmentId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.EnvironmentId == 0 || obj.EnvironmentId != EnvironmentId)
        return BadRequest();

      try
      {
        context.Environment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Faction/{FactionId:int}")]
    public IActionResult Faction_Update([FromBody] Faction obj, int FactionId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.FactionId == 0 || obj.FactionId != FactionId)
        return BadRequest();

      try
      {
        context.Faction.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Feat/{FeatId:int}")]
    public IActionResult Feat_Update([FromBody] Feat obj, int FeatId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.FeatId == 0 || obj.FeatId != FeatId)
        return BadRequest();

      try
      {
        context.Feat.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Language/{LanguageId:int}")]
    public IActionResult Language_Update([FromBody] Language obj, int LanguageId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.LanguageId == 0 || obj.LanguageId != LanguageId)
        return BadRequest();

      try
      {
        context.Language.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Location/{LocationId:int}")]
    public IActionResult Location_Update([FromBody] Location obj, int LocationId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.LocationId == 0 || obj.LocationId != LocationId)
        return BadRequest();

      try
      {
        context.Location.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("MagicItem/{MagicItemId:int}")]
    public IActionResult MagicItem_Update([FromBody] MagicItem obj, int MagicItemId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.MagicItemId == 0 || obj.MagicItemId != MagicItemId)
        return BadRequest();

      try
      {
        context.MagicItem.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("MonsterSpawn/{MonsterSpawnId:int}")]
    public IActionResult MonsterSpawn_Update([FromBody] MonsterSpawn obj, int MonsterSpawnId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.SpawnId == 0 || obj.SpawnId != MonsterSpawnId)
        return BadRequest();

      try
      {
        context.MonsterSpawn.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Month/{MonthId:int}")]
    public IActionResult Month_Update([FromBody] Month obj, int MonthId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.MonthId == 0 || obj.MonthId != MonthId)
        return BadRequest();

      try
      {
        context.Month.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Plane/{PlaneId:int}")]
    public IActionResult Plane_Update([FromBody] Plane obj, int PlaneId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.PlaneId == 0 || obj.PlaneId != PlaneId)
        return BadRequest();

      try
      {
        context.Plane.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Season/{SeasonId:int}")]
    public IActionResult Season_Update([FromBody] Season obj, int SeasonId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.SeasonId == 0 || obj.SeasonId != SeasonId)
        return BadRequest();

      try
      {
        context.Season.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Skill/{SkillId:int}")]
    public IActionResult Skill_Update([FromBody] Skill obj, int SkillId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.SkillId == 0 || obj.SkillId != SkillId)
        return BadRequest();

      try
      {
        context.Skill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Spell/{SpellId:int}")]
    public IActionResult Spell_Update([FromBody] Spell obj, int SpellId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.SpellId == 0 || obj.SpellId != SpellId)
        return BadRequest();

      try
      {
        context.Spell.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("SpellDetail/{SpellDetailId:int}")]
    public IActionResult SpellDetail_Update([FromBody] SpellDetail obj, int SpellDetailId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.SpellId == 0 || obj.SpellId != SpellDetailId)
        return BadRequest();

      try
      {
        context.SpellDetail.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Terrain/{TerrainId:int}")]
    public IActionResult Terrain_Update([FromBody] Terrain obj, int TerrainId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.TerrainId == 0 || obj.TerrainId != TerrainId)
        return BadRequest();

      try
      {
        context.Terrain.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Territory/{TerritoryId:int}")]
    public IActionResult Territory_Update([FromBody] Territory obj, int TerritoryId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.TerritoryId == 0 || obj.TerritoryId != TerritoryId)
        return BadRequest();

      try
      {
        context.Territory.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Time/{TimeId:int}")]
    public IActionResult Time_Update([FromBody] Time obj, int TimeId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.TimeId == 0 || obj.TimeId != TimeId)
        return BadRequest();

      try
      {
        context.Time.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("TrackedEvent/{TrackedEventId:int}")]
    public IActionResult TrackedEvent_Update([FromBody] TrackedEvent obj, int TrackedEventId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.TrackedEventId == 0 || obj.TrackedEventId != TrackedEventId)
        return BadRequest();

      try
      {
        context.TrackedEvent.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    [HttpPut("Weather/{WeatherId:int}")]
    public IActionResult Weather_Update([FromBody] Weather obj, int WeatherId)
    {
      var context = PFDAL.GetContext();

      if (obj == null || obj.WeatherId == 0 || obj.WeatherId != WeatherId)
        return BadRequest();

      try
      {
        context.Weather.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }


    #endregion

    #region Delete

    // Ehh... let's hold off on this one...

    #endregion

    #region Private Methods

    private string GetTypesForBestiary(int bestiaryId)
    {
      var ret = string.Empty;

      var context = PFDAL.GetContext();
      var b = context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId);
      if (b != null)
      {
        var t = context.BestiaryType.FirstOrDefault(x => x.BestiaryTypeId == b.Type);
        if (t != null)
          ret += t.Name + ", ";

        ret += b.SubType;
      }

      return ret;
    }

    #endregion
  }
}