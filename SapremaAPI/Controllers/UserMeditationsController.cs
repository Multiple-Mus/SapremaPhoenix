using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using Microsoft.AspNetCore.Authorization;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/usermeditations")]
    [Authorize]
    public class UserMeditationsController : Controller
    {
        /// <summary>
        /// Get meditation store content
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>JSON object of all store items</returns>
        [HttpGet("shopall/{id}", Name = "GetUserMeditations")]
        public string GetShopMeditations(string id)
        {
            var meditations = new Get().GetUserMeditations(id);
            var serializedMeditations = JsonConvert.SerializeObject(meditations);
            return serializedMeditations;
        }

        ///// <summary>
        ///// Get all a users meditations
        ///// </summary>
        ///// <param name="id">USer ID</param>
        [HttpGet("meditationlist/{id}", Name = "GetUserListMeditations")]
        public string GetUserListMeditations(string id)
        {
            var meditations = new Get().GetUserListMeditations(id);
            var serializedMeditations = JsonConvert.SerializeObject(meditations);
            return serializedMeditations;
        }

        /// <summary>
        /// Delete purchased meditation
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="itemid">Meditation ID</param>
        /// <returns></returns>
        [HttpDelete("shopdelete/{userid}/{itemid}", Name = "DeleteUserMeditation")]
        public bool DeleteUserMeditation(string userId, string itemid)
        {
            var success = new Delete().DeleteUserMeditation(userId, itemid);
            return success;
        }

        /// <summary>
        /// Purchase a meditation
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <param name="itemid">Item ID</param>
        /// <returns></returns>
        [HttpPost("shopbuy/{userid}/{itemid}", Name = "BuyMeditation")]
        public bool BuyMeditation(string userId, string itemid)
        {
            var success = new Create().CreateBuyMeditation(userId, itemid);
            return success;
        }
    }
}