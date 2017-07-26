using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using SapremaAPI.Entities;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/MeditationReview")]
    public class MeditationReviewController : Controller
    {
        /// <summary>
        /// Get all users reviews
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>List of all meditation user has made</returns>
        [HttpGet("UserReviews/{id}", Name = "GetUserReviews")]
        public string GetUserReviews(string id)
        {
            var userReviews = new Get().GetAllUserMeditationReviews(id);
            var userReviewsSerialized = JsonConvert.SerializeObject(userReviews);
            return userReviewsSerialized;
        }

        /// <summary>
        /// Returns all meditations reviews
        /// </summary>
        /// <param name="id">Meditation ID</param>
        /// <returns>List of all reviews for a meditation</returns>
        [HttpGet("MeditationReviews/{id}", Name = "GetMweditationReviews")]
        /*
         * Ignored as moved into MeditationsController
         * */
        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetMeditationReviews(string id)
        {
            var meditationReviews = new Get().GetAllMeditationReviews(id);
            var meditationReviewsSerialized = JsonConvert.SerializeObject(meditationReviews);
            return meditationReviewsSerialized;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="meditationId"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{meditationid}", Name = "GetUserReview")]
        public string Get(string userId, string meditationId)
        {
            var meditationReview = new Get().GetSingleMeditationReview(userId, meditationId);
            var meditationReviewSerialised = JsonConvert.SerializeObject(meditationReview);
            return meditationReviewSerialised;
        }
        
        // POST: api/MeditationReview
        [HttpPost]
        public void Post(string value)
        {
            var meditationReview = JsonConvert.DeserializeObject<SapReviewMeditation>(value);
            var success = new Create().CreateMeditationReview(meditationReview);
        }
        
        // PUT: api/MeditationReview/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var success = new Delete().DeleteMeditationReview(id);
        }
    }
}
