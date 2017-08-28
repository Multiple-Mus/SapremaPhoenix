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
        [Authorize]
        [HttpGet("meditation/{userid}/{itemid}", Name = "CheckOwnership")]
        public string CheckOwnership(string userId, string itemId)
        {
            var success = new Get().GetOwnership(userId, itemId);
            return success;
        }
    }
}