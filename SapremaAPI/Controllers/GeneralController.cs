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
    [Route("api/general")]
    public class GeneralController : Controller
    {
        /// <summary>
        /// Get review type
        /// </summary>
        /// <param name="id">Review ID</param>
        /// <returns>Type of review</returns>
        //[HttpGet("reviewtype/{id}", Name = "GetReviewType")]
        //[ApiExplorerSettings(IgnoreApi = true)]
        //public string GetReviewType(string id)
        //{
        //    var reviewType = new Get().GetReviewType(id);
        //    return reviewType;
        //}
    }
}
