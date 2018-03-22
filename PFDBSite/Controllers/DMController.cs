using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PFDAL.Models;
using PFDBSite.Models;

namespace PFDBSite.Controllers
{
    public class DMController : Controller
    {
        private PFDBContext context = new PFDBContext();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bestiary()
        {
            var list = context.Bestiary.Select(x => new BestiaryListItem() {
                Name = x.Name
                , Cr = ParseCR(x.Cr ?? 0)
                , Ac = x.Ac
                , BestiaryId = x.BestiaryId
            }).ToList();
            return View("Bestiary/Index.aspx", new BestiaryListModel(list));
        }

        public IActionResult Bestiary(int bestiaryId)
        {
            var bestiary = context.Bestiary.FirstOrDefault(x => x.BestiaryId == bestiaryId);
            return View("Bestiary/Edit.aspx", new BestiaryDetailModel(bestiary));
        }

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
    }
}