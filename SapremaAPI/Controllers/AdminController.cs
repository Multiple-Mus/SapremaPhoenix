using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SapremaAPI.DAL;
using SapremaAPI.Entities;

namespace SapremaAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : Controller
    {
        /*
         * Teacher admionistration section
         * */

        /// <summary>
        /// Get all teachers
        /// </summary>
        /// <returns>List of all teachers</returns>
        [HttpGet("teachers", Name = "GetTeachers")]
        public string GetTeachersAdmin()
        {
            var teachersList = new Get().GetAllTeachersAdmin();
            var teachersListSerialized = JsonConvert.SerializeObject(teachersList);
            return teachersListSerialized;
        }

        /// <summary>
        /// Edit teachers verification status
        /// </summary>
        /// <param name="id">Teacher ID</param>
        /// <param name="status">Verification Status</param>
        [HttpPut("teachers/{id}/{status}", Name = "VerifyTeachers")]
        public bool UpdateVerificationStatus(string id, string status)
        {
            var success = new Update().UpdateTeacherVerification(id, status);
            return success;
        }

        /*
         * Content administration section
         * */

        /// <summary>
        /// Get all flagged items
        /// </summary>
        /// <returns>List of all flagged items</returns>
        [HttpGet("flaggeditem", Name = "GetFlaggedItems")]
        public string GetFlaggedItems()
        {
            var flaggedList = new Get().GetAllFlaggedItems();
            var serializedFlaggedList = JsonConvert.SerializeObject(flaggedList);
            return serializedFlaggedList;
        }

        /// <summary>
        /// Get a single flagged item
        /// </summary>
        /// <param name="id">Flag ID</param>
        /// <returns>Single flagged item</returns>
        [HttpGet("flaggeditem/{id}", Name = "GetFlaggedItem")]
        public string GetFlaggedItem(string id)
        {
            var flaggedItem = new Get().GetSingleFlaggedItem(id);
            var serializedFlaggedItem = JsonConvert.SerializeObject(flaggedItem);
            return serializedFlaggedItem;
        }

        /// <summary>
        /// Update flagged issue status
        /// </summary>
        /// <param name="id">Flag ID</param>
        /// <param name="status">Status of flag</param>
        [HttpPut("flaggeditem/{id}", Name = "ResolveFlag")]
        public bool ResolveFlag(string id, string status)
        {
            var success = new Update().ResolveFlaggedItem(id, status);
            return success;
        }

        /*
         * Meditation admninstration section
         * */


        [HttpGet("meditation", Name = "GetMeditationAdmin")]
        public string GetMeditationAdmin()
        {
            var meditationList = new Get().GetAllMeditations();
            var seralizedMeditationList = JsonConvert.SerializeObject(meditationList);
            return seralizedMeditationList;
        }

        /// <summary>
        /// Add a meditation
        /// </summary>
        /// <param name="value">JSON of meditation being uploaded</param>
        [HttpPost("meditation", Name = "CreateMeditation")]
        public bool CreateMeditation([FromBody] string value)
        {
            var meditation = JsonConvert.DeserializeObject<SapMeditations>(value);
            var success = new Create().CreateMeditation(meditation);
            return success;
        }

        [HttpGet("{id}")]
        public string GetSingleMeditation(string id)
        {
            var meditation = new Get().GetSingleMeditation(id);
            var serializedMeditation = JsonConvert.SerializeObject(meditation);
            return serializedMeditation;
        }

        /// <summary>
        /// Update a meditation
        /// </summary>
        /// <param name="value">JSON of meditation being updated</param>
        [HttpPut("meditation/{id}", Name = "UpdateMeditation")]
        public bool EditMeditation(Guid id, [FromBody] SapMeditations value)
        {
           // var meditation = JsonConvert.DeserializeObject<SapMeditations>(value);
            value.MeditationId = id;
            var success = new Update().UpdateMeditation(value);
            return success;
        }

        /// <summary>
        /// Delete a meditation
        /// </summary>
        /// <param name="id">Meditation ID</param>
        [HttpDelete("meditation/{id}", Name = "DeleteMeditation")]
        public bool DeleteMeditation(string id)
        {
            var success = new Delete().DeleteMeditation(id);
            return success;
        }
    }
}
