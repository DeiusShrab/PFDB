using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using DBConnect;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace PFDBSite.Controllers
{
  [Route("DM")]
  [Authorize(Roles = "DM,Admin")]
  public class DMController : PFDBController
  {
    #region HTTP Methods

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    #endregion
  }
}