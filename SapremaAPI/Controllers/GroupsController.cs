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

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        /// <summary>
        /// Get a single group
        /// </summary>
        /// <param name="id">Group ID</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSingleGroup")]
        public string GetSingleGroup(string id)
        {
            var group = new Get().GetSingleGroup(id);
            var serializedGroup = JsonConvert.SerializeObject(group);
            return serializedGroup;
        }
    }
}