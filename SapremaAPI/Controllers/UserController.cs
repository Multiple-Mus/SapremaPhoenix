using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using SapremaAPI.Entities;
using SapremaAPI.Models;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/user")]
    public class UserController : Controller
    {
        /// <summary>
        /// Get all user reviews
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>JSON object of all user reviews</returns>
        [HttpGet("{id}/reviews", Name = "GetAllUserReviews")]
        public string GetUserReviews(string id)
        {
            var userReviews = new Get().GetAllUserReviews(id);
            var userReviewsSerialized = JsonConvert.SerializeObject(userReviews);
            return userReviewsSerialized;
        }

        /// <summary>
        /// Get a single review from user
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="reviewId">Review ID</param>
        /// <returns></returns>
        [HttpGet("{id}/reviews/{itemid}" , Name = "GetSingleUserReview")]
        public string GetSinlgleUserReview(string userId, string reviewId)
        {
            var singleReview = new Get().GetSingleUserReview(userId, reviewId);
            var success = JsonConvert.SerializeObject(singleReview);
            return success;
        }

        /// <summary>
        /// Create a flagged item
        /// </summary>
        /// <param name="value">JSON of FlaggedModel object</param>
        [HttpPost("flagitem", Name = "FlagItem")]
        public bool CreateFlag(string value)
        {
            var flag = JsonConvert.DeserializeObject<FlaggedModel>(value);
            var success = new Create().CreateFlaggedItem(flag);
            return success;
        }
    }
}
