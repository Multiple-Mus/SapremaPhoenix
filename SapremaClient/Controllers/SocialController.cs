using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SapremaClient.Models;

namespace SapremaClient.Controllers
{
    public class SocialController : Controller
    {
        public string sapremaAPI = "http://localhost:5001/api/";

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult ManageGroups()
        {
            return View();
        }



        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        [Authorize]
        public async Task<string> GetUserPoses()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "userposes/" + id;
            var content = await client.GetStringAsync(url);

            return content;
        }



        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateGroup([FromBody]string value)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();

            var list = JsonConvert.DeserializeObject<GroupModel>(value);


            var url = sapremaAPI + "usergroups/groups/" + id +
                "?groupname=" + list.GroupName +
                "&groupdescription=" + list.GroupDescription +
                "&groupstatus=" + list.GroupStatus +
                "&grouplevel=" + list.GroupLevel;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            var content = await client.PostAsync(url, stringContent);

            return content;
        }

        [Authorize]
        public async Task<HttpResponseMessage> OmitPose(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();


            var url = sapremaAPI + "userposes/" + itemId + "/" + id;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            var content = await client.PostAsync(url, stringContent);

            return content;
        }

        [Authorize]
        public async Task<bool> IncludePose(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "userposes/" + itemId + "/" + id;

            var content = await client.DeleteAsync(url);

            return true;
        }

        [Authorize]
        public async Task<bool> DeleteGroup(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usergroups/groups/" + itemId + "/" + id;

            var content = await client.DeleteAsync(url);

            return true;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> UpdateGroup(string itemId, [FromBody] string value)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();

            var list = JsonConvert.DeserializeObject<GroupModel>(value);


            var url = sapremaAPI + "usergroups/groups/" + list.GroupId +
                "?groupname=" + list.GroupName +
                "&groupdescription=" + list.GroupDescription +
                "&groupstatus=" + list.GroupStatus +
                "&grouplevel=" + list.GroupLevel +
                "&groupadmin=" + id;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            var content = await client.PutAsync(url, stringContent);

            return content;
        }
    }
}