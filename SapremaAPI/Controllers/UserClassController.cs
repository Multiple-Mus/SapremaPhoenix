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


    }
}