using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowReminder.Web.Models;

namespace ShowReminder.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly DeploymentProperties _deploymentProperties;

        public HomeController(DeploymentProperties deploymentProperties)
        {
            _deploymentProperties = deploymentProperties;
        }

        public IActionResult Index()
        {

            ViewData["RootPathPrefix"] = _deploymentProperties.RootPathPrefix;
            ViewData["ApiUrl"] = _deploymentProperties.ApiUrl;

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
