using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SapremaClient.Controllers
{
    public class MeditationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}