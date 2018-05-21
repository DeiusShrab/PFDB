using System;
using System.Diagnostics;
using System.Threading.Tasks;
using DBConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  public class HomeController : PFDBController
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Maps()
    {
      return View();
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult EnableRoles()
    {

      return Ok();
    }

    private async Task CreateRolesAndAdmin(IServiceProvider serviceProvider, string userName, string email, string pass)
    {
      var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      string[] roleNames = { "DM", "Admin" };
      IdentityResult roleResult;

      foreach (var roleName in roleNames)
      {
        var roleExists = await RoleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
          roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
        }
      }

      var adminUser = new ApplicationUser
      {
        UserName = userName,
        Email = email
      };

      string userPassword = pass;
      var _user = UserManager.FindByEmailAsync(email);
    }
  }
}
