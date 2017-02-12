using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShowReminder.API.Manager;
using ShowReminder.API.Models;
using ShowReminder.API.ViewModel;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.API.Controllers
{

    [Route("api/show")]
    public class ShowController : Controller
    {

        private readonly ShowManager _showManager;

        public ShowController(IOptions<AuthenticationParam> optionsAccessor)
        {
            _showManager = new ShowManager(optionsAccessor.Value);
        }

        [HttpGet]
        [Route("search")]
        public JsonResponse<IEnumerable<Show>> Search(string terms)
        {
            return new JsonResponse<IEnumerable<Show>>()
            {
                Data = _showManager.Search(terms),
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResponse<Show> Get(int id)
        {
            return new JsonResponse<Show>()
            {
                Data = _showManager.GetShow(id),
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("{id}/episodes")]
        public JsonResponse<IEnumerable<Episode>> GetEpisodes(int id)
        {
            return new JsonResponse<IEnumerable<Episode>>()
            {
                Data = _showManager.GetAllEpisodesForShow(id),
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("{id}/nextlast")]
        public JsonResponse<ShowNextLast> GetWithNextLast(int id)
        {
            return new JsonResponse<ShowNextLast>()
            {
                Data = _showManager.GetShowWithNextLast(id),
                ErrorMessage = null
            };
        }

    }
}
