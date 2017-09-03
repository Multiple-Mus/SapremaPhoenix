using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using SapremaAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace SapremaAPI.Controllers
{
    [Route("api/meditation")]
    public class MeditationController : Controller
    {
        /// <summary>
        /// Get list of all meditation themes
        /// </summary>
        /// <returns>JSON list of themes</returns>
        [HttpGet("themes")]
        public string GetMeditationThemes()
        {
            var themeList = new Get().GetMeditationThemes();
            var serializedThemeList = JsonConvert.SerializeObject(themeList);
            return serializedThemeList;
        }

        /// <summary>
        /// Get list of all meditation types
        /// </summary>
        /// <returns>JSON list of types</returns>
        [HttpGet("types")]
        public string GetMeditationTypes()
        {
            var typeList = new Get().GetMeditationTypes();
            var serializedTypeList = JsonConvert.SerializeObject(typeList);
            return serializedTypeList;
        }

        ///// <summary>
        ///// Get all meditations
        ///// </summary>
        ///// <returns>List of all meditations</returns>
        //[HttpGet]
        //[Authorize]
        //public string GetAllMeditations()
        //{
        //    var meditationList = new Get().GetAllMeditations();
        //    var seralizedMeditationList = JsonConvert.SerializeObject(meditationList);
        //    return seralizedMeditationList;
        //}

        /// <summary>
        /// Get single meditation
        /// </summary>
        /// <param name="id">Meditation Id</param>
        /// <returns>Details of a single meditation</returns>
        [HttpGet("{id}")]
        public string GetSingleMeditation(string id)
        {
            var meditation = new Get().GetSingleMeditation(id);
            var serializedMeditation = JsonConvert.SerializeObject(meditation);
            return serializedMeditation;
        }

        /// <summary>
        /// Get reviews for a meditation
        /// </summary>
        /// <param name="id">Meditation ID</param>
        /// <returns>All reviews for a single meditation</returns>
        [HttpGet("reviews/{id}", Name = "GetMeditationReviews")]
        public string GetMeditationReviews(string id)
        {
            var meditationReviews = new Get().GetAllMeditationReviews(id);
            var meditationReviewsSerialized = JsonConvert.SerializeObject(meditationReviews);
            return meditationReviewsSerialized;
        }


    }
}
