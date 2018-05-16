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
  [Authorize]
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
      object player = null;
      if (player != null)
      {
        // List all player-owned characters and equipment, plus options to make new ones

        return View();
      }

      return BadRequest();
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