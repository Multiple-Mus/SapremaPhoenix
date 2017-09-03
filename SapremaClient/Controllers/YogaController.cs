using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Text;

namespace SapremaClient.Controllers
{
    public class YogaController : Controller
    {
        public string sapremaAPI = "http://localhost:5001/api/";

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Store()
        {
            return View();
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult Practice()
        {
            return View();
        }

        [Authorize]
        [Authorize]
        public IActionResult Manage()
        {
            return View();
        }

        /*
         * Methods for authorized API actions
         * */

        [Authorize]
        public async Task<string> GetShopList()
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
        public async Task<string> GetUserPoses()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userposes/";
            var content = await client.GetStringAsync(url);

            return content;
        }

        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> SaveClass([FromBody] string value)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userclass";
            var stringContent = new StringContent(value, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(url, stringContent);

            return response;
        }

        [Authorize]
        public async Task<string> GetUserClasses()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userclass/user";
            var content = await client.GetStringAsync(url);

            return content;
        }

        [Authorize]
        public async Task<string> GetSubbedClasses()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var url = sapremaAPI + "userclass/subbed";
            var content = await client.GetStringAsync(url);

            return content;
        }
    }
}
 