using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using Microsoft.AspNetCore.Authorization;
using SapremaAPI.Entities;
using SapremaAPI.Models;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/userclass")]
    [Authorize]
    public class UserClassController : Controller
    {
        [HttpPost(Name = "CreateClass")]
        public IActionResult CreateGroup([FromBody] ClassModel value)
        {
            if (value == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            value.ClassCreatedBy = userId;
            var success = new Create().CreateClass(value);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpGet("user", Name = "GetUserClasses")]
        public string GetUserClasses()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var classes = new Get().GetUserclasses(userId);
            var seralizedClasses = JsonConvert.SerializeObject(classes);
            return seralizedClasses;
        }

        [HttpGet("subbed", Name = "GetSubbedClasses")]
        public string GetSubbedClasses()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var classes = new Get().GetSubbedclasses(userId);
            var seralizedClasses = JsonConvert.SerializeObject(classes);
            return seralizedClasses;
        }

        [HttpGet("poses/{itemid}", Name = "GetClassPoses")]
        public string GetClassPoses(string itemid)
        {
            //var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var poses = new Get().GetClassPoses(itemid);
            var seralizedClasses = JsonConvert.SerializeObject(poses);
            return seralizedClasses;
        }

        [HttpGet("checkclass/{itemid}", Name = "CheckClass")]
        public string CheckClass(string itemid)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var result = new Get().CheckClass(userId, itemid);
            if (result == true)
            {
                return "true";
            }
            
            else
            {
                return "false";
            }
        }

        [HttpGet("review/{itemid}", Name = "GetClassReview")]
        public string GetClassReview(string itemid)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Get().GetSingleUserReview(userId, itemid);
            var serializedSuccess = JsonConvert.SerializeObject(success);
            return serializedSuccess;
        }

        [HttpPost("review/{itemid}", Name = "CreateClassReview")]
        public IActionResult CreateClassReview(string itemid, [FromBody] SapReviewClass review)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            review.UserId = userId;
            review.ClassId = Guid.Parse(itemid);
            var success = new Create().CreateClassReview(review);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPut("review/{itemid}", Name = "UpdateClassReview")]
        public IActionResult UpdateClassReview(string itemid, [FromBody] SapReviewClass review)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            review.UserId = userId;
            review.ClassId = Guid.Parse(itemid);
            var success = new Update().UpdateClassReview(review);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }
    }
}