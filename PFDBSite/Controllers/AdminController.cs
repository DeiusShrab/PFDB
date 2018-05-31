using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PFDBSite.Models;
using PFDBSite.Models.AdminModels;

namespace PFDBSite.Controllers
{
  [AutoValidateAntiforgeryToken]
  [Authorize(Roles = "Admin")]
  public class AdminController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AdminController(
      UserManager<ApplicationUser> userManager,
      RoleManager<IdentityRole> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public IActionResult ManageUsers()
    {
      return View(_userManager.Users);
    }

    [HttpGet]
    public async Task<IActionResult> ManageUsersEdit(string id)
    {
      var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
      if (user == null)
      {
        throw new ApplicationException($"Unable to load user with ID '{id}'.");
      }

      var model = new UsersEditViewModel();
      model.User = user;
      model.Roles = new List<Tuple<IdentityRole, bool>>();
      foreach (var role in _roleManager.Roles)
      {
        model.Roles.Add(new Tuple<IdentityRole, bool>(role, await _userManager.IsInRoleAsync(user, role.Name)));
      }

      return View(model);
    }
  }
}