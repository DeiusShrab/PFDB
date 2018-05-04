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
  [Authorize]
  public class DMController : PFDBController
  {
    #region HTTP Methods

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    public IActionResult LoginPage()
    {
      return View("LoginPage");
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public IActionResult Login(JObject info)
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
        HttpContext.Response.Headers.Add("Bearer", content);

        return Index();
      }

      return Forbid();
    }

    #endregion
  }
}