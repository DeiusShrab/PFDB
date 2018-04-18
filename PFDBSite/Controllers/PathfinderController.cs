using Microsoft.AspNetCore.Mvc;

namespace PFDBSite.Controllers
{
  public class PathfinderController : Controller
  {
    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Maps()
    {
      return View();
    }
  }
}