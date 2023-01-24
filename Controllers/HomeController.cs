﻿using Microsoft.AspNetCore.Mvc;
using projekt_v4._0.Models;
using System.Diagnostics;

namespace projekt_v4._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Porady()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
            // chodzmy na ryby 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
