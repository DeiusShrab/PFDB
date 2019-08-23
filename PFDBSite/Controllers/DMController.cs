using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using DBConnect;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  [Route("DM")]
  [Authorize(Roles = "DM,Admin")]
  public class DMController : PFDBController
  {
    public DMController(UserManager<ApplicationUser> userManager) : base(userManager)
    {

    }

    #region HTTP Methods

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("Bestiary")]
    public IActionResult BestiaryList()
    {
      var context = PFDAL.GetContext();
      var model = new Views.DM.Bestiary_.CreateModel(context);
      return View(model);
    }

    [HttpGet("Bestiary/Details")]
    public IActionResult BestiaryDetail()
    {
      var context = PFDAL.GetContext();
      var model = new Views.DM.Bestiary_.DetailsModel(context);
      return View(model);
    }

    #endregion
  }
}