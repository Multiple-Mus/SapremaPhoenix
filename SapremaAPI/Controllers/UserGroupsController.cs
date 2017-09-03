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
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/usergroups")]
    [Authorize]
    public class UserGroupsController : Controller
    {
        /// <summary>
        /// Create a group
        /// </summary>
        /// <param name="value">Group Model</param>
        /// <returns>Status Code</returns>
        [HttpPost("groups", Name = "CreateGroup")]
        public IActionResult CreateGroup([FromBody] SapGroups value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            value.GroupAdmin = userId;
            var success = new Create().CreateGroup(value);

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
        /// Delete a group
        /// </summary>
        /// <param name="itemid">ID of group being deleted</param>
        /// <returns>Status code</returns>
        [HttpDelete("groups/{itemid}", Name = "DeleteGroup")]
        public IActionResult DeleteGroup(string itemid)
        {
            if (itemid == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Delete().DeleteGroup(itemid, userId);

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
        /// Update Group
        /// </summary>
        /// <param name="value">Group model</param>
        /// <returns>Status Code</returns>
        [HttpPut("groups", Name = "UpdateGroup")]
        public IActionResult UpdateGroup([FromBody] SapGroups value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            value.GroupAdmin = userId;
            var success = new Update().UpdateGroup(value);

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
        /// <returns>All teachers groups</returns>
        [HttpGet("groups", Name = "GetUserGroups")]
        public string GetGroups()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var groups = new Get().GetUserGroups(userId);
            var serializedGroups = JsonConvert.SerializeObject(groups);
            return serializedGroups;
        }




















        ///// <summary>
        ///// Get all user reviews
        ///// </summary>
        ///// <param name="id">User ID</param>
        ///// <returns>JSON object of all user reviews</returns>
        //[HttpGet("{id}/reviews", Name = "GetAllUserReviews")]
        //public string GetUserReviews(string id)
        //{
        //    var userReviews = new Get().GetAllUserReviews(id);
        //    var userReviewsSerialized = JsonConvert.SerializeObject(userReviews);
        //    return userReviewsSerialized;
        //}

        ///// <summary>
        ///// Get a single review from user
        ///// </summary>
        ///// <param name="id">User ID</param>
        ///// <param name="itemid">Review ID</param>
        ///// <returns></returns>
        //[HttpGet("{id}/reviews/{itemid}", Name = "GetSingleUserReview")]
        //public string GetSinlgleUserReview(string id, string itemid)
        //{
        //    var singleReview = new Get().GetSingleUserReview(id, itemid);
        //    var success = JsonConvert.SerializeObject(singleReview);
        //    return success;
        //}

        ///// <summary>
        ///// Create a flagged item
        ///// </summary>
        ///// <param name="value">JSON of FlaggedModel object</param>
        //[HttpPost("flagitem", Name = "FlagItem")]
        //public bool CreateFlag(string value)
        //{
        //    var flag = JsonConvert.DeserializeObject<FlaggedModel>(value);
        //    var success = new Create().CreateFlaggedItem(flag);
        //    return success;
        //}

        //[HttpGet("classInfo/{id}", Name = "GetClassInfo")]
        //public string GetClassData(string id)
        //{
        //    var classData = new Get().GetClassData(id);
        //    if (classData != null)
        //    {
        //        return JsonConvert.SerializeObject(classData);
        //    }
        //    return "";
        //}

        //[HttpPost("groupClassRemove/{id}", Name = "RemoveGroupClass")]
        //public void RemoveGroupClass(string id)
        //{
        //    var isDelete = new Delete().DeleteGroupClass(id);
        //}
    }
}
