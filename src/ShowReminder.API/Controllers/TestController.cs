using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowReminder.TVDBFetcher.Manager;
using ShowReminder.TVDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowReminder.API.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {
        // GET: api/test
        [HttpGet]
        public SeriesData Get()
        {
            var seriesRequester = new SeriesManager();

            return seriesRequester.GetSeries(295515);
        }

        [HttpGet]
        [Route("login")]
        public string Login()
        {
            var manager = new AbstractManager();
            var param = new AuthenticationParam();
            return manager.Login(param);
        }
    
    }
}
