using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OwlTin.Common.Utils;
using OwlTin.Common.ViewModels;
using ShowReminder.Web.Manager;
using ShowReminder.Web.ViewModel;
using ShowReminder.Data;
using ShowReminder.Data.Entity;
using ShowReminder.Web.Scheduler;
using ShowReminder.Web.Scheduler.Jobs;

namespace ShowReminder.Web.Controllers.Api
{
    
    [Route("api/show/tracked")]
    public class TrackedShowApiController : Controller
    {
        private readonly TrackedShowManager _trackedShowManager;
        private readonly QuartzScheduler _quartzScheduler;
        
        public TrackedShowApiController(TrackedShowManager trackedShowmanager, QuartzScheduler quartzScheduler)
        {
          
            _trackedShowManager = trackedShowmanager;
            this._quartzScheduler = quartzScheduler;
        }
        
        [HttpGet]
        [Route("")]
        public ListJsonResponse<TrackedShow>  GetAll(SortParam sort = null, Dictionary<string, string> filters = null)
        {
            if (null == sort.SortBy && !filters.Any())
            {
                return new ListJsonResponse<TrackedShow>()
                {
                    Data = _trackedShowManager.GetAllOrderedByAirDate(),
                    ErrorMessage = null
                };
            }
            return new ListJsonResponse<TrackedShow>()
            {
                Data = _trackedShowManager.GetAll(sort, filters.ConvertToFilterParams()),
                ErrorMessage = null
            };
        }

        [HttpGet]
        [Route("update")]
        public JsonResponse<bool> UpdateAll()
        {
            this._quartzScheduler.RunJobNow(typeof(UpdateExpiredShowsJob));

            return new JsonResponse<bool>()
            {
                Data = true,
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

        [HttpDelete]
        [Route("{id}")]
        public JsonResponse<bool> Delete(int id)
        {
            try
            {
                _trackedShowManager.Delete(id);
                return new JsonResponse<bool>()
                {
                    Data = true,
                    ErrorMessage = null
                };
            }
            catch (ManagerException e)
            {
                return new JsonResponse<bool>()
                {
                    Data = false,
                    ErrorMessage = e.Message
                };
            }
        }

       
    }
}
