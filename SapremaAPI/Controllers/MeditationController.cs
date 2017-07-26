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
    [Route("api/meditation")]
    public class MeditationController : Controller
    {
        /// <summary>
        /// Get all meditations
        /// </summary>
        /// <returns>List of all meditations</returns>
        [HttpGet]
        public string Get()
        {
            var meditationList = new Get().GetAllMeditations();
            var seralizedMeditationList = JsonConvert.SerializeObject(meditationList);
            return seralizedMeditationList;
        }

        /// <summary>
        /// Get single meditation
        /// </summary>
        /// <param name="id">Meditation Id</param>
        /// <returns>Details of a single meditation</returns>
        [HttpGet("{id}")]
        public string Get(string id)
        {
            var meditation = new Get().GetSingleMeditation(id);
            var serializedMeditation = JsonConvert.SerializeObject(meditation);
            return serializedMeditation;
        }

        /// <summary>
        /// Get reviews for a meditation
        /// </summary>
        /// <param name="id">Meditation ID</param>
        /// <returns>All reviews for a single meditation</returns>
        [HttpGet("{id}/reviews")]
        public string GetMeditationReviews(string id)
        {
            var meditationReviews = new Get().GetAllMeditationReviews(id);
            var meditationReviewsSerialized = JsonConvert.SerializeObject(meditationReviews);
            return meditationReviewsSerialized;
        }

        /// <summary>
        /// Add a meditation
        /// </summary>
        /// <param name="value">JSON of meditation being uploaded</param>
        [HttpPost]
        public void Post(string value)
        {
            var meditation = JsonConvert.DeserializeObject<SapMeditations>(value);
            var success = new Create().CreateMeditation(meditation);
        }
        
        /// <summary>
        /// Update a meditation
        /// </summary>
        /// <param name="value">JSON of meditation being updated</param>
        [HttpPut("{id}")]
        public void Put(string value)
        {
            var meditation = JsonConvert.DeserializeObject<SapMeditations>(value);
            var success = new Update().UpdateMeditation(meditation);
        }
        
        /// <summary>
        /// Delete a meditation
        /// </summary>
        /// <param name="id">Meditation ID</param>
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            var success = new Delete().DeleteMeditation(id);
        }
    }
}
