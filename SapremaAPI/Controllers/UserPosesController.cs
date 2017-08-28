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
        /// <param name="id">User ID</param>
        /// <returns>JSON list of user poses</returns>
        [HttpGet("{id}", Name = "GetUserPoses")]
        public string GetUserPoses(string id)
        {
            var poses = new Get().GetAllUserPoses(id);
            var serializedPoses = JsonConvert.SerializeObject(poses);
            return serializedPoses;
        }

        [HttpPost("{poseid}/{userid}", Name = "OmitPose")]
        public bool OmitPose(string poseid, string userid)
        {
            var success = new Create().OmitPose(poseid, userid);
            return success;
        }

        [HttpDelete("{poseid}/{userid}", Name = "IncludePose")]
        public bool IncludePose(string poseid, string userid)
        {
            var success = new Delete().IncludePose(poseid, userid);
            return success;
        }
    }
}