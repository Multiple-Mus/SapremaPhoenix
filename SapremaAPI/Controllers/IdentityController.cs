using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SapremaAPI.Controllers
{
    //[produces("application/json")]
    //[route("api/identity")]
    //public class identitycontroller : controller
    //{
    //}

    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var identity = this.User.Identity as ClaimsIdentity;
            var roleClaims = identity.Claims.Where(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Select(x => x.Value).ToList();
            var nonRoleClaims = identity.Claims.Where(x => x.Type != ClaimsIdentity.DefaultRoleClaimType).Select(x => new { Type = x.Type, Value = x.Value }).ToList();
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}