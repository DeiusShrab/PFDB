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

        public IActionResult Continents()
        {
            return new ObjectResult(context.Continent.ToList());
        }
        
        public IActionResult Seasons()
        {
            return new ObjectResult(context.Season.ToList());
        }

        [HttpPost]
        public IActionResult RandomEncounter([FromBody] RandomEncounterRequest request)
        {
            return new ObjectResult(helperRandom.GenerateRandomEncounters(request));
        }
    }
}