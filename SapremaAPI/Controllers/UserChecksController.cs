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
using Microsoft.AspNetCore.Authentication;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/userchecks")]
    public class UserChecksController : Controller
    {
        /// <summary>
        /// Check if user owns meditation
        /// </summary>
        /// <param name="itemId">Meditation ID</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("meditation/{itemid}", Name = "CheckOwnership")]
        public string CheckOwnership(string itemId)
        {
            var userId = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var success = new Get().GetOwnership(userId, itemId);
            if (success == true)
            {
                return "true";
            }

            else
            {
                return "false";
            }
        }
    }
}