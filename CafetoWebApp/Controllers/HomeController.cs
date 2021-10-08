using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CafetoWebApp.Models;

using CafetoTest;

namespace CafetoWebApp.Controllers
{
    public class HomeController : Controller
    {
        private event EventHandler ExceptionTestEventHappened;
        private readonly IJobLogger _logger;

        public HomeController(IJobLogger logger)
        {
            _logger = logger;

            _logger.LogMessage(LogMessageType.Info, "Cafeto Logger was successfully injected!");

            try
            {
                // NOTE: THIS EXCEPTION IS INTENTIONAL (For logging purposes)
                this.ExceptionTestEventHappened(this, null);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(LogMessageType.Error, "Your event has no subscribers!");
            }

            _logger.LogMessage(LogMessageType.Warning, "Always use JobLogger!");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
