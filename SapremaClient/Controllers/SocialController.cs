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
        public string sapremaAPI = "http://localhost:5001/api/"; // URL of SapremaAPI

        /*
         * Methods to return views
         * */

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
        public IActionResult Search()
        {
            return View();
        }

        /*
         * Methods dealing with group creation
         * */

        [Authorize]
        public async Task<string> GetStoreList()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "usergroups/store";
            var content = await client.GetStringAsync(url);

            return content;
        }

        [HttpGet]
        [Authorize]
        public async Task<string> GetUserGroups(string id)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token"); // Get user access token
            var client = new HttpClient(); // Created new Http Client instance
            client.SetBearerToken(accessToken); // Add token to this instance
            var url = sapremaAPI + "usergroups/groups"; // Build API URL
            var response = await client.GetStringAsync(url); // Contact APi with token

            return response;
        }
   
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> CreateGroup([FromBody] string value)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token"); // Get user access token
            var client = new HttpClient(); // Created new Http Client instance
            client.SetBearerToken(accessToken); // Add token to this instance
            var url = sapremaAPI + "usergroups/groups/"; // Build API URL
            var stringContent = new StringContent(value, Encoding.UTF8, "application/json"); // Set as Http request parameter
            HttpResponseMessage response = await client.PostAsync(url, stringContent); // Contact APi with token and Http content

            return response;
        }

        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> JoinGroup(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token"); // Get user access token
            var client = new HttpClient(); // Created new Http Client instance
            client.SetBearerToken(accessToken); // Add token to this instance
            var url = sapremaAPI + "usergroups/join/" + itemId; // Build API URL
            var stringContent = new StringContent("", Encoding.UTF8, "application/json"); // Set as Http request parameter
            HttpResponseMessage response = await client.PostAsync(url, stringContent); // Contact APi with token and Http content

            return response;
        }

        [HttpDelete]
        [Authorize]
        public async Task<HttpResponseMessage> LeaveGroup(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token"); // Get user access token
            var client = new HttpClient(); // Created new Http Client instance
            client.SetBearerToken(accessToken); // Add token to this instance
            var url = sapremaAPI + "usergroups/leave/" + itemId; // Build API URL
            HttpResponseMessage response = await client.DeleteAsync(url);

            return response;
        }

        [HttpDelete]
        [Authorize]
        public async Task<HttpResponseMessage> DeleteGroup(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "usergroups/groups/" + itemId;
            HttpResponseMessage response = await client.DeleteAsync(url);

            return response;
        }

        [HttpPut]
        [Authorize]
        public async Task<HttpResponseMessage> UpdateGroup([FromBody] string value)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "usergroups/groups";
            var stringContent = new StringContent(value, Encoding.UTF8, "application/json");
            var content = await client.PutAsync(url, stringContent);

            return content;
        }

        /*
         * Methods dealing with profile 
         * */

        [Authorize]
        public async Task<string> GetUserPoses()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userposes";
            var content = await client.GetStringAsync(url);

            return content;
        }
        
        [HttpPost]
        [Authorize]
        public async Task<HttpResponseMessage> OmitPose(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userposes/" + itemId;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            var content = await client.PostAsync(url, stringContent);

            return content;
        }

        [HttpDelete]
        [Authorize]
        public async Task<bool> IncludePose(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userposes/" + itemId;
            var content = await client.DeleteAsync(url);

            return true;
        }
    }
}