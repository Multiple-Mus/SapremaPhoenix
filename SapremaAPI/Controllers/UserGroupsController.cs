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

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/usergroups")]
    public class UserGroupsController : Controller
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
        /// <param name="id">User ID</param>
        /// <param name="value">JSON of group being added</param>
        [HttpPost("groups/{id}", Name = "CreateGroup")]
        public IActionResult CreateGroup(string id, string groupname, string groupdescription, string groupstatus, string grouplevel)
        {
            SapGroups group = new SapGroups()
            {
                GroupAdmin = id,
                GroupName = groupname,
                GroupDescription = groupdescription,
                GroupStatus = Convert.ToBoolean(groupstatus),
                GroupLevel = int.Parse(grouplevel)
            };

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
        public IActionResult UpdateGroup(Guid id, string groupname, string groupdescription, string groupstatus, string grouplevel, string groupadmin)
        {
            SapGroups group = new SapGroups()
            {
                GroupAdmin = groupadmin,
                GroupName = groupname,
                GroupDescription = groupdescription,
                GroupStatus = Convert.ToBoolean(groupstatus),
                GroupLevel = int.Parse(grouplevel),
                GroupId = id
            };

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

        /// <summary>
        /// Delete a group
        /// </summary>
        /// <param name="id">Group ID</param>
        [HttpDelete("groups/{itemid}/{userid}", Name = "DeleteGroup")]
        public IActionResult DeleteGroup(string itemid, string userid)
        {
            if (itemid == null)
            {
                return BadRequest();
            }

            var success = new Delete().DeleteGroup(itemid, userid);

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

        [HttpGet("classInfo/{id}", Name = "GetClassInfo")]
        public string GetClassData(string id)
        {
            var classData = new Get().GetClassData(id);
            if(classData != null)
            {
                return JsonConvert.SerializeObject(classData);
            }
            return "";
        }

        [HttpPost("groupClassRemove/{id}", Name = "RemoveGroupClass")]
        public void RemoveGroupClass(string id)
        {
            var isDelete = new Delete().DeleteGroupClass(id);
        }
    }
}
