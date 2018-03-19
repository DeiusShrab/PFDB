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
        public IActionResult GetAllContinents()
        {
            return new ObjectResult(context.Continent.ToList());
        }

        [HttpPost("Encounter")]
        public IActionResult GenRandomEncounter([FromBody] RandomEncounterRequest request)
        {
            var ret = new RandomEncounterResult();
            ret.Success = false;

            ret = helperRandom.GenerateRandomEncounters(request);

            return new ObjectResult(ret);
        }
    }
}