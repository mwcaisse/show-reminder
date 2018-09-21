﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShowReminder.API.Manager;
using ShowReminder.API.Models;
using ShowReminder.API.ViewModel;


namespace ShowReminder.API.Controllers
{

    [Route("show")]
    public class ShowController : Controller
    {

        private readonly ShowManager _showManager;

        public ShowController(ShowManager showManager)
        {
            _showManager = showManager;
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
