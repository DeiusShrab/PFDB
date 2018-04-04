using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
      return View("DMIndex");
    }

    [Route("Bestiary")]
    public IActionResult Bestiary(string sortOrder, string searchString)
    {
      //var bestiaryList = context.Bestiary.Select(x => new BestiaryListItem()
      //{
      //  Name = x.Name,
      //  CrDisplay = ParseCR(x.Cr ?? 0),
      //  Type = GetBestiaryType(x.BestiaryId),
      //  SubType = GetBestiarySubTypes(x.BestiaryId),
      //  Cr = x.Cr ?? 0,
      //  BestiaryId = x.BestiaryId
      //});
      var bestiaryList = new List<BestiaryListItem>()
      {
        new BestiaryListItem()
        {
          Name = "Testbeast",
          CrDisplay = "1",
          Type = "Test",
          SubType = "Test, Test",
          Cr = 1,
          BestiaryId = 1
        },
        new BestiaryListItem()
        {
          Name = "Testbeast2",
          CrDisplay = "3",
          Type = "Test",
          SubType = "Test, Test",
          Cr = 3,
          BestiaryId = 2
        },
        new BestiaryListItem()
        {
          Name = "Testbeast3",
          CrDisplay = "5",
          Type = "Test",
          SubType = "Test, Test",
          Cr = 5,
          BestiaryId = 3
        }
      }.OrderBy(x => x.Name); // TESTING

      // Make this more efficient
      // Also consider using a *prefix for searching by type
      if (!string.IsNullOrWhiteSpace(searchString))
      {
        bestiaryList = bestiaryList.Where(x => x.Name.ToLower().Contains(searchString.ToLower())
          || x.Type.ToLower().Contains(searchString.ToLower())
          || x.SubType.ToLower().Contains(searchString.ToLower())
          ).OrderBy(x => x.Name);
      }

      ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
      ViewBag.CrSortParm = sortOrder == "cr" ? "cr_desc" : "cr";
      ViewBag.TypeSortParm = sortOrder == "type" ? "type_desc" : "type";

      switch (sortOrder)
      {
        case "cr_desc":
          bestiaryList = bestiaryList.OrderByDescending(x => x.Cr);
          break;
        case "cr":
          bestiaryList = bestiaryList.OrderBy(x => x.Cr);
          break;
        case "type_desc":
          bestiaryList = bestiaryList.OrderByDescending(x => x.Type);
          break;
        case "type":
          bestiaryList = bestiaryList.OrderBy(x => x.Type);
          break;
        case "name_desc":
          bestiaryList = bestiaryList.OrderByDescending(x => x.Name);
          break;
      }

      return View("BestiaryIndex", bestiaryList);
    }

    [Route("Bestiary/{bestiaryId:int}")]
    public IActionResult BestiaryEdit(int bestiaryId)
    {
      //var bestiary = context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId);
      var bestiary = new Bestiary()
      {
        Name = "Testbeast",
        Hp = 10,
        Type = "Test",
        SubType = "Test, Test",
        Cr = 1,
        BestiaryId = 1
      }; // TESTING

      if (bestiary != null)
        return View("BestiaryEdit", bestiary);

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

    private string GetBestiaryType(int bestiaryId)
    {
      var ret = string.Empty;

      var bestiary = context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId);
      if (bestiary != null)
      {
        var type = context.BestiaryType.FirstOrDefault(x => x.BestiaryTypeId.ToString() == bestiary.Type);
        if (type != null)
        {
          ret = type.Name;
        }
      }

      return ret;
    }

    private string GetBestiarySubTypes(int bestiaryId)
    {
      var sb = new StringBuilder();

      var types = context.BestiarySubType.Where(x => x.BestiaryId == bestiaryId);
      if (types != null)
      {
        foreach (var item in types)
        {
          var type = context.BestiaryType.FirstOrDefault(x => x.BestiaryTypeId == item.BestiaryTypeId);
          if (type != null)
          {
            sb.Append(type.Name + ", ");
          }
        }
      }

      if (sb.Length > 0)
        sb.Remove(sb.Length - 2, 2);

      return sb.ToString();
    }

    #endregion
  }
}