using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFDAL.ConnectModels;
using PFDAL.Models;
using PFDBSite.Helpers;

namespace PFDBSite.Controllers
{
  [Produces("application/json")]
  [Route("api")]
  public class ApiController : Controller
  {
    public PFDBContext context = new PFDBContext();
    private RandomHelper helperRandom = new RandomHelper();

    [HttpGet("Continents")]
    public IActionResult Continents()
    {
      return new ObjectResult(context.Continent.ToList());
    }

    [HttpGet("Seasons")]
    public IActionResult Seasons()
    {
      return new ObjectResult(context.Season.ToList());
    }

    [HttpPost("RandomEncounter")]
    public IActionResult RandomEncounter([FromBody] RandomEncounterRequest request)
    {
      return new ObjectResult(helperRandom.GenerateRandomEncounters(request));
    }

    [HttpGet("bestiary/{bestiaryId:int}")]
    public IActionResult Bestiary(int bestiaryId)
    {
      return new ObjectResult(context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId));
    }

    [HttpGet("bestiarydetail/{bestiaryId:int}")]
    public IActionResult BestiaryDetail(int bestiaryId)
    {
      return new ObjectResult(context.BestiaryDetail.FirstOrDefault(x => x.BestiaryId == bestiaryId));
    }
  }
}