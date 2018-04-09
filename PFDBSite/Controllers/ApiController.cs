using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFDAL;
using PFDAL.ConnectModels;
using PFDBSite.Helpers;

namespace PFDBSite.Controllers
{
  [Produces("application/json")]
  [Route("api")]
  public class ApiController : PFDBController
  {
    public IPFDBContext context = PFDAL.PFDAL.GetContext(Configuration["WorkingEnvironment"]);
    private RandomHelper helperRandom;

    public ApiController()
    {
      helperRandom = new RandomHelper(context);
    }

    [HttpGet("Continent")]
    public IActionResult ContinentList()
    {
      return new JsonResult(context.Continent.ToList());
    }

    [HttpGet("Season")]
    public IActionResult SeasonList()
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
  }
}