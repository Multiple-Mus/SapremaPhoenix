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

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/userposes")]
    [Authorize]
    public class UserPosesController : Controller
    {
        /// <summary>
        /// Return a list of all poses for a user
        /// </summary>
        /// <returns>JSON list of user poses</returns>
        [HttpGet(Name = "GetUserPoses")]
        public string GetUserPoses()
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var poses = new Get().GetAllUserPoses(userId);
            var serializedPoses = JsonConvert.SerializeObject(poses);

            return serializedPoses;
        }

        /// <summary>
        /// Omit a pose
        /// </summary>
        /// <param name="poseid"></param>
        /// <returns>Status code</returns>
        [HttpPost("{poseid}", Name = "OmitPose")]
        public IActionResult OmitPose(string poseid)
        {
            if (poseid == null)
            {
                return BadRequest();
            }

            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Create().OmitPose(poseid, userId);

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
        /// Delete omitted pose
        /// </summary>
        /// <param name="poseid">Pose ID</param>
        /// <returns>Status code</returns>
        [HttpDelete("{poseid}", Name = "IncludePose")]
        public IActionResult IncludePose(string poseid)
        {
            if (poseid == null)
            {
                return BadRequest();
            }
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Delete().IncludePose(poseid, userId);

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