using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TVDBFetcher.Manager;
using TVDBFetcher.Model;

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
            var seriesRequester = new SeriesRequester();

            return seriesRequester.GetSeries(295515);
        }
    
    }
}
