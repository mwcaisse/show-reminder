using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShowReminder.TVDBFetcher.Manager;
using ShowReminder.TVDBFetcher.Model;
using ShowReminder.TVDBFetcher.Model.Authentication;
using ShowReminder.TVDBFetcher.Model.Search;


// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowReminder.API.Controllers
{
    [Route("api/test")]
    public class TestController : Controller
    {

        private readonly AuthenticationParam _authenticationParam;

        private readonly SeriesManager _seriesManager;

        private readonly SearchManager _searchManager;

        public TestController(IOptions<AuthenticationParam> optionsAccessor)
        {
            _authenticationParam = optionsAccessor.Value;
            _seriesManager = new SeriesManager(_authenticationParam);
            _searchManager = new SearchManager(_authenticationParam);
        }
       
    }
}
