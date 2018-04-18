using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
    }
}
