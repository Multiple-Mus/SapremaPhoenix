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

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/usermeditations")]
    [Authorize]
    public class UserMeditationsController : Controller
    {
        /// <summary>
        /// Get store list of meditations
        /// </summary>
        /// <returns>List of meditations</returns>
        [HttpGet("shopall", Name = "GetUserMeditations")]
        public string GetShopMeditations()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var meditations = new Get().GetUserMeditations(userId);
            var serializedMeditations = JsonConvert.SerializeObject(meditations);
            return serializedMeditations;
        }

        /// <summary>
        /// Get list of users meditations
        /// </summary>
        /// <returns>List of meditations</returns>
        [HttpGet("meditationlist", Name = "GetUserListMeditations")]
        public string GetUserListMeditations()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var meditations = new Get().GetUserListMeditations(userId);
            var serializedMeditations = JsonConvert.SerializeObject(meditations);
            return serializedMeditations;
        }

        /// <summary>
        /// Delete purchased meditation
        /// </summary>
        /// <param name="itemid">Meditation ID</param>
        /// <returns>Status code</returns>
        [HttpDelete("shopdelete/{itemid}", Name = "DeleteUserMeditation")]
        public IActionResult DeleteUserMeditation(string itemid)
        {
            if (itemid == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Delete().DeleteUserMeditation(userId, itemid);

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
        /// Purchase a meditation
        /// </summary>
        /// <param name="itemid">Item ID</param>
        /// <returns>Status code</returns>
        [HttpPost("shopbuy/{itemid}", Name = "BuyMeditation")]
        public IActionResult BuyMeditation(string itemid)
        {
            if (itemid == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Create().CreateBuyMeditation(userId, itemid);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }

        }

        [HttpGet("review/{itemid}", Name = "GetReview")]
        public string GetReview(string itemid)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Get().GetSingleUserReview(userId, itemid);
            var serializedSuccess = JsonConvert.SerializeObject(success);
            return serializedSuccess;
        }

        [HttpPost("review/{itemid}", Name = "CreateReview")]
        public IActionResult CreateReview(string itemid, [FromBody] SapReviewMeditation review)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            review.UserId = userId;
            review.MeditationId = Guid.Parse(itemid);
            var success = new Create().CreateMeditationReview(review);

            if (success == true)
            {
                return Ok();
            }

            else
            {
                return BadRequest();
            }
        }

        [HttpPut("review/{itemid}", Name = "UpdateReview")]
        public IActionResult UpdateReview(string itemid, [FromBody] SapReviewMeditation review)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            review.UserId = userId;
            review.MeditationId = Guid.Parse(itemid);
            var success = new Update().UpdateMeditationReview(review);

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