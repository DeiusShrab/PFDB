using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using DBConnect;
using DBConnect.ConnectModels;
using PFAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Linq;
using DBConnect.DBModels;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PFAPI.Controllers
{
  [Produces("application/json")]
  [Route("api/v1")]
  [Authorize]
#if !DEBUG
  [RequireHttps]
#endif
  public class ApiController : Controller
  {
    private RandomHelper helperRandom;
    private QueryHelper helperQuery;
    public static IConfiguration Configuration { get; set; }

    public ApiController()
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json");

      Configuration = builder.Build();
      helperRandom = new RandomHelper();
      helperQuery = new QueryHelper();
    }

    [AllowAnonymous]
    [Route("test")]
    public IActionResult Test()
    {
      var data = PFDAL.GetContext().CampaignData;
      Console.WriteLine($"TESTAPI - {data.Count()} entries in CampaignData");
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

#region Queries

    [HttpGet("CampaignData/{campaignId:int}")]
    public IActionResult GetCampaignData(int campaignId)
    {
      var data = PFDAL.GetContext().CampaignData.Where(x => x.CampaignId == campaignId);

      if (data == null)
        return NotFound();

      var dict = new Dictionary<string, string>();
      foreach (var item in data)
      {
        dict.Add(item.Key, item.Value);
      }

      return new JsonResult(dict);
    }

    [HttpPost("CampaignData/{campaignId:int}")]
    public IActionResult UpdateCampaignData([FromBody] Dictionary<string, string> campaignData, int campaignId)
    {
      if (campaignData == null)
      {
        return BadRequest();
      }

      var context = PFDAL.GetContext();

      var data = from cd in context.CampaignData
                 where cd.CampaignId == campaignId
                 select cd;

      foreach (var key in campaignData.Keys)
      {
        var item = data.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
        if (item != null)
        {
          item.Value = campaignData[key];
        }
        else
        {
          context.CampaignData.Add(new CampaignData { CampaignId = campaignId, Key = key, Value = campaignData[key] });
        }
      }

      context.SaveChanges();

      if (campaignData.Keys.Contains(PFConfig.STR_FANTASYDATE))
      {
        LiveDisplayClient.UpdateDate(campaignId);
      }

      return Ok();
    }

    [HttpGet("List/{listType}")]
    public IActionResult ObjectList(string listType)
    {
      var context = PFDAL.GetContext();
      var type = listType.ToLower();

      if (type == "bestiary")
        return new JsonResult(context.Bestiary.Select(x => new ListItemResult() { Id = x.BestiaryId, Name = x.Name, Notes = x.Cr.ToString() }));
      else if (type == "bestiarytype")
        return new JsonResult(context.BestiaryType.Select(x => new ListItemResult() { Id = x.BestiaryTypeId, Name = x.Name }));
      else if (type == "continent")
        return new JsonResult(context.Continent.Select(x => new ListItemResult() { Id = x.ContinentId, Name = x.Name }));
      else if (type == "environment")
        return new JsonResult(context.Environment.Select(x => new ListItemResult() { Id = x.EnvironmentId, Name = x.Name }));
      else if (type == "faction")
        return new JsonResult(context.Faction.Select(x => new ListItemResult() { Id = x.FactionId, Name = x.Name }));
      else if (type == "feat")
        return new JsonResult(context.Feat.Select(x => new ListItemResult() { Id = x.FeatId, Name = x.Name }));
      else if (type == "language")
        return new JsonResult(context.Language.Select(x => new ListItemResult() { Id = x.LanguageId, Name = x.Name }));
      else if (type == "location")
        return new JsonResult(context.Location.Select(x => new ListItemResult() { Id = x.LocationId, Name = x.Name }));
      else if (type == "magicitem")
        return new JsonResult(context.MagicItem.Select(x => new ListItemResult() { Id = x.MagicItemId, Name = x.Name }));
      else if (type == "monsterspawn")
        return new JsonResult(context.Bestiary.Select(x => new ListItemResult() { Id = x.BestiaryId, Name = x.Name, Notes = GetTypesForBestiary(x.BestiaryId) }));
      else if (type == "month")
        return new JsonResult(context.Month.Select(x => new ListItemResult() { Id = x.MonthId, Name = x.Name }));
      else if (type == "season")
        return new JsonResult(context.Season.Select(x => new ListItemResult() { Id = x.SeasonId, Name = x.Name }));
      else if (type == "spell")
        return new JsonResult(context.Spell.Select(x => new ListItemResult() { Id = x.SpellId, Name = x.Name }));
      else if (type == "territory")
        return new JsonResult(context.Territory.Select(x => new ListItemResult() { Id = x.TerritoryId, Name = x.Name }));
      else if (type == "weather")
        return new JsonResult(context.Weather.Select(x => new ListItemResult() { Id = x.WeatherId, Name = x.Name }));
      else if (type == "terrain")
        return new JsonResult(context.Terrain.Select(x => new ListItemResult() { Id = x.TerrainId, Name = x.Name }));
      else if (type == "time")
        return new JsonResult(context.Time.Select(x => new ListItemResult() { Id = x.TimeId, Name = x.Name }));
      else if (type == "trackedEvent")
        return new JsonResult(context.TrackedEvent.Select(x => new ListItemResult() { Id = x.TrackedEventId, Name = x.Name }));
      else if (type == "plane")
        return new JsonResult(context.Plane.Select(x => new ListItemResult() { Id = x.PlaneId, Name = x.Name }));
      else if (type == "skill")
        return new JsonResult(context.Skill.Select(x => new ListItemResult() { Id = x.SkillId, Name = x.Name }));
      else if (type == "spellschool")
        return new JsonResult(context.SpellSchool.Select(x => new ListItemResult() { Id = x.SpellSchoolId, Name = x.Name }));
      else if (type == "spellsubschool")
        return new JsonResult(context.SpellSubSchool.Select(x => new ListItemResult() { Id = x.SpellSubSchoolId, Name = x.Name, Notes = x.SpellSchoolId.ToString() }));
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

    [HttpGet("FantasyDate/{campaignId:int}")]
    public IActionResult GetFantasyDate(int campaignId)
    {
      var data = PFDAL.GetContext().CampaignData.FirstOrDefault(x => x.CampaignId == campaignId && x.Key == PFConfig.STR_FANTASYDATE);

      if (data == null)
        return NotFound();

      return Ok(data.Value);
    }

    [HttpPost("FantasyDate/{campaignId:int}")]
    public IActionResult UpdateFantasyDate([FromBody] string newDate, int campaignId)
    {
      if (string.IsNullOrWhiteSpace(newDate))
        return BadRequest();

      var context = PFDAL.GetContext();
      var data = context.CampaignData.FirstOrDefault(x => x.CampaignId == campaignId && x.Key == PFConfig.STR_FANTASYDATE);
      if (data == null)
        data = new CampaignData { CampaignId = campaignId, Key = PFConfig.STR_FANTASYDATE };

      context.CampaignData.Attach(data);
      data.Value = newDate;
      context.SaveChanges();

      try
      {
        // Send a request to the PFDBSite LiveDisplayApiController
      }
      catch(Exception ex)
      {
        Console.WriteLine("This doesn't work yet");
      }

      return Ok();
    }

    [HttpPost("LookupSpell")]
    public IActionResult LookupSpells([FromBody] SpellLookupRequest request)
    {
      if (request == null)
        return BadRequest();

      return new JsonResult(helperQuery.SpellLookup(request));
    }

    [HttpPost("Spawns/{bestiaryId:int}")]
    public IActionResult UpdateSpawns([FromBody] SpawnUpdateRequest request, int bestiaryId)
    {
      if (request == null || request.BestiaryId != bestiaryId || request.BestiaryId <= 0)
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

    [HttpGet("ContinentWeathers")]
    public IActionResult GetContinentWeathers(int continentId, int seasonId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentWeather.Where(x => x.ContinentId == continentId && x.SeasonId == seasonId));
    }

    [HttpPost("ContinentWeathers")]
    public IActionResult UpdateContinentWeathers([FromBody] ContinentWeatherUpdateRequest request)
    {
      if (request == null || request.ContinentId <= 0 || request.SeasonId <= 0)
        return BadRequest();

      helperQuery.UpdateContinentWeathers(request);

      return Ok();
    }

    [HttpGet("EnvironmentsForContinent")]
    public IActionResult GetEnvironmentsForContinent(int continentId)
    {
      if (continentId <= 0)
        return BadRequest();

      return new JsonResult(helperQuery.EnvironmentsForContinent(continentId));
    }

    [HttpGet("MonsterSpawnEdit")]
    public IActionResult GetMonsterSpawnEdit()
    {
      return new JsonResult(helperQuery.GetMonsterSpawnEdit());
    }

    [HttpPost("MonsterSpawnEdit")]
    public IActionResult UpdateMonsterSpawn([FromBody] UpdateMonsterSpawnRequest request)
    {
      if (request == null || request.BestiaryId <= 0 || request.SeasonId <= 0 || request.ContinentId <= 0)
        return BadRequest();

      if (request.Spawns.Any(x => x.ContinentId != request.ContinentId) || request.Spawns.Any(x => x.BestiaryId != request.BestiaryId) || request.Spawns.Any(x => x.SeasonId != request.SeasonId))
        return BadRequest();

      helperQuery.UpdateMonsterSpawn(request);

      return Ok();
    }

#endregion

#region All

    [HttpGet("Armor")]
    public IActionResult Armor_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Armor.ToList());
    }

    [HttpGet("Bestiary")]
    public IActionResult Bestiary_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Bestiary.ToList());
    }

    [HttpGet("BestiaryDetail")]
    public IActionResult BestiaryDetail_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryDetail.ToList());
    }

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

    [HttpGet("BestiaryMagic")]
    public IActionResult BestiaryMagic_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryMagic.ToList());
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

    [HttpGet("Campaign")]
    public IActionResult Campaign_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Campaign.ToList());
    }

    [HttpGet("CampaignData")]
    public IActionResult CampaignData_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CampaignData.ToList());
    }

    [HttpGet("Character")]
    public IActionResult Character_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Character.ToList());
    }

    [HttpGet("CharacterClassAbility")]
    public IActionResult CharacterClassAbility_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterClassAbility.ToList());
    }

    [HttpGet("CharacterClassLevel")]
    public IActionResult CharacterClassLevel_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterClassLevel.ToList());
    }

    [HttpGet("CharacterFeat")]
    public IActionResult CharacterFeat_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterFeat.ToList());
    }

    [HttpGet("CharacterGear")]
    public IActionResult CharacterGear_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterGear.ToList());
    }

    [HttpGet("CharacterGearEnchantment")]
    public IActionResult CharacterGearEnchantment_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterGearEnchantment.ToList());
    }

    [HttpGet("CharacterLanguage")]
    public IActionResult CharacterLanguage_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterLanguage.ToList());
    }

    [HttpGet("CharacterMagic")]
    public IActionResult CharacterMagic_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterMagic.ToList());
    }

    [HttpGet("CharacterRaceAbility")]
    public IActionResult CharacterRaceAbility_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterRaceAbility.ToList());
    }

    [HttpGet("CharacterSkill")]
    public IActionResult CharacterSkill_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterSkill.ToList());
    }

    [HttpGet("Class")]
    public IActionResult Class_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Class.ToList());
    }

    [HttpGet("ClassAbility")]
    public IActionResult ClassAbility_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ClassAbility.ToList());
    }

    [HttpGet("ClassSkill")]
    public IActionResult ClassSkill_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ClassSkill.ToList());
    }

    [HttpGet("Continent")]
    public IActionResult Continent_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Continent.ToList());
    }

    [HttpGet("ContinentEnvironment")]
    public IActionResult ContinentEnvironment_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentEnvironment.ToList());
    }

    [HttpGet("ContinentWeather")]
    public IActionResult ContinentWeather_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentWeather.ToList());
    }

    [HttpGet("Enchantment")]
    public IActionResult Enchantment_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Enchantment.ToList());
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

    [HttpGet("FavoredClass")]
    public IActionResult FavoredClass_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.FavoredClass.ToList());
    }

    [HttpGet("Feat")]
    public IActionResult Feat_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Feat.ToList());
    }

    [HttpGet("Gear")]
    public IActionResult Gear_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Gear.ToList());
    }

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

    [HttpGet("Npc")]
    public IActionResult Npc_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Npc.ToList());
    }

    [HttpGet("Npcdetail")]
    public IActionResult Npcdetail_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Npcdetail.ToList());
    }

    [HttpGet("OverridePrerequisite")]
    public IActionResult OverridePrerequisite_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.OverridePrerequisite.ToList());
    }

    [HttpGet("Plane")]
    public IActionResult Plane_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Plane.ToList());
    }

    [HttpGet("Player")]
    public IActionResult Player_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Player.ToList());
    }

    [HttpGet("PlayerCampaign")]
    public IActionResult PlayerCampaign_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.PlayerCampaign.ToList());
    }

    [HttpGet("Prerequisite")]
    public IActionResult Prerequisite_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Prerequisite.ToList());
    }

    [HttpGet("Race")]
    public IActionResult Race_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Race.ToList());
    }

    [HttpGet("RaceAbility")]
    public IActionResult RaceAbility_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.RaceAbility.ToList());
    }

    [HttpGet("RaceSubType")]
    public IActionResult RaceSubType_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.RaceSubType.ToList());
    }

    [HttpGet("Season")]
    public IActionResult Season_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Season.ToList());
    }

    [HttpGet("Skill")]
    public IActionResult Skill_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Skill.ToList());
    }

    [HttpGet("Spell")]
    public IActionResult Spell_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Spell.ToList());
    }

    [HttpGet("SpellDetail")]
    public IActionResult SpellDetail_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellDetail.ToList());
    }

    [HttpGet("SpellSchool")]
    public IActionResult SpellSchool_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellSchool.ToList());
    }

    [HttpGet("SpellSubSchool")]
    public IActionResult SpellSubSchool_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellSubSchool.ToList());
    }

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

    [HttpGet("Weapon")]
    public IActionResult Weapon_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weapon.ToList());
    }

    [HttpGet("WeaponAttribute")]
    public IActionResult WeaponAttribute_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.WeaponAttribute.ToList());
    }

    [HttpGet("WeaponAttributeApplied")]
    public IActionResult WeaponAttributeApplied_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.WeaponAttributeApplied.ToList());
    }

    [HttpGet("Weather")]
    public IActionResult Weather_All()
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weather.ToList());
    }

#endregion

#region Details

    [HttpGet("Armor/{ArmorId:int}")]
    public IActionResult Armor_Detail(int ArmorId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Armor.FirstOrDefault(x => x.GearId == ArmorId));
    }

    [HttpGet("Bestiary/{BestiaryId:int}")]
    public IActionResult Bestiary_Detail(int BestiaryId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Bestiary.FirstOrDefault(x => x.BestiaryId == BestiaryId));
    }

    [HttpGet("BestiaryDetail/{BestiaryDetailId:int}")]
    public IActionResult BestiaryDetail_Detail(int BestiaryDetailId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryDetail.FirstOrDefault(x => x.BestiaryId == BestiaryDetailId));
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

    [HttpGet("BestiaryMagic/{BestiaryMagicId:int}")]
    public IActionResult BestiaryMagic_Detail(int BestiaryMagicId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.BestiaryMagic.FirstOrDefault(x => x.BestiaryMagicId == BestiaryMagicId));
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

    [HttpGet("Campaign/{CampaignId:int}")]
    public IActionResult Campaign_Detail(int CampaignId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Campaign.FirstOrDefault(x => x.CampaignId == CampaignId));
    }

    [HttpGet("CampaignData/{CampaignId:int}/{CampaignDataKey}")]
    public IActionResult CampaignData_Detail(int CampaignId, string Key)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CampaignData.FirstOrDefault(x => x.CampaignId == CampaignId && x.Key.Equals(Key, StringComparison.InvariantCultureIgnoreCase)));
    }

    [HttpGet("Character/{CharacterId:int}")]
    public IActionResult Character_Detail(int CharacterId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Character.FirstOrDefault(x => x.CharacterId == CharacterId));
    }

    [HttpGet("CharacterClassAbility/{CharacterClassAbilityId:int}")]
    public IActionResult CharacterClassAbility_Detail(int CharacterClassAbilityId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterClassAbility.FirstOrDefault(x => x.CharacterClassAbilityId == CharacterClassAbilityId));
    }

    [HttpGet("CharacterClassLevel/{CharacterId:int}/{ClassId:int}")]
    public IActionResult CharacterClassLevel_Detail(int CharacterId, int ClassId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterClassLevel.FirstOrDefault(x => x.CharacterId == CharacterId && x.ClassId == ClassId));
    }

    [HttpGet("CharacterFeat/{CharacterFeatId:int}")]
    public IActionResult CharacterFeat_Detail(int CharacterFeatId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterFeat.FirstOrDefault(x => x.CharacterFeatId == CharacterFeatId));
    }

    [HttpGet("CharacterGear/{CharacterGearId:int}")]
    public IActionResult CharacterGear_Detail(int CharacterGearId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterGear.FirstOrDefault(x => x.CharacterGearId == CharacterGearId));
    }

    [HttpGet("CharacterGearEnchantment/{CharacterGearId:int}/{EnchantmentId:int}")]
    public IActionResult CharacterGearEnchantment_Detail(int CharacterGearId, int EnchantmentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterGearEnchantment.FirstOrDefault(x => x.CharacterGearId == CharacterGearId && x.EnchantmentId == EnchantmentId));
    }

    [HttpGet("CharacterLanguage/{CharacterId:int}/{LanguageId:int}")]
    public IActionResult CharacterLanguage_Detail(int CharacterId, int LanguageId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterLanguage.FirstOrDefault(x => x.CharacterId == CharacterId && x.LanguageId == LanguageId));
    }

    [HttpGet("CharacterMagic/{CharacterMagicId:int}")]
    public IActionResult CharacterMagic_Detail(int CharacterMagicId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterMagic.FirstOrDefault(x => x.CharacterMagicId == CharacterMagicId));
    }

    [HttpGet("CharacterRaceAbility/{CharacterId:int}/{RaceAbilityId:int}")]
    public IActionResult CharacterRaceAbility_Detail(int CharacterId, int RaceAbilityId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterRaceAbility.FirstOrDefault(x => x.CharacterId == CharacterId && x.RaceAbilityId == RaceAbilityId));
    }

    [HttpGet("CharacterSkill/{CharacterId:int}/{SkillId:int}")]
    public IActionResult CharacterSkill_Detail(int CharacterId, int SkillId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.CharacterSkill.FirstOrDefault(x => x.CharacterId == CharacterId && x.SkillId == SkillId));
    }

    [HttpGet("Class/{ClassId:int}")]
    public IActionResult Class_Detail(int ClassId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Class.FirstOrDefault(x => x.ClassId == ClassId));
    }

    [HttpGet("ClassAbility/{ClassAbilityId:int}")]
    public IActionResult ClassAbility_Detail(int ClassAbilityId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ClassAbility.FirstOrDefault(x => x.ClassAbilityId == ClassAbilityId));
    }

    [HttpGet("ClassSkill/{ClassId:int}/{SkillId:int}")]
    public IActionResult ClassSkill_Detail(int ClassId, int SkillId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ClassSkill.FirstOrDefault(x => x.ClassId == ClassId));
    }

    [HttpGet("Continent/{ContinentId:int}")]
    public IActionResult Continent_Detail(int ContinentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Continent.FirstOrDefault(x => x.ContinentId == ContinentId));
    }

    [HttpGet("ContinentEnvironment/{ContinentId:int}/{EnvironmentId:int}")]
    public IActionResult ContinentEnvironment_Detail(int ContinentId, int EnvironmentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentEnvironment.FirstOrDefault(x => x.ContinentId == ContinentId && x.EnvironmentId == EnvironmentId));
    }

    [HttpGet("ContinentWeather/{ContinentWeatherId:int}")]
    public IActionResult ContinentWeather_Detail(int ContinentWeatherId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.ContinentWeather.FirstOrDefault(x => x.CWID == ContinentWeatherId));
    }

    [HttpGet("Enchantment/{EnchantmentId:int}")]
    public IActionResult Enchantment_Detail(int EnchantmentId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Enchantment.FirstOrDefault(x => x.EnchantmentId == EnchantmentId));
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

    [HttpGet("FavoredClass/{FavoredClassId:int}")]
    public IActionResult FavoredClass_Detail(int FavoredClassId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.FavoredClass.FirstOrDefault(x => x.FavoredClassId == FavoredClassId));
    }

    [HttpGet("Feat/{FeatId:int}")]
    public IActionResult Feat_Detail(int FeatId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Feat.FirstOrDefault(x => x.FeatId == FeatId));
    }

    [HttpGet("Gear/{GearId:int}")]
    public IActionResult Gear_Detail(int GearId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Gear.FirstOrDefault(x => x.GearId == GearId));
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

    [HttpGet("Npc/{NpcId:int}")]
    public IActionResult Npc_Detail(int NpcId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Npc.FirstOrDefault(x => x.Npcid == NpcId));
    }

    [HttpGet("Npcdetail/{NpcdetailId:int}")]
    public IActionResult Npcdetail_Detail(int NpcdetailId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Npcdetail.FirstOrDefault(x => x.Npcid == NpcdetailId));
    }

    [HttpGet("OverridePrerequisite/{ClassAbilityId:int}/{PrerequisiteId:int}")]
    public IActionResult OverridePrerequisite_Detail(int ClassAbilityId, int PrerequisiteId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.OverridePrerequisite.FirstOrDefault(x => x.ClassAbilityId == ClassAbilityId && x.PrerequisiteId == PrerequisiteId));
    }

    [HttpGet("Plane/{PlaneId:int}")]
    public IActionResult Plane_Detail(int PlaneId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Plane.FirstOrDefault(x => x.PlaneId == PlaneId));
    }

    [HttpGet("Player/{PlayerId:int}")]
    public IActionResult Player_Detail(int PlayerId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Player.FirstOrDefault(x => x.PlayerId == PlayerId));
    }

    [HttpGet("PlayerCampaign/{PlayerId:int}/{CampaignId:int}")]
    public IActionResult PlayerCampaign_Detail(int PlayerId, int CampaignId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.PlayerCampaign.FirstOrDefault(x => x.PlayerId == PlayerId && x.CampaignId == CampaignId));
    }

    [HttpGet("Prerequisite/{PrerequisiteId:int}")]
    public IActionResult Prerequisite_Detail(int PrerequisiteId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Prerequisite.FirstOrDefault(x => x.PrerequisiteId == PrerequisiteId));
    }

    [HttpGet("Race/{RaceId:int}")]
    public IActionResult Race_Detail(int RaceId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Race.FirstOrDefault(x => x.RaceId == RaceId));
    }

    [HttpGet("RaceAbility/{RaceAbilityId:int}")]
    public IActionResult RaceAbility_Detail(int RaceAbilityId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.RaceAbility.FirstOrDefault(x => x.RaceAbilityId == RaceAbilityId));
    }

    [HttpGet("RaceSubType/{RaceId:int}/{BestiaryTypeId:int}")]
    public IActionResult RaceSubType_Detail(int RaceId, int BestiaryTypeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.RaceSubType.FirstOrDefault(x => x.RaceId == RaceId && x.BestiaryTypeId == BestiaryTypeId));
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

    [HttpGet("SpellDetail/{SpellDetailId:int}")]
    public IActionResult SpellDetail_Detail(int SpellDetailId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellDetail.FirstOrDefault(x => x.SpellId == SpellDetailId));
    }

    [HttpGet("SpellSchool/{SpellSchoolId:int}")]
    public IActionResult SpellSchool_Detail(int SpellSchoolId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellSchool.FirstOrDefault(x => x.SpellSchoolId == SpellSchoolId));
    }

    [HttpGet("SpellSubSchool/{SpellSubSchoolId:int}")]
    public IActionResult SpellSubSchool_Detail(int SpellSubSchoolId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.SpellSubSchool.FirstOrDefault(x => x.SpellSubSchoolId == SpellSubSchoolId));
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

    [HttpGet("Weapon/{WeaponId:int}")]
    public IActionResult Weapon_Detail(int WeaponId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weapon.FirstOrDefault(x => x.GearId == WeaponId));
    }

    [HttpGet("WeaponAttribute/{WeaponAttributeId:int}")]
    public IActionResult WeaponAttribute_Detail(int WeaponAttributeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.WeaponAttribute.FirstOrDefault(x => x.WeaponAttributeId == WeaponAttributeId));
    }

    [HttpGet("WeaponAttributeApplied/{WeaponId:int}/{AttributeId:int}")]
    public IActionResult WeaponAttributeApplied_Detail(int WeaponId, int AttributeId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.WeaponAttributeApplied.FirstOrDefault(x => x.GearId == WeaponId && x.WeaponAttributeId == AttributeId));
    }

    [HttpGet("Weather/{WeatherId:int}")]
    public IActionResult Weather_Detail(int WeatherId)
    {
      var context = PFDAL.GetContext();
      return new JsonResult(context.Weather.FirstOrDefault(x => x.WeatherId == WeatherId));
    }

#endregion

#region Create

    [HttpPost("Armor")]
    public IActionResult Armor_Create([FromBody] Armor obj)
    {
      if (obj == null || obj.GearId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Armor.Add(obj);
        context.SaveChanges();
        return Created($"Armor/{obj.GearId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Bestiary")]
    public IActionResult Bestiary_Create([FromBody] Bestiary obj)
    {
      if (obj == null || obj.BestiaryId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

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
        return BadRequest(ex);
      }
    }

    // BestiaryDetail created as part of Bestiary

    [HttpPost("BestiaryEnvironment")]
    public IActionResult BestiaryEnvironment_Create([FromBody] BestiaryEnvironment obj)
    {
      if (obj == null || obj.BestiaryEnvironmentId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryEnvironment.Add(obj);
        context.SaveChanges();
        return Created($"BestiaryEnvironment/{obj.BestiaryEnvironmentId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiaryFeat")]
    public IActionResult BestiaryFeat_Create([FromBody] BestiaryFeat obj)
    {
      if (obj == null || obj.BestiaryFeatId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryFeat.Add(obj);
        context.SaveChanges();
        return Created($"BestiaryFeat/{obj.BestiaryFeatId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiaryLanguage")]
    public IActionResult BestiaryLanguage_Create([FromBody] BestiaryLanguage obj)
    {
      if (obj == null || obj.BestiaryLanguageId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryLanguage.Add(obj);
        context.SaveChanges();
        return Created($"BestiaryLanguage/{obj.BestiaryLanguageId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiaryMagic")]
    public IActionResult BestiaryMagic_Create([FromBody] BestiaryMagic obj)
    {
      if (obj == null || obj.BestiaryMagicId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryMagic.Add(obj);
        context.SaveChanges();
        return Created($"BestiaryMagic/{obj.BestiaryMagicId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiarySkill")]
    public IActionResult BestiarySkill_Create([FromBody] BestiarySkill obj)
    {
      if (obj == null || obj.BestiarySkillId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiarySkill.Add(obj);
        context.SaveChanges();
        return Created($"BestiarySkill/{obj.BestiarySkillId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiarySubType")]
    public IActionResult BestiarySubType_Create([FromBody] BestiarySubType obj)
    {
      if (obj == null || obj.BestiarySubTypeId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiarySubType.Add(obj);
        context.SaveChanges();
        return Created($"BestiarySubType/{obj.BestiarySubTypeId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("BestiaryType")]
    public IActionResult BestiaryType_Create([FromBody] BestiaryType obj)
    {
      if (obj == null || obj.BestiaryTypeId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryType.Add(obj);
        context.SaveChanges();
        return Created($"BestiaryType/{obj.BestiaryTypeId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Campaign")]
    public IActionResult Campaign_Create([FromBody] Campaign obj)
    {
      if (obj == null || obj.CampaignId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Campaign.Add(obj);
        context.SaveChanges();
        return Created($"Campaign/{obj.CampaignId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CampaignData")]
    public IActionResult CampaignData_Create([FromBody] CampaignData obj)
    {
      if (obj == null || string.IsNullOrWhiteSpace(obj.Key) || obj.CampaignId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CampaignData.Any(x => x.CampaignId == obj.CampaignId && x.Key.Equals(obj.Key, StringComparison.InvariantCultureIgnoreCase)))
        return BadRequest("Object already exists");

      try
      {
        context.CampaignData.Add(obj);
        context.SaveChanges();
        return Created($"CampaignData/{obj.Key}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Character")]
    public IActionResult Character_Create([FromBody] Character obj)
    {
      if (obj == null || obj.CharacterId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Character.Add(obj);
        context.SaveChanges();
        return Created($"Character/{obj.CharacterId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterClassAbility")]
    public IActionResult CharacterClassAbility_Create([FromBody] CharacterClassAbility obj)
    {
      if (obj == null || obj.CharacterClassAbilityId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterClassAbility.Add(obj);
        context.SaveChanges();
        return Created($"CharacterClassAbility/{obj.CharacterClassAbilityId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterClassLevel")]
    public IActionResult CharacterClassLevel_Create([FromBody] CharacterClassLevel obj)
    {
      if (obj == null || obj.CharacterId == 0 || obj.ClassId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CharacterClassLevel.Any(x => x.CharacterId == obj.CharacterId && x.ClassId == obj.ClassId))
        return BadRequest("Object already exists");

      try
      {
        context.CharacterClassLevel.Add(obj);
        context.SaveChanges();
        return Created($"CharacterClassLevel/{obj.CharacterId}/{obj.ClassId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterFeat")]
    public IActionResult CharacterFeat_Create([FromBody] CharacterFeat obj)
    {
      if (obj == null || obj.CharacterFeatId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterFeat.Add(obj);
        context.SaveChanges();
        return Created($"CharacterFeat/{obj.CharacterFeatId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterGear")]
    public IActionResult CharacterGear_Create([FromBody] CharacterGear obj)
    {
      if (obj == null || obj.CharacterGearId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterGear.Add(obj);
        context.SaveChanges();
        return Created($"CharacterGear/{obj.CharacterGearId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterGearEnchantment")]
    public IActionResult CharacterGearEnchantment_Create([FromBody] CharacterGearEnchantment obj)
    {
      if (obj == null || obj.CharacterGearId == 0 || obj.EnchantmentId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CharacterGearEnchantment.Any(x => x.CharacterGearId == obj.CharacterGearId && x.EnchantmentId == obj.EnchantmentId))
        return BadRequest("Object already exists");

      try
      {
        context.CharacterGearEnchantment.Add(obj);
        context.SaveChanges();
        return Created($"CharacterGearEnchantment/{obj.CharacterGearId}/{obj.EnchantmentId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterLanguage")]
    public IActionResult CharacterLanguage_Create([FromBody] CharacterLanguage obj)
    {
      if (obj == null || obj.CharacterId == 0 || obj.LanguageId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CharacterLanguage.Any(x => x.CharacterId == obj.CharacterId && x.LanguageId == obj.LanguageId))
        return BadRequest("Object already exists");

      try
      {
        context.CharacterLanguage.Add(obj);
        context.SaveChanges();
        return Created($"CharacterLanguage/{obj.CharacterId}/{obj.LanguageId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterMagic")]
    public IActionResult CharacterMagic_Create([FromBody] CharacterMagic obj)
    {
      if (obj == null || obj.CharacterMagicId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterMagic.Add(obj);
        context.SaveChanges();
        return Created($"CharacterMagic/{obj.CharacterMagicId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterRaceAbility")]
    public IActionResult CharacterRaceAbility_Create([FromBody] CharacterRaceAbility obj)
    {
      if (obj == null || obj.CharacterId == 0 || obj.RaceAbilityId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CharacterRaceAbility.Any(x => x.CharacterId == obj.CharacterId && x.RaceAbilityId == obj.RaceAbilityId))
        return BadRequest("Object already exists");

      try
      {
        context.CharacterRaceAbility.Add(obj);
        context.SaveChanges();
        return Created($"CharacterRaceAbility/{obj.CharacterId}/{obj.RaceAbilityId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("CharacterSkill")]
    public IActionResult CharacterSkill_Create([FromBody] CharacterSkill obj)
    {
      if (obj == null || obj.CharacterId == 0 || obj.SkillId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.CharacterSkill.Any(x => x.CharacterId == obj.CharacterId && x.SkillId == obj.SkillId))
        return BadRequest("Object already exists");

      try
      {
        context.CharacterSkill.Add(obj);
        context.SaveChanges();
        return Created($"CharacterSkill/{obj.CharacterId}/{obj.SkillId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Class")]
    public IActionResult Class_Create([FromBody] Class obj)
    {
      if (obj == null || obj.ClassId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Class.Add(obj);
        context.SaveChanges();
        return Created($"Class/{obj.ClassId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("ClassAbility")]
    public IActionResult ClassAbility_Create([FromBody] ClassAbility obj)
    {
      if (obj == null || obj.ClassAbilityId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ClassAbility.Add(obj);
        context.SaveChanges();
        return Created($"ClassAbility/{obj.ClassAbilityId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("ClassSkill")]
    public IActionResult ClassSkill_Create([FromBody] ClassSkill obj)
    {
      if (obj == null || obj.ClassId == 0 || obj.SkillId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.ClassSkill.Any(x => x.ClassId == obj.ClassId && x.SkillId == obj.SkillId))
        return BadRequest("Object already exists");

      try
      {
        context.ClassSkill.Add(obj);
        context.SaveChanges();
        return Created($"ClassSkill/{obj.ClassId}/{obj.SkillId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Continent")]
    public IActionResult Continent_Create([FromBody] Continent obj)
    {
      if (obj == null || obj.ContinentId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Continent.Add(obj);
        context.SaveChanges();
        return Created($"Continent/{obj.ContinentId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("ContinentEnvironment")]
    public IActionResult ContinentEnvironment_Create([FromBody] ContinentEnvironment obj)
    {
      if (obj == null || obj.ContinentId == 0 || obj.EnvironmentId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.ContinentEnvironment.Any(x => x.ContinentId == obj.ContinentId && x.EnvironmentId == obj.EnvironmentId))
        return BadRequest("Object already exists");

      try
      {
        context.ContinentEnvironment.Add(obj);
        context.SaveChanges();
        return Created($"ContinentEnvironment/{obj.ContinentId}/{obj.EnvironmentId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("ContinentWeather")]
    public IActionResult ContinentWeather_Create([FromBody] ContinentWeather obj)
    {
      if (obj == null || obj.CWID != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ContinentWeather.Add(obj);
        context.SaveChanges();
        return Created($"ContinentWeather/{obj.CWID.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Enchantment")]
    public IActionResult Enchantment_Create([FromBody] Enchantment obj)
    {
      if (obj == null || obj.EnchantmentId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Enchantment.Add(obj);
        context.SaveChanges();
        return Created($"Enchantment/{obj.EnchantmentId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Environment")]
    public IActionResult Environment_Create([FromBody] DBConnect.DBModels.Environment obj)
    {
      if (obj == null || obj.EnvironmentId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Environment.Add(obj);
        context.SaveChanges();
        return Created($"Environment/{obj.EnvironmentId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Faction")]
    public IActionResult Faction_Create([FromBody] Faction obj)
    {
      if (obj == null || obj.FactionId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Faction.Add(obj);
        context.SaveChanges();
        return Created($"Faction/{obj.FactionId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("FavoredClass")]
    public IActionResult FavoredClass_Create([FromBody] FavoredClass obj)
    {
      if (obj == null || obj.FavoredClassId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.FavoredClass.Add(obj);
        context.SaveChanges();
        return Created($"FavoredClass/{obj.FavoredClassId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Feat")]
    public IActionResult Feat_Create([FromBody] Feat obj)
    {
      if (obj == null || obj.FeatId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Feat.Add(obj);
        context.SaveChanges();
        return Created($"Feat/{obj.FeatId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Gear")]
    public IActionResult Gear_Create([FromBody] Gear obj)
    {
      if (obj == null || obj.GearId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Gear.Add(obj);
        context.SaveChanges();
        return Created($"Gear/{obj.GearId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Language")]
    public IActionResult Language_Create([FromBody] Language obj)
    {
      if (obj == null || obj.LanguageId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Language.Add(obj);
        context.SaveChanges();
        return Created($"Language/{obj.LanguageId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Location")]
    public IActionResult Location_Create([FromBody] Location obj)
    {
      if (obj == null || obj.LocationId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Location.Add(obj);
        context.SaveChanges();
        return Created($"Location/{obj.LocationId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("MagicItem")]
    public IActionResult MagicItem_Create([FromBody] MagicItem obj)
    {
      if (obj == null || obj.MagicItemId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.MagicItem.Add(obj);
        context.SaveChanges();
        return Created($"MagicItem/{obj.MagicItemId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("MonsterSpawn")]
    public IActionResult MonsterSpawn_Create([FromBody] MonsterSpawn obj)
    {
      if (obj == null || obj.SpawnId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.MonsterSpawn.Add(obj);
        context.SaveChanges();
        return Created($"MonsterSpawn/{obj.SpawnId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Month")]
    public IActionResult Month_Create([FromBody] Month obj)
    {
      if (obj == null || obj.MonthId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Month.Add(obj);
        context.SaveChanges();
        return Created($"Month/{obj.MonthId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Npc")]
    public IActionResult Npc_Create([FromBody] Npc obj)
    {
      if (obj == null || obj.Npcid != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Npc.Add(obj);
        context.SaveChanges();
        return Created($"Npc/{obj.Npcid.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Npcdetail")]
    public IActionResult Npcdetail_Create([FromBody] Npcdetail obj)
    {
      if (obj == null || obj.Npcid != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Npcdetail.Add(obj);
        context.SaveChanges();
        return Created($"Npcdetail/{obj.Npcid.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("OverridePrerequisite")]
    public IActionResult OverridePrerequisite_Create([FromBody] OverridePrerequisite obj)
    {
      if (obj == null || obj.ClassAbilityId == 0 || obj.PrerequisiteId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.OverridePrerequisite.Any(x => x.ClassAbilityId == obj.ClassAbilityId && x.PrerequisiteId == obj.PrerequisiteId))
        return BadRequest("Object already exists");

      try
      {
        context.OverridePrerequisite.Add(obj);
        context.SaveChanges();
        return Created($"OverridePrerequisite/{obj.ClassAbilityId}/{obj.PrerequisiteId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Plane")]
    public IActionResult Plane_Create([FromBody] Plane obj)
    {
      if (obj == null || obj.PlaneId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Plane.Add(obj);
        context.SaveChanges();
        return Created($"Plane/{obj.PlaneId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Player")]
    public IActionResult Player_Create([FromBody] Player obj)
    {
      if (obj == null || obj.PlayerId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Player.Add(obj);
        context.SaveChanges();
        return Created($"Player/{obj.PlayerId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("PlayerCampaign")]
    public IActionResult PlayerCampaign_Create([FromBody] PlayerCampaign obj)
    {
      if (obj == null || obj.PlayerId == 0 || obj.CampaignId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.PlayerCampaign.Any(x => x.PlayerId == obj.PlayerId && x.CampaignId == obj.CampaignId))
        return BadRequest("Object already exists");

      try
      {
        context.PlayerCampaign.Add(obj);
        context.SaveChanges();
        return Created($"PlayerCampaign/{obj.PlayerId}/{obj.CampaignId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Prerequisite")]
    public IActionResult Prerequisite_Create([FromBody] Prerequisite obj)
    {
      if (obj == null || obj.PrerequisiteId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Prerequisite.Add(obj);
        context.SaveChanges();
        return Created($"Prerequisite/{obj.PrerequisiteId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Race")]
    public IActionResult Race_Create([FromBody] Race obj)
    {
      if (obj == null || obj.RaceId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Race.Add(obj);
        context.SaveChanges();
        return Created($"Race/{obj.RaceId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("RaceAbility")]
    public IActionResult RaceAbility_Create([FromBody] RaceAbility obj)
    {
      if (obj == null || obj.RaceAbilityId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.RaceAbility.Add(obj);
        context.SaveChanges();
        return Created($"RaceAbility/{obj.RaceAbilityId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("RaceSubType")]
    public IActionResult RaceSubType_Create([FromBody] RaceSubType obj)
    {
      if (obj == null || obj.RaceId == 0 || obj.BestiaryTypeId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.RaceSubType.Any(x => x.RaceId == obj.RaceId && x.BestiaryTypeId == obj.BestiaryTypeId))
        return BadRequest("Object already exists");

      try
      {
        context.RaceSubType.Add(obj);
        context.SaveChanges();
        return Created($"RaceSubType/{obj.RaceId}/{obj.BestiaryTypeId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Season")]
    public IActionResult Season_Create([FromBody] Season obj)
    {
      if (obj == null || obj.SeasonId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Season.Add(obj);
        context.SaveChanges();
        return Created($"Season/{obj.SeasonId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Skill")]
    public IActionResult Skill_Create([FromBody] Skill obj)
    {
      if (obj == null || obj.SkillId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Skill.Add(obj);
        context.SaveChanges();
        return Created($"Skill/{obj.SkillId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Spell")]
    public IActionResult Spell_Create([FromBody] Spell obj)
    {
      if (obj == null || obj.SpellId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

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
        return BadRequest(ex);
      }
    }

    // SpellDetail created as part of Spell

    [HttpPost("SpellSchool")]
    public IActionResult SpellSchool_Create([FromBody] SpellSchool obj)
    {
      if (obj == null || obj.SpellSchoolId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.SpellSchool.Add(obj);
        context.SaveChanges();
        return Created($"SpellSchool/{obj.SpellSchoolId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("SpellSubSchool")]
    public IActionResult SpellSubSchool_Create([FromBody] SpellSubSchool obj)
    {
      if (obj == null || obj.SpellSubSchoolId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.SpellSubSchool.Add(obj);
        context.SaveChanges();
        return Created($"SpellSubSchool/{obj.SpellSubSchoolId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Terrain")]
    public IActionResult Terrain_Create([FromBody] Terrain obj)
    {
      if (obj == null || obj.TerrainId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Terrain.Add(obj);
        context.SaveChanges();
        return Created($"Terrain/{obj.TerrainId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Territory")]
    public IActionResult Territory_Create([FromBody] Territory obj)
    {
      if (obj == null || obj.TerritoryId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Territory.Add(obj);
        context.SaveChanges();
        return Created($"Territory/{obj.TerritoryId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Time")]
    public IActionResult Time_Create([FromBody] Time obj)
    {
      if (obj == null || obj.TimeId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Time.Add(obj);
        context.SaveChanges();
        return Created($"Time/{obj.TimeId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("TrackedEvent")]
    public IActionResult TrackedEvent_Create([FromBody] TrackedEvent obj)
    {
      if (obj == null || obj.TrackedEventId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.TrackedEvent.Add(obj);
        context.SaveChanges();
        return Created($"TrackedEvent/{obj.TrackedEventId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Weapon")]
    public IActionResult Weapon_Create([FromBody] Weapon obj)
    {
      if (obj == null || obj.GearId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Weapon.Add(obj);
        context.SaveChanges();
        return Created($"Weapon/{obj.GearId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("WeaponAttribute")]
    public IActionResult WeaponAttribute_Create([FromBody] WeaponAttribute obj)
    {
      if (obj == null || obj.WeaponAttributeId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.WeaponAttribute.Add(obj);
        context.SaveChanges();
        return Created($"WeaponAttribute/{obj.WeaponAttributeId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("WeaponAttributeApplied")]
    public IActionResult WeaponAttributeApplied_Create([FromBody] WeaponAttributeApplied obj)
    {
      if (obj == null || obj.GearId == 0 || obj.WeaponAttributeId == 0)
        return BadRequest();

      var context = PFDAL.GetContext();
      if (context.WeaponAttributeApplied.Any(x => x.GearId == obj.GearId && x.WeaponAttributeId == obj.WeaponAttributeId))
        return BadRequest("Object already exists");

      try
      {
        context.WeaponAttributeApplied.Add(obj);
        context.SaveChanges();
        return Created($"WeaponAttributeApplied/{obj.GearId}/{obj.WeaponAttributeId}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPost("Weather")]
    public IActionResult Weather_Create([FromBody] Weather obj)
    {
      if (obj == null || obj.WeatherId != 0)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Weather.Add(obj);
        context.SaveChanges();
        return Created($"Weather/{obj.WeatherId.ToString()}", obj);
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

#endregion

#region Update

    [HttpPut("Armor/{ArmorId:int}")]
    public IActionResult Armor_Update([FromBody] Armor obj, int ArmorId)
    {
      if (obj == null || ArmorId == 0 || obj.GearId != ArmorId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Armor.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Bestiary/{BestiaryId:int}")]
    public IActionResult Bestiary_Update([FromBody] Bestiary obj, int BestiaryId)
    {
      if (obj == null || BestiaryId == 0 || obj.BestiaryId != BestiaryId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Bestiary.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryDetail/{BestiaryDetailId:int}")]
    public IActionResult BestiaryDetail_Update([FromBody] BestiaryDetail obj, int BestiaryDetailId)
    {
      if (obj == null || BestiaryDetailId == 0 || obj.BestiaryId != BestiaryDetailId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryDetail.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryEnvironment/{BestiaryEnvironmentId:int}")]
    public IActionResult BestiaryEnvironment_Update([FromBody] BestiaryEnvironment obj, int BestiaryEnvironmentId)
    {
      if (obj == null || BestiaryEnvironmentId == 0 || obj.BestiaryEnvironmentId != BestiaryEnvironmentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryEnvironment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryFeat/{BestiaryFeatId:int}")]
    public IActionResult BestiaryFeat_Update([FromBody] BestiaryFeat obj, int BestiaryFeatId)
    {
      if (obj == null || BestiaryFeatId == 0 || obj.BestiaryFeatId != BestiaryFeatId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryFeat.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryLanguage/{BestiaryLanguageId:int}")]
    public IActionResult BestiaryLanguage_Update([FromBody] BestiaryLanguage obj, int BestiaryLanguageId)
    {
      if (obj == null || BestiaryLanguageId == 0 || obj.BestiaryLanguageId != BestiaryLanguageId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryLanguage.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryMagic/{BestiaryMagicId:int}")]
    public IActionResult BestiaryMagic_Update([FromBody] BestiaryMagic obj, int BestiaryMagicId)
    {
      if (obj == null || BestiaryMagicId == 0 || obj.BestiaryMagicId != BestiaryMagicId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryMagic.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiarySkill/{BestiarySkillId:int}")]
    public IActionResult BestiarySkill_Update([FromBody] BestiarySkill obj, int BestiarySkillId)
    {
      if (obj == null || BestiarySkillId == 0 || obj.BestiarySkillId != BestiarySkillId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiarySkill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiarySubType/{BestiarySubTypeId:int}")]
    public IActionResult BestiarySubType_Update([FromBody] BestiarySubType obj, int BestiarySubTypeId)
    {
      if (obj == null || BestiarySubTypeId == 0 || obj.BestiarySubTypeId != BestiarySubTypeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiarySubType.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("BestiaryType/{BestiaryTypeId:int}")]
    public IActionResult BestiaryType_Update([FromBody] BestiaryType obj, int BestiaryTypeId)
    {
      if (obj == null || BestiaryTypeId == 0 || obj.BestiaryTypeId != BestiaryTypeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.BestiaryType.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Campaign/{CampaignId:int}")]
    public IActionResult Campaign_Update([FromBody] Campaign obj, int CampaignId)
    {
      if (obj == null || CampaignId == 0 || obj.CampaignId != CampaignId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Campaign.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CampaignData/{CampaignId:int}/{CampaignDataKey}")]
    public IActionResult CampaignData_Update([FromBody] CampaignData obj, int CampaignId, string Key)
    {
      if (obj == null || CampaignId == 0 || string.IsNullOrWhiteSpace(Key) || obj.CampaignId != CampaignId || obj.Key != Key)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CampaignData.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Character/{CharacterId:int}")]
    public IActionResult Character_Update([FromBody] Character obj, int CharacterId)
    {
      if (obj == null || CharacterId == 0 || obj.CharacterId != CharacterId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Character.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterClassAbility/{CharacterClassAbilityId:int}")]
    public IActionResult CharacterClassAbility_Update([FromBody] CharacterClassAbility obj, int CharacterClassAbilityId)
    {
      if (obj == null || CharacterClassAbilityId == 0 || obj.CharacterClassAbilityId != CharacterClassAbilityId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterClassAbility.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterClassLevel/{CharacterId:int}/{ClassId:int}")]
    public IActionResult CharacterClassLevel_Update([FromBody] CharacterClassLevel obj, int CharacterId, int ClassId)
    {
      if (obj == null || CharacterId == 0 || ClassId == 0 || obj.CharacterId != CharacterId || obj.ClassId != ClassId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterClassLevel.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterFeat/{CharacterFeatId:int}")]
    public IActionResult CharacterFeat_Update([FromBody] CharacterFeat obj, int CharacterFeatId)
    {
      if (obj == null || CharacterFeatId == 0 || obj.CharacterFeatId != CharacterFeatId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterFeat.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterGear/{CharacterGearId:int}")]
    public IActionResult CharacterGear_Update([FromBody] CharacterGear obj, int CharacterGearId)
    {
      if (obj == null || CharacterGearId == 0 || obj.CharacterGearId != CharacterGearId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterGear.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterGearEnchantment/{CharacterGearId:int}/{EnchantmentId:int}")]
    public IActionResult CharacterGearEnchantment_Update([FromBody] CharacterGearEnchantment obj, int CharacterGearId, int EnchantmentId)
    {
      if (obj == null || CharacterGearId == 0 || EnchantmentId == 0 || obj.CharacterGearId != CharacterGearId || obj.EnchantmentId != EnchantmentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterGearEnchantment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterLanguage/{CharacterId:int}/{LanguageId:int}")]
    public IActionResult CharacterLanguage_Update([FromBody] CharacterLanguage obj, int CharacterId, int LanguageId)
    {
      if (obj == null || CharacterId == 0 || LanguageId == 0 || obj.CharacterId != CharacterId | obj.LanguageId != LanguageId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterLanguage.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterMagic/{CharacterMagicId:int}")]
    public IActionResult CharacterMagic_Update([FromBody] CharacterMagic obj, int CharacterMagicId)
    {
      if (obj == null || CharacterMagicId == 0 || obj.CharacterMagicId != CharacterMagicId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterMagic.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterRaceAbility/{CharacterId:int}/{RaceAbilityId:int}")]
    public IActionResult CharacterRaceAbility_Update([FromBody] CharacterRaceAbility obj, int CharacterId, int RaceAbilityId)
    {
      if (obj == null || CharacterId == 0 || RaceAbilityId == 0 || obj.CharacterId != CharacterId || obj.RaceAbilityId != RaceAbilityId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterRaceAbility.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("CharacterSkill/{CharacterId:int}/{SkillId:int}")]
    public IActionResult CharacterSkill_Update([FromBody] CharacterSkill obj, int CharacterId, int SkillId)
    {
      if (obj == null || CharacterId == 0 || SkillId == 0 || obj.CharacterId != CharacterId || obj.SkillId != SkillId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.CharacterSkill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Class/{ClassId:int}")]
    public IActionResult Class_Update([FromBody] Class obj, int ClassId)
    {
      if (obj == null || ClassId == 0 || obj.ClassId != ClassId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Class.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("ClassAbility/{ClassAbilityId:int}")]
    public IActionResult ClassAbility_Update([FromBody] ClassAbility obj, int ClassAbilityId)
    {
      if (obj == null || ClassAbilityId == 0 || obj.ClassAbilityId != ClassAbilityId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ClassAbility.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("ClassSkill/{ClassId:int}/{SkillId:int}")]
    public IActionResult ClassSkill_Update([FromBody] ClassSkill obj, int ClassId, int SkillId)
    {
      if (obj == null || ClassId == 0 || SkillId == 0 || obj.ClassId != ClassId || obj.SkillId != SkillId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ClassSkill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Continent/{ContinentId:int}")]
    public IActionResult Continent_Update([FromBody] Continent obj, int ContinentId)
    {
      if (obj == null || ContinentId == 0 || obj.ContinentId != ContinentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Continent.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("ContinentEnvironment/{ContinentId:int}/{EnvironmentId:int}")]
    public IActionResult ContinentEnvironment_Update([FromBody] ContinentEnvironment obj, int ContinentId, int EnvironmentId)
    {
      if (obj == null || ContinentId == 0 | EnvironmentId == 0 || obj.ContinentId != ContinentId || obj.EnvironmentId != EnvironmentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ContinentEnvironment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("ContinentWeather/{ContinentWeatherId:int}")]
    public IActionResult ContinentWeather_Update([FromBody] ContinentWeather obj, int ContinentWeatherId)
    {
      if (obj == null || ContinentWeatherId == 0 || obj.CWID != ContinentWeatherId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.ContinentWeather.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Enchantment/{EnchantmentId:int}")]
    public IActionResult Enchantment_Update([FromBody] Enchantment obj, int EnchantmentId)
    {
      if (obj == null || EnchantmentId == 0 || obj.EnchantmentId != EnchantmentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Enchantment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Environment/{EnvironmentId:int}")]
    public IActionResult Environment_Update([FromBody] DBConnect.DBModels.Environment obj, int EnvironmentId)
    {
      if (obj == null || EnvironmentId == 0 || obj.EnvironmentId != EnvironmentId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Environment.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Faction/{FactionId:int}")]
    public IActionResult Faction_Update([FromBody] Faction obj, int FactionId)
    {
      if (obj == null || FactionId == 0 || obj.FactionId != FactionId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Faction.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("FavoredClass/{FavoredClassId:int}")]
    public IActionResult FavoredClass_Update([FromBody] FavoredClass obj, int FavoredClassId)
    {
      if (obj == null || FavoredClassId == 0 || obj.FavoredClassId != FavoredClassId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.FavoredClass.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Feat/{FeatId:int}")]
    public IActionResult Feat_Update([FromBody] Feat obj, int FeatId)
    {
      if (obj == null || FeatId == 0 || obj.FeatId != FeatId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Feat.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Gear/{GearId:int}")]
    public IActionResult Gear_Update([FromBody] Gear obj, int GearId)
    {
      if (obj == null || GearId == 0 || obj.GearId != GearId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Gear.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Language/{LanguageId:int}")]
    public IActionResult Language_Update([FromBody] Language obj, int LanguageId)
    {
      if (obj == null || LanguageId == 0 || obj.LanguageId != LanguageId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Language.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Location/{LocationId:int}")]
    public IActionResult Location_Update([FromBody] Location obj, int LocationId)
    {
      if (obj == null || LocationId == 0 || obj.LocationId != LocationId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Location.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("MagicItem/{MagicItemId:int}")]
    public IActionResult MagicItem_Update([FromBody] MagicItem obj, int MagicItemId)
    {
      if (obj == null || MagicItemId == 0 || obj.MagicItemId != MagicItemId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.MagicItem.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("MonsterSpawn/{MonsterSpawnId:int}")]
    public IActionResult MonsterSpawn_Update([FromBody] MonsterSpawn obj, int MonsterSpawnId)
    {
      if (obj == null || MonsterSpawnId == 0 || obj.SpawnId != MonsterSpawnId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.MonsterSpawn.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Month/{MonthId:int}")]
    public IActionResult Month_Update([FromBody] Month obj, int MonthId)
    {
      if (obj == null || MonthId == 0 || obj.MonthId != MonthId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Month.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Npc/{NpcId:int}")]
    public IActionResult Npc_Update([FromBody] Npc obj, int NpcId)
    {
      if (obj == null || NpcId == 0 || obj.Npcid != NpcId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Npc.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Npcdetail/{NpcdetailId:int}")]
    public IActionResult Npcdetail_Update([FromBody] Npcdetail obj, int NpcdetailId)
    {
      if (obj == null || NpcdetailId == 0 || obj.Npcid != NpcdetailId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Npcdetail.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("OverridePrerequisite/{ClassAbilityId:int}/{PrerequisiteId:int}")]
    public IActionResult OverridePrerequisite_Update([FromBody] OverridePrerequisite obj, int ClassAbilityId, int PrerequisiteId)
    {
      if (obj == null || ClassAbilityId == 0 || PrerequisiteId == 0 || obj.ClassAbilityId != ClassAbilityId || obj.PrerequisiteId != PrerequisiteId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.OverridePrerequisite.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Plane/{PlaneId:int}")]
    public IActionResult Plane_Update([FromBody] Plane obj, int PlaneId)
    {
      if (obj == null || PlaneId == 0 || obj.PlaneId != PlaneId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Plane.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Player/{PlayerId:int}")]
    public IActionResult Player_Update([FromBody] Player obj, int PlayerId)
    {
      if (obj == null || PlayerId == 0 || obj.PlayerId != PlayerId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Player.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("PlayerCampaign/{PlayerId:int}/{CampaignId:int}")]
    public IActionResult PlayerCampaign_Update([FromBody] PlayerCampaign obj, int PlayerId, int CampaignId)
    {
      if (obj == null || PlayerId == 0 || CampaignId == 0 || obj.PlayerId != PlayerId || obj.CampaignId != CampaignId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.PlayerCampaign.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Prerequisite/{PrerequisiteId:int}")]
    public IActionResult Prerequisite_Update([FromBody] Prerequisite obj, int PrerequisiteId)
    {
      if (obj == null || PrerequisiteId == 0 || obj.PrerequisiteId != PrerequisiteId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Prerequisite.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Race/{RaceId:int}")]
    public IActionResult Race_Update([FromBody] Race obj, int RaceId)
    {
      if (obj == null || RaceId == 0 || obj.RaceId != RaceId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Race.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("RaceAbility/{RaceAbilityId:int}")]
    public IActionResult RaceAbility_Update([FromBody] RaceAbility obj, int RaceAbilityId)
    {
      if (obj == null || RaceAbilityId == 0 || obj.RaceAbilityId != RaceAbilityId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.RaceAbility.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("RaceSubType/{RaceId:int}/{BestiaryTypeId:int}")]
    public IActionResult RaceSubType_Update([FromBody] RaceSubType obj, int RaceId, int BestiaryTypeId)
    {
      if (obj == null || RaceId == 0 || BestiaryTypeId == 0 || obj.RaceId != RaceId || obj.BestiaryTypeId != BestiaryTypeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.RaceSubType.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Season/{SeasonId:int}")]
    public IActionResult Season_Update([FromBody] Season obj, int SeasonId)
    {
      if (obj == null || SeasonId == 0 || obj.SeasonId != SeasonId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Season.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Skill/{SkillId:int}")]
    public IActionResult Skill_Update([FromBody] Skill obj, int SkillId)
    {
      if (obj == null || SkillId == 0 || obj.SkillId != SkillId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Skill.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Spell/{SpellId:int}")]
    public IActionResult Spell_Update([FromBody] Spell obj, int SpellId)
    {
      if (obj == null || SpellId == 0 || obj.SpellId != SpellId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Spell.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("SpellDetail/{SpellDetailId:int}")]
    public IActionResult SpellDetail_Update([FromBody] SpellDetail obj, int SpellDetailId)
    {
      if (obj == null || SpellDetailId == 0 || obj.SpellId != SpellDetailId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.SpellDetail.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("SpellSchool/{SpellSchoolId:int}")]
    public IActionResult SpellSchool_Update([FromBody] SpellSchool obj, int SpellSchoolId)
    {
      if (obj == null || SpellSchoolId == 0 || obj.SpellSchoolId != SpellSchoolId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.SpellSchool.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("SpellSubSchool/{SpellSubSchoolId:int}")]
    public IActionResult SpellSubSchool_Update([FromBody] SpellSubSchool obj, int SpellSubSchoolId)
    {
      if (obj == null || SpellSubSchoolId == 0 || obj.SpellSubSchoolId != SpellSubSchoolId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.SpellSubSchool.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Terrain/{TerrainId:int}")]
    public IActionResult Terrain_Update([FromBody] Terrain obj, int TerrainId)
    {
      if (obj == null || TerrainId == 0 || obj.TerrainId != TerrainId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Terrain.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Territory/{TerritoryId:int}")]
    public IActionResult Territory_Update([FromBody] Territory obj, int TerritoryId)
    {
      if (obj == null || TerritoryId == 0 || obj.TerritoryId != TerritoryId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Territory.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Time/{TimeId:int}")]
    public IActionResult Time_Update([FromBody] Time obj, int TimeId)
    {
      if (obj == null || TimeId == 0 || obj.TimeId != TimeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Time.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("TrackedEvent/{TrackedEventId:int}")]
    public IActionResult TrackedEvent_Update([FromBody] TrackedEvent obj, int TrackedEventId)
    {
      if (obj == null || TrackedEventId == 0 || obj.TrackedEventId != TrackedEventId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.TrackedEvent.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Weapon/{WeaponId:int}")]
    public IActionResult Weapon_Update([FromBody] Weapon obj, int WeaponId)
    {
      if (obj == null || WeaponId == 0 || obj.GearId != WeaponId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Weapon.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("WeaponAttribute/{WeaponAttributeId:int}")]
    public IActionResult WeaponAttribute_Update([FromBody] WeaponAttribute obj, int WeaponAttributeId)
    {
      if (obj == null || WeaponAttributeId == 0 || obj.WeaponAttributeId != WeaponAttributeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.WeaponAttribute.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("WeaponAttributeApplied/{GearId:int}/{WeaponAttributeId:int}")]
    public IActionResult WeaponAttributeApplied_Update([FromBody] WeaponAttributeApplied obj, int GearId, int WeaponAttributeId)
    {
      if (obj == null || GearId == 0 || WeaponAttributeId == 0 || obj.GearId != GearId || obj.WeaponAttributeId != WeaponAttributeId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.WeaponAttributeApplied.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }

    [HttpPut("Weather/{WeatherId:int}")]
    public IActionResult Weather_Update([FromBody] Weather obj, int WeatherId)
    {
      if (obj == null || WeatherId == 0 || obj.WeatherId != WeatherId)
        return BadRequest();

      var context = PFDAL.GetContext();

      try
      {
        context.Weather.Attach(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        context.SaveChanges();
        return NoContent();
      }
      catch (Exception ex)
      {
        return BadRequest(ex);
      }
    }


#endregion

#region Delete

    [HttpDelete("Armor/{ArmorId:int}")]
    public IActionResult Armor_Delete(int ArmorId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Armor.FirstOrDefault(x => x.GearId == ArmorId);
      if (obj == null || obj.GearId != ArmorId)
        return NotFound();

      context.Armor.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Bestiary/{BestiaryId:int}")]
    public IActionResult Bestiary_Delete(int BestiaryId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Bestiary.FirstOrDefault(x => x.BestiaryId == BestiaryId);
      if (obj == null || obj.BestiaryId != BestiaryId)
        return NotFound();

      var obj2 = context.BestiaryDetail.FirstOrDefault(x => x.BestiaryId == BestiaryId);
      if (obj != null)
        context.BestiaryDetail.Remove(obj2);

      context.Bestiary.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    // BestiaryDetail deleted with Bestiary

    [HttpDelete("BestiaryEnvironment/{BestiaryEnvironmentId:int}")]
    public IActionResult BestiaryEnvironment_Delete(int BestiaryEnvironmentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiaryEnvironment.FirstOrDefault(x => x.BestiaryEnvironmentId == BestiaryEnvironmentId);
      if (obj == null || obj.BestiaryEnvironmentId != BestiaryEnvironmentId)
        return NotFound();

      context.BestiaryEnvironment.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiaryFeat/{BestiaryFeatId:int}")]
    public IActionResult BestiaryFeat_Delete(int BestiaryFeatId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiaryFeat.FirstOrDefault(x => x.BestiaryFeatId == BestiaryFeatId);
      if (obj == null || obj.BestiaryFeatId != BestiaryFeatId)
        return NotFound();

      context.BestiaryFeat.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiaryLanguage/{BestiaryLanguageId:int}")]
    public IActionResult BestiaryLanguage_Delete(int BestiaryLanguageId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiaryLanguage.FirstOrDefault(x => x.BestiaryLanguageId == BestiaryLanguageId);
      if (obj == null || obj.BestiaryLanguageId != BestiaryLanguageId)
        return NotFound();

      context.BestiaryLanguage.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiaryMagic/{BestiaryMagicId:int}")]
    public IActionResult BestiaryMagic_Delete(int BestiaryMagicId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiaryMagic.FirstOrDefault(x => x.BestiaryMagicId == BestiaryMagicId);
      if (obj == null || obj.BestiaryMagicId != BestiaryMagicId)
        return NotFound();

      context.BestiaryMagic.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiarySkill/{BestiarySkillId:int}")]
    public IActionResult BestiarySkill_Delete(int BestiarySkillId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiarySkill.FirstOrDefault(x => x.BestiarySkillId == BestiarySkillId);
      if (obj == null || obj.BestiarySkillId != BestiarySkillId)
        return NotFound();

      context.BestiarySkill.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiarySubType/{BestiarySubTypeId:int}")]
    public IActionResult BestiarySubType_Delete(int BestiarySubTypeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiarySubType.FirstOrDefault(x => x.BestiarySubTypeId == BestiarySubTypeId);
      if (obj == null || obj.BestiarySubTypeId != BestiarySubTypeId)
        return NotFound();

      context.BestiarySubType.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("BestiaryType/{BestiaryTypeId:int}")]
    public IActionResult BestiaryType_Delete(int BestiaryTypeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.BestiaryType.FirstOrDefault(x => x.BestiaryTypeId == BestiaryTypeId);
      if (obj == null || obj.BestiaryTypeId != BestiaryTypeId)
        return NotFound();

      context.BestiaryType.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Campaign/{CampaignId:int}")]
    public IActionResult Campaign_Delete(int CampaignId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Campaign.FirstOrDefault(x => x.CampaignId == CampaignId);
      if (obj == null || obj.CampaignId != CampaignId)
        return NotFound();

      context.Campaign.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CampaignData/{CampaignDataId:int}/{CampaignDataKey}")]
    public IActionResult CampaignData_Delete(int CampaignId, string Key)
    {
      var context = PFDAL.GetContext();

      var obj = context.CampaignData.FirstOrDefault(x => x.CampaignId == CampaignId && x.Key.Equals(Key, StringComparison.InvariantCultureIgnoreCase));
      if (obj == null || obj.CampaignId != CampaignId)
        return NotFound();

      context.CampaignData.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Character/{CharacterId:int}")]
    public IActionResult Character_Delete(int CharacterId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Character.FirstOrDefault(x => x.CharacterId == CharacterId);
      if (obj == null || obj.CharacterId != CharacterId)
        return NotFound();

      context.Character.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterClassAbility/{CharacterClassAbilityId:int}")]
    public IActionResult CharacterClassAbility_Delete(int CharacterClassAbilityId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterClassAbility.FirstOrDefault(x => x.CharacterClassAbilityId == CharacterClassAbilityId);
      if (obj == null || obj.CharacterClassAbilityId != CharacterClassAbilityId)
        return NotFound();

      context.CharacterClassAbility.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterClassLevel/{CharacterId:int}/{ClassId:int}")]
    public IActionResult CharacterClassLevel_Delete(int CharacterId, int ClassId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterClassLevel.FirstOrDefault(x => x.CharacterId == CharacterId && x.ClassId == ClassId);
      if (obj == null || CharacterId == 0 || ClassId == 0 || obj.CharacterId != CharacterId || obj.ClassId != ClassId)
        return NotFound();

      context.CharacterClassLevel.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterFeat/{CharacterFeatId:int}")]
    public IActionResult CharacterFeat_Delete(int CharacterFeatId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterFeat.FirstOrDefault(x => x.CharacterFeatId == CharacterFeatId);
      if (obj == null || obj.CharacterFeatId != CharacterFeatId)
        return NotFound();

      context.CharacterFeat.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterGear/{CharacterGearId:int}")]
    public IActionResult CharacterGear_Delete(int CharacterGearId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterGear.FirstOrDefault(x => x.CharacterGearId == CharacterGearId);
      if (obj == null || obj.CharacterGearId != CharacterGearId)
        return NotFound();

      context.CharacterGear.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterGearEnchantment/{CharacterGearId:int}/{EnchantmentId:int}")]
    public IActionResult CharacterGearEnchantment_Delete(int CharacterGearId, int EnchantmentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterGearEnchantment.FirstOrDefault(x => x.CharacterGearId == CharacterGearId && x.EnchantmentId == EnchantmentId);
      if (obj == null || obj.CharacterGearId != CharacterGearId || obj.EnchantmentId != EnchantmentId)
        return NotFound();

      context.CharacterGearEnchantment.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterLanguage/{CharacterId:int}/{LanguageId:int}")]
    public IActionResult CharacterLanguage_Delete(int CharacterId, int LanguageId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterLanguage.FirstOrDefault(x => x.CharacterId == CharacterId && x.LanguageId == LanguageId);
      if (obj == null || obj.CharacterId != CharacterId || obj.LanguageId != LanguageId)
        return NotFound();

      context.CharacterLanguage.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterMagic/{CharacterMagicId:int}")]
    public IActionResult CharacterMagic_Delete(int CharacterMagicId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterMagic.FirstOrDefault(x => x.CharacterMagicId == CharacterMagicId);
      if (obj == null || obj.CharacterMagicId != CharacterMagicId)
        return NotFound();

      context.CharacterMagic.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterRaceAbility/{CharacterId:int}/{RaceAbilityId:int}")]
    public IActionResult CharacterRaceAbility_Delete(int CharacterId, int RaceAbilityId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterRaceAbility.FirstOrDefault(x => x.CharacterId == CharacterId && x.RaceAbilityId == RaceAbilityId);
      if (obj == null || obj.CharacterId != CharacterId || obj.RaceAbilityId != RaceAbilityId)
        return NotFound();

      context.CharacterRaceAbility.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("CharacterSkill/{CharacterId:int}/{SkillId:int}")]
    public IActionResult CharacterSkill_Delete(int CharacterId, int SkillId)
    {
      var context = PFDAL.GetContext();

      var obj = context.CharacterSkill.FirstOrDefault(x => x.CharacterId == CharacterId && x.SkillId == SkillId);
      if (obj == null || obj.CharacterId != CharacterId || obj.SkillId != SkillId)
        return NotFound();

      context.CharacterSkill.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Class/{ClassId:int}")]
    public IActionResult Class_Delete(int ClassId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Class.FirstOrDefault(x => x.ClassId == ClassId);
      if (obj == null || obj.ClassId != ClassId)
        return NotFound();

      context.Class.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("ClassAbility/{ClassAbilityId:int}")]
    public IActionResult ClassAbility_Delete(int ClassAbilityId)
    {
      var context = PFDAL.GetContext();

      var obj = context.ClassAbility.FirstOrDefault(x => x.ClassAbilityId == ClassAbilityId);
      if (obj == null || obj.ClassAbilityId != ClassAbilityId)
        return NotFound();

      context.ClassAbility.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("ClassSkill/{ClassId:int}/{SkillId:int}")]
    public IActionResult ClassSkill_Delete(int ClassId, int SkillId)
    {
      var context = PFDAL.GetContext();

      var obj = context.ClassSkill.FirstOrDefault(x => x.ClassId == ClassId && x.SkillId == SkillId);
      if (obj == null || obj.ClassId != ClassId || obj.SkillId != SkillId)
        return NotFound();

      context.ClassSkill.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Continent/{ContinentId:int}")]
    public IActionResult Continent_Delete(int ContinentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Continent.FirstOrDefault(x => x.ContinentId == ContinentId);
      if (obj == null || obj.ContinentId != ContinentId)
        return NotFound();

      context.Continent.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("ContinentEnvironment/{ContinentId:int}/{EnvironmentId:int}")]
    public IActionResult ContinentEnvironment_Delete(int ContinentId, int EnvironmentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.ContinentEnvironment.FirstOrDefault(x => x.ContinentId == ContinentId && x.EnvironmentId == EnvironmentId);
      if (obj == null || obj.ContinentId != ContinentId || obj.EnvironmentId != EnvironmentId)
        return NotFound();

      context.ContinentEnvironment.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("ContinentWeather/{ContinentWeatherId:int}")]
    public IActionResult ContinentWeather_Delete(int ContinentWeatherId)
    {
      var context = PFDAL.GetContext();

      var obj = context.ContinentWeather.FirstOrDefault(x => x.CWID == ContinentWeatherId);
      if (obj == null || obj.CWID != ContinentWeatherId)
        return NotFound();

      context.ContinentWeather.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Enchantment/{EnchantmentId:int}")]
    public IActionResult Enchantment_Delete(int EnchantmentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Enchantment.FirstOrDefault(x => x.EnchantmentId == EnchantmentId);
      if (obj == null || obj.EnchantmentId != EnchantmentId)
        return NotFound();

      context.Enchantment.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Environment/{EnvironmentId:int}")]
    public IActionResult Environment_Delete(int EnvironmentId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Environment.FirstOrDefault(x => x.EnvironmentId == EnvironmentId);
      if (obj == null || obj.EnvironmentId != EnvironmentId)
        return NotFound();

      context.Environment.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Faction/{FactionId:int}")]
    public IActionResult Faction_Delete(int FactionId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Faction.FirstOrDefault(x => x.FactionId == FactionId);
      if (obj == null || obj.FactionId != FactionId)
        return NotFound();

      context.Faction.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("FavoredClass/{FavoredClassId:int}")]
    public IActionResult FavoredClass_Delete(int FavoredClassId)
    {
      var context = PFDAL.GetContext();

      var obj = context.FavoredClass.FirstOrDefault(x => x.FavoredClassId == FavoredClassId);
      if (obj == null || obj.FavoredClassId != FavoredClassId)
        return NotFound();

      context.FavoredClass.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Feat/{FeatId:int}")]
    public IActionResult Feat_Delete(int FeatId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Feat.FirstOrDefault(x => x.FeatId == FeatId);
      if (obj == null || obj.FeatId != FeatId)
        return NotFound();

      context.Feat.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Gear/{GearId:int}")]
    public IActionResult Gear_Delete(int GearId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Gear.FirstOrDefault(x => x.GearId == GearId);
      if (obj == null || obj.GearId != GearId)
        return NotFound();

      context.Gear.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Language/{LanguageId:int}")]
    public IActionResult Language_Delete(int LanguageId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Language.FirstOrDefault(x => x.LanguageId == LanguageId);
      if (obj == null || obj.LanguageId != LanguageId)
        return NotFound();

      context.Language.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Location/{LocationId:int}")]
    public IActionResult Location_Delete(int LocationId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Location.FirstOrDefault(x => x.LocationId == LocationId);
      if (obj == null || obj.LocationId != LocationId)
        return NotFound();

      context.Location.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("MagicItem/{MagicItemId:int}")]
    public IActionResult MagicItem_Delete(int MagicItemId)
    {
      var context = PFDAL.GetContext();

      var obj = context.MagicItem.FirstOrDefault(x => x.MagicItemId == MagicItemId);
      if (obj == null || obj.MagicItemId != MagicItemId)
        return NotFound();

      context.MagicItem.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("MonsterSpawn/{MonsterSpawnId:int}")]
    public IActionResult MonsterSpawn_Delete(int MonsterSpawnId)
    {
      var context = PFDAL.GetContext();

      var obj = context.MonsterSpawn.FirstOrDefault(x => x.SpawnId == MonsterSpawnId);
      if (obj == null || obj.SpawnId != MonsterSpawnId)
        return NotFound();

      context.MonsterSpawn.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Month/{MonthId:int}")]
    public IActionResult Month_Delete(int MonthId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Month.FirstOrDefault(x => x.MonthId == MonthId);
      if (obj == null || obj.MonthId != MonthId)
        return NotFound();

      context.Month.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Npc/{NpcId:int}")]
    public IActionResult Npc_Delete(int NpcId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Npc.FirstOrDefault(x => x.Npcid == NpcId);
      if (obj == null || obj.Npcid != NpcId)
        return NotFound();

      context.Npc.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Npcdetail/{NpcdetailId:int}")]
    public IActionResult Npcdetail_Delete(int NpcdetailId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Npcdetail.FirstOrDefault(x => x.Npcid == NpcdetailId);
      if (obj == null || obj.Npcid != NpcdetailId)
        return NotFound();

      context.Npcdetail.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("OverridePrerequisite/{ClassAbilityId:int}/{PrerequisiteId:int}")]
    public IActionResult OverridePrerequisite_Delete(int ClassAbilityId, int PrerequisiteId)
    {
      var context = PFDAL.GetContext();

      var obj = context.OverridePrerequisite.FirstOrDefault(x => x.ClassAbilityId == ClassAbilityId && x.PrerequisiteId == PrerequisiteId);
      if (obj == null || obj.ClassAbilityId != ClassAbilityId || obj.PrerequisiteId != PrerequisiteId)
        return NotFound();

      context.OverridePrerequisite.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Plane/{PlaneId:int}")]
    public IActionResult Plane_Delete(int PlaneId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Plane.FirstOrDefault(x => x.PlaneId == PlaneId);
      if (obj == null || obj.PlaneId != PlaneId)
        return NotFound();

      context.Plane.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Player/{PlayerId:int}")]
    public IActionResult Player_Delete(int PlayerId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Player.FirstOrDefault(x => x.PlayerId == PlayerId);
      if (obj == null || obj.PlayerId != PlayerId)
        return NotFound();

      context.Player.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("PlayerCampaign/{PlayerId:int}/{CampaignId:int}")]
    public IActionResult PlayerCampaign_Delete(int PlayerId, int CampaignId)
    {
      var context = PFDAL.GetContext();

      var obj = context.PlayerCampaign.FirstOrDefault(x => x.PlayerId == PlayerId && x.CampaignId== CampaignId);
      if (obj == null || obj.PlayerId != PlayerId || obj.CampaignId != CampaignId)
        return NotFound();

      context.PlayerCampaign.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Prerequisite/{PrerequisiteId:int}")]
    public IActionResult Prerequisite_Delete(int PrerequisiteId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Prerequisite.FirstOrDefault(x => x.PrerequisiteId == PrerequisiteId);
      if (obj == null || obj.PrerequisiteId != PrerequisiteId)
        return NotFound();

      context.Prerequisite.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Race/{RaceId:int}")]
    public IActionResult Race_Delete(int RaceId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Race.FirstOrDefault(x => x.RaceId == RaceId);
      if (obj == null || obj.RaceId != RaceId)
        return NotFound();

      context.Race.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("RaceAbility/{RaceAbilityId:int}")]
    public IActionResult RaceAbility_Delete(int RaceAbilityId)
    {
      var context = PFDAL.GetContext();

      var obj = context.RaceAbility.FirstOrDefault(x => x.RaceAbilityId == RaceAbilityId);
      if (obj == null || obj.RaceAbilityId != RaceAbilityId)
        return NotFound();

      context.RaceAbility.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("RaceSubType/{RaceId:int}/{BestiaryTypeId:int}")]
    public IActionResult RaceSubType_Delete(int RaceId, int BestiaryTypeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.RaceSubType.FirstOrDefault(x => x.RaceId == RaceId && x.BestiaryTypeId == BestiaryTypeId);
      if (obj == null || obj.RaceId != RaceId || obj.BestiaryTypeId != BestiaryTypeId)
        return NotFound();

      context.RaceSubType.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Season/{SeasonId:int}")]
    public IActionResult Season_Delete(int SeasonId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Season.FirstOrDefault(x => x.SeasonId == SeasonId);
      if (obj == null || obj.SeasonId != SeasonId)
        return NotFound();

      context.Season.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Skill/{SkillId:int}")]
    public IActionResult Skill_Delete(int SkillId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Skill.FirstOrDefault(x => x.SkillId == SkillId);
      if (obj == null || obj.SkillId != SkillId)
        return NotFound();

      context.Skill.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Spell/{SpellId:int}")]
    public IActionResult Spell_Delete(int SpellId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Spell.FirstOrDefault(x => x.SpellId == SpellId);
      if (obj == null || obj.SpellId != SpellId)
        return NotFound();

      var obj2 = context.SpellDetail.FirstOrDefault(x => x.SpellId == SpellId);
      if (obj != null)
        context.SpellDetail.Remove(obj2);

      context.Spell.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    // SpellDetail deleted with Spell;

    [HttpDelete("SpellSchool/{SpellSchoolId:int}")]
    public IActionResult SpellSchool_Delete(int SpellSchoolId)
    {
      var context = PFDAL.GetContext();

      var obj = context.SpellSchool.FirstOrDefault(x => x.SpellSchoolId == SpellSchoolId);
      if (obj == null || obj.SpellSchoolId != SpellSchoolId)
        return NotFound();

      context.SpellSchool.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("SpellSubSchool/{SpellSubSchoolId:int}")]
    public IActionResult SpellSubSchool_Delete(int SpellSubSchoolId)
    {
      var context = PFDAL.GetContext();

      var obj = context.SpellSubSchool.FirstOrDefault(x => x.SpellSubSchoolId == SpellSubSchoolId);
      if (obj == null || obj.SpellSubSchoolId != SpellSubSchoolId)
        return NotFound();

      context.SpellSubSchool.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Terrain/{TerrainId:int}")]
    public IActionResult Terrain_Delete(int TerrainId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Terrain.FirstOrDefault(x => x.TerrainId == TerrainId);
      if (obj == null || obj.TerrainId != TerrainId)
        return NotFound();

      context.Terrain.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Territory/{TerritoryId:int}")]
    public IActionResult Territory_Delete(int TerritoryId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Territory.FirstOrDefault(x => x.TerritoryId == TerritoryId);
      if (obj == null || obj.TerritoryId != TerritoryId)
        return NotFound();

      context.Territory.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Time/{TimeId:int}")]
    public IActionResult Time_Delete(int TimeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Time.FirstOrDefault(x => x.TimeId == TimeId);
      if (obj == null || obj.TimeId != TimeId)
        return NotFound();

      context.Time.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("TrackedEvent/{TrackedEventId:int}")]
    public IActionResult TrackedEvent_Delete(int TrackedEventId)
    {
      var context = PFDAL.GetContext();

      var obj = context.TrackedEvent.FirstOrDefault(x => x.TrackedEventId == TrackedEventId);
      if (obj == null || obj.TrackedEventId != TrackedEventId)
        return NotFound();

      context.TrackedEvent.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Weapon/{WeaponId:int}")]
    public IActionResult Weapon_Delete(int WeaponId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Weapon.FirstOrDefault(x => x.GearId == WeaponId);
      if (obj == null || obj.GearId != WeaponId)
        return NotFound();

      context.Weapon.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("WeaponAttribute/{WeaponAttributeId:int}")]
    public IActionResult WeaponAttribute_Delete(int WeaponAttributeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.WeaponAttribute.FirstOrDefault(x => x.WeaponAttributeId == WeaponAttributeId);
      if (obj == null || obj.WeaponAttributeId != WeaponAttributeId)
        return NotFound();

      context.WeaponAttribute.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("WeaponAttributeApplied/{GearId:int}/{WeaponAttributeId:int}")]
    public IActionResult WeaponAttributeApplied_Delete(int GearId, int WeaponAttributeId)
    {
      var context = PFDAL.GetContext();

      var obj = context.WeaponAttributeApplied.FirstOrDefault(x => x.GearId == GearId && x.WeaponAttributeId == WeaponAttributeId);
      if (obj == null || obj.GearId != GearId || obj.WeaponAttributeId != WeaponAttributeId)
        return NotFound();

      context.WeaponAttributeApplied.Remove(obj);
      context.SaveChanges();
      return Ok();
    }

    [HttpDelete("Weather/{WeatherId:int}")]
    public IActionResult Weather_Delete(int WeatherId)
    {
      var context = PFDAL.GetContext();

      var obj = context.Weather.FirstOrDefault(x => x.WeatherId == WeatherId);
      if (obj == null || obj.WeatherId != WeatherId)
        return NotFound();

      context.Weather.Remove(obj);
      context.SaveChanges();
      return Ok();
    }




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