using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShowReminder.API.Manager;
using ShowReminder.API.ViewModel;
using ShowReminder.Data;
using ShowReminder.Data.Entity;
using ShowReminder.TVDBFetcher.Model.Authentication;

namespace ShowReminder.API.Controllers
{
    
    [Route("api/show/tracked")]
    public class TrackedShowController : Controller
    {
        private readonly TrackedShowManager _trackedShowManager;

        public TrackedShowController(ShowManager showManager, DataContext dataContext){
          
            _trackedShowManager = new TrackedShowManager(dataContext, showManager);
        }

        [HttpGet]
        [Route("")]
        public ListJsonResponse<TrackedShow>  GetAll()
        {
            return new ListJsonResponse<TrackedShow>()
            {
                Data = _trackedShowManager.GetAll(),
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("{id}")]
        public JsonResponse<TrackedShow> Get(long id)
        {
            return new JsonResponse<TrackedShow>()
            {
                Data = _trackedShowManager.Get(id),
                ErrorMessage = null
            };
        }

        [HttpPost]
        [Route("add/{showId}")]
        public JsonResponse<TrackedShow> Add(int showId)
        {
            try
            {
                var trackedShow = _trackedShowManager.Add(showId);
                return new JsonResponse<TrackedShow>()
                {
                    Data = trackedShow,
                    ErrorMessage = null
                };
            }
            catch (ManagerException e)
            {
                return new JsonResponse<TrackedShow>()
                {
                    Data = null,
                    ErrorMessage = e.Message
                };
            }
        }

       
    }
}
