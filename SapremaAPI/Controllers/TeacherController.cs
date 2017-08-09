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
    [Route("api/teacher")]
    public class TeacherController : Controller
    {
        /// <summary>
        /// Get all a teachers groups
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <returns>All teachers groups</returns>
        [HttpGet("groups/{id}", Name = "GetAllGroups")]
        public string GetTeacherGroups(string id)
        {
            var groups = new Get().GetTeacherGroups(id);
            var serializedGroups = JsonConvert.SerializeObject(groups);
            return serializedGroups;
        }

        /// <summary>
        /// Add a group
        /// </summary>
        /// <param name="value">JSON of group being added</param>
        [HttpPost("groups", Name = "CreateGroup")]
        public bool CreateGroup(string value)
        {
            var group = JsonConvert.DeserializeObject<SapGroups>(value);
            var success = new Create().CreateGroup(group);
            return success;
        }

        /// <summary>
        /// Update a group
        /// </summary>
        /// <param name="value">JSON of group being updated</param>
        [HttpPut("groups/{id}", Name = "UpdateGroup")]
        public bool UpdateGroup(string value)
        {
            var group = JsonConvert.DeserializeObject<SapGroups>(value);
            var success = new Update().UpdateGroup(group);
            return success;
        }
    }
}
