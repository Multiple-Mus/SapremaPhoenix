using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZeroFormatter;
using ZeroFormatter.Formatters;
using SapremaAPI.Models;
using System.Diagnostics;
using Newtonsoft.Json;
using SapremaAPI.Entities;
using SapremaAPI.DAL;

namespace SapremaAPI.Controllers
{
    //This controller is used solely for testing
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        /// <summary>
        /// Testing file size and serialisation time.
        /// </summary>
        [HttpGet]
        public string TestingFileType()
        {
            //return new string[] { "value1", "value2" };
            List<TestItem> testList = new List<TestItem>();
            int i = 0;
            while (i < 1000)
            {
                testList.Add(new TestItem()
                {
                    Name = "Seamus O'Higgins",
                    Style = "Anusara",
                    Price = 13.37,
                    Owned = true,
                    Rating = 3.141
                });
                i++;
            }

            Stopwatch timeToSerialize = new Stopwatch();
            timeToSerialize.Start();
            string serializedTestList = JsonConvert.SerializeObject(testList);
            // Total time to serialize
            timeToSerialize.Stop();
            return serializedTestList;
        }

        /// <summary>
        /// Testing database connection.
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>User details</returns>
        [HttpGet("{id}")]
        public string TestDBConnection(string id)
        {
            var user = new Get().GetUser(id);
            string seralizedUser = JsonConvert.SerializeObject(user);
            return seralizedUser;

        }

        // POST api/values
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public void Delete(int id)
        {
        }
    }
}
