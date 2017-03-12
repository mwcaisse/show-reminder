using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShowReminder.API.Manager;
using ShowReminder.API.Models;
using ShowReminder.API.ViewModel;
using ShowReminder.Data;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.API.Controllers
{

    [Route("api/show")]
    public class ShowController : Controller
    {

        private readonly ShowManager _showManager;
        private readonly ShowContext _showContext;

        public ShowController(IOptions<AuthenticationParam> optionsAccessor, ShowContext showContext)
        {
            _showManager = new ShowManager(optionsAccessor.Value);
            _showContext = showContext;
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

        [HttpGet]
        [Route("test")]
        public JsonResponse<List<Data.Entity.Show>> TestDatabase()
        {
            var shows = _showContext.Shows.ToList();
            return new JsonResponse<List<Data.Entity.Show>>()
            {
                Data = shows,
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("test/add/{showId}")]
        public JsonResponse<Data.Entity.Show> TestAddShow(int showId)
        {
            var show = _showManager.GetShowWithNextLast(showId);

            var showEntity = new Data.Entity.Show()
            {
                TvdbId = show.Id,
                Name = show.Name,
                FirstAiredDate = show.FirstAired,
                AirDay = show.AirsDayOfWeek,
                AirTime = show.AirsTime
            };

            if (null != show.LastEpisode)
            {
                showEntity.LastEpisode = new Data.Entity.Episode()
                {
                    OverallNumber = show.LastEpisode.OverallNumber,
                    AirDate = show.LastEpisode.AirDate,
                    Name = show.LastEpisode.Name,
                    EpisodeNumber = show.LastEpisode.EpisodeNumber,
                    SeasonNumber = show.LastEpisode.SeasonNumber,
                    Overview = show.LastEpisode.Overview
                };
            }

            if (null != show.NextEpisode)
            {
                showEntity.NextEpisode = new Data.Entity.Episode()
                {
                    OverallNumber = show.NextEpisode.OverallNumber,
                    AirDate = show.NextEpisode.AirDate,
                    Name = show.NextEpisode.Name,
                    EpisodeNumber = show.NextEpisode.EpisodeNumber,
                    SeasonNumber = show.NextEpisode.SeasonNumber,
                    Overview = show.NextEpisode.Overview
                };
            }

            _showContext.Shows.Add(showEntity);
            _showContext.SaveChanges();

            return new JsonResponse<Data.Entity.Show>()
            {
                Data = showEntity,
                ErrorMessage = null
            };

        }

    }
}
