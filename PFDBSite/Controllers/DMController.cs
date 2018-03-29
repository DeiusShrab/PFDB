using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFDAL;
using PFDAL.Models;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
  [Route("DM")]
  public class DMController : PFDBController
  {
    public IPFDBContext context = PFDAL.PFDAL.GetContext(Configuration["WorkingEnvironment"]);

    #region HTTP Methods

    public IActionResult Index()
    {
      return View();
    }

    [Route("Bestiary")]
    public IActionResult Bestiary()
    {
      return View("BestiaryIndex.aspx", context.Bestiary);
      //var list = context.Bestiary.Select(x => new BestiaryListItem()
      //{
      //  Name = x.Name,
      //  Cr = ParseCR(x.Cr ?? 0),
      //  Ac = x.Ac,
      //  BestiaryId = x.BestiaryId
      //}).ToList();
      //return View("Bestiary/Index.aspx", new BestiaryListModel(list));
    }

    [Route("Bestiary/{bestiaryId:int}")]
    public IActionResult Bestiary(int bestiaryId)
    {
      var bestiary = context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId);
      if (bestiary != null)
        return View("BestiaryEdit.aspx", bestiary);

      return new NotFoundResult();
    }

    #endregion

    #region Private Methods

    private string ParseCR(int cr)
    {
      switch (cr)
      {
        case 0:
          return "1/2";
        case -1:
          return "1/3";
        case -2:
          return "1/4";
        case -3:
          return "1/6";
        case -4:
          return "1/8";
        default:
          return cr.ToString();
      }
    }

    #endregion
  }
}