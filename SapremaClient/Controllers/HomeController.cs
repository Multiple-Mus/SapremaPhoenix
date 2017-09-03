using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using SapremaClient.Models;
using Microsoft.AspNetCore.Http;
using IdentityModel;
using System.Net;

namespace SapremaClient.Controllers
{
    public class HomeController : Controller
    {
        public string sapremaAPI = "http://localhost:5001/api/";

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<string> GetData()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usermeditations/shopall/" + id;
            var content = await client.GetStringAsync(url);

            return content;
        }

        [Authorize]
        public async Task<string> GetShopList()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            //User.GetUserId();
            var content = await client.GetStringAsync("http://localhost:5001/api/meditation");


            return content;
        }

        [Authorize]
        public IActionResult Secure()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Cookies");
            await HttpContext.Authentication.SignOutAsync("oidc");
        }

        [Authorize]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Authorize]
        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            var tokenClient = new TokenClient("http://localhost:5000/connect/token", "mvc", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        [Authorize]
        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }
    }
}
