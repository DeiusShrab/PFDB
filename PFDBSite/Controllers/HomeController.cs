﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
