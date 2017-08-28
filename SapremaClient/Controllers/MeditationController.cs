using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.AspNetCore.Razor.CodeGenerators;
using System.IO;

namespace SapremaClient.Controllers
{
    public class MeditationController : Controller
    {
        public string sapremaAPI = "http://localhost:5001/api/";

        /*
         * Methods to return views
         * */
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult BreathTimer()
        {
            return View();
        }

        [Authorize]
        public IActionResult Store()
        {
            return View();
        }

        [Authorize]
        public IActionResult UploadMeditation()
        {
            return View();
        }

        [Authorize]
        public IActionResult Meditations()
        {
            return View();
        }

        //[Authorize]
        //public IActionResult PlayMeditation()
        //{
        //    return View();
        //}

        /*
         * Methods for authorized API actions
         * */
         /*
          * Display the store for a user
          * */
        [Authorize]
        public async Task<string> GetShopList()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usermeditations/shopall/" + id;
            var content = await client.GetStringAsync(url);

            return content;
        }

        /*
         * purchase a meditation
         * */
        [Authorize]
        public async Task<HttpResponseMessage> BuyMeditation(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usermeditations/shopbuy/" + id + "/" + itemId;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage responce = await client.PostAsync(url, stringContent);

            return responce;
        }

        /*
         * Return a meditation
         * */
        [Authorize]
        public async Task<HttpResponseMessage> ReturnMeditation(string itemId)
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usermeditations/shopdelete/" + id + "/" + itemId;
            var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            HttpResponseMessage responce = await client.DeleteAsync(url);

            return responce;
        }

        /*
         * Get a list of meditations owned by a user
         * */
        [Authorize]
        public async Task<string> GetMeditationList()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "usermeditations/meditationlist/" + id;
            var content = await client.GetStringAsync(url);

            return content;
        }

        //[Authorize]
        //public async Task<HttpResponseMessage> UploadMeditationDetails([FromBody]string itemId)
        //{
        //    var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
        //    var client = new HttpClient();
        //    client.SetBearerToken(accessToken);
        //    var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
        //    var url = sapremaAPI + "admin/meditation";
        //    var stringContent = new StringContent(itemId, Encoding.UTF8, "text/json");
        //    HttpResponseMessage responce = await client.PostAsync(url, stringContent);

        //    return responce;
        //}


        [Authorize]
        public async Task<bool> UploadMeditationDetails()
        {
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

            if (accessToken != null)
            {
                return true;
            }
            else {
                return false;
            }
        }

        [Authorize]
        public async Task<ActionResult> PlayMeditation(string itemId)
        {
            //ActionResult
            var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var id = User.Claims.Where(a => a.Type == "sub").Select(b => b.Value).ToArray().First();
            var url = sapremaAPI + "userchecks/meditation/" + id + "/" + itemId;
            //var stringContent = new StringContent("", Encoding.UTF8, "application/json");
            var responce = await client.GetStringAsync(url);
            if (responce == "true")
            {
                //var fileName = itemId + ".jpg";

                var imagePath = "../Uploads/MeditationImages/";

                return base.File(imagePath, "image/jpg");
            }

            else
            {
                return new EmptyResult();
            }
        }

    }
}