﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace provider.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Server Up!");
        }
    }
}
