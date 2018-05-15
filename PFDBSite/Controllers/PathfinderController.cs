using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using DBConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  [Authorize(Policy = "PlayerInfo")]
  public class PathfinderController : Controller
  {
    [AllowAnonymous]
    public IActionResult Index()
    {
      return View();
    }

    [AllowAnonymous]
    public IActionResult Maps()
    {
      return View();
    }

    public IActionResult Characters()
    {
      var principal = Thread.CurrentPrincipal as ClaimsPrincipal;
      var ident = principal.Identity as ClaimsIdentity;
      var claim = ident.Claims.First(x => x.Type == ClaimTypes.Name);
      var userName = claim.Value;
      var context = PFDAL.GetContext();
      var player = context.Player.Find(userName);
      if (player != null)
      {
        // List all player-owned characters and equipment, plus options to make new ones

        return View();
      }

      return BadRequest();
    }

    [AllowAnonymous]
    public IActionResult Login()
    {
      return View();
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Login(LoginViewModel model)
    {
      if (ModelState.IsValid)
      {
        if (PFDAL.ValidatePassword(model.Username, model.Password))
        {
          var claims = new List<Claim>
          {
            new Claim("PFPlayer", model.Username),
            new Claim(ClaimTypes.Role, "User")
          };

          var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

          var authProperties = new AuthenticationProperties
          {
            AllowRefresh = true,
          };
          
          SignIn(claimsIdentity, authProperties);

          return RedirectToAction("Index");
        }
        else
        {
          ModelState.AddModelError("", "Invalid Username and/or Password");
        }
      }
      else
      {
        ModelState.AddModelError("", "Model Invalid");
      }

      return View(model);
    }

    public IActionResult Logout()
    {
      SignOut();
      return RedirectToAction("Index");
    }

    [HttpGet("Characters/{characterId:int}")]
    public IActionResult CharacterSheet(int characterId)
    {
      var context = PFDAL.GetContext();
      var character = context.Character.Find(characterId);
      if (character != null)
      {
        var model = new CharacterSheetViewModel();
        model.Character = character;

        return View(model);
      }

      return NotFound();
    }

    private async void SignIn(ClaimsIdentity claimsIdentity, AuthenticationProperties authProperties)
    {
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    }

    private async void SignOut()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
  }
}