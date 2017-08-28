using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;

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
    }
}
 