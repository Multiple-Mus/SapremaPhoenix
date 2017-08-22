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
        /// <param name="id">User ID</param>
        /// <param name="itemid">Review ID</param>
        /// <returns></returns>
        [HttpGet("{id}/reviews/{itemid}" , Name = "GetSingleUserReview")]
        public string GetSinlgleUserReview(string id, string itemid)
        {
            var singleReview = new Get().GetSingleUserReview(id, itemid);
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

        /// <summary>
        /// Add a group
        /// </summary>
        /// <param name="value">JSON of group being added</param>
        [HttpPost("groups", Name = "CreateGroup")]
        public IActionResult CreateGroup([FromBody] string value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var group = JsonConvert.DeserializeObject<SapGroups>(value);
            var success = new Create().CreateGroup(group);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Update a group
        /// </summary>
        /// <param name="id">ID of group being edited</param>
        /// <param name="value">JSON of group being updated</param>
        [HttpPut("groups/{id}", Name = "UpdateGroup")]
        public IActionResult UpdateGroup(Guid id, [FromBody] string value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var group = JsonConvert.DeserializeObject<SapGroups>(value);
            group.GroupId = id;
            var success = new Update().UpdateGroup(group);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("groups/{id}", Name = "DeleteGroup")]
        public IActionResult DeleteGroup(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var success = new Delete().DeleteGroup(id);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get all a teachers groups
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns>All teachers groups</returns>
        [HttpGet("groups/{id}", Name = "GetAllGroups")]
        public string GetTeacherGroups(string id)
        {
            var groups = new Get().GetUserGroups(id);
            var serializedGroups = JsonConvert.SerializeObject(groups);
            return serializedGroups;
        }
    }
}
