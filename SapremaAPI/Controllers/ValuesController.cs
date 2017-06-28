using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            List<YogaPose> poseList = new List<YogaPose>();
            int i = 0;
            while (i < 1000)
            {
                poseList.Add(new YogaPose()
                {
                    PoseId = 123456789,
                    PoseName = "pose name"
                });
                i++;
            }
            string serializedList = JsonConvert.SerializeObject(poseList);
            return serializedList;
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class YogaPose
    {
        public long PoseId { get; set; }
        public string PoseName { get; set; }
    }
}
