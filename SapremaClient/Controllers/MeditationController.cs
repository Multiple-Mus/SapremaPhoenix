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

        public IActionResult BreathTimer()
        {
            return View();
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult UploadMeditation()
        {
            return View();
        }

        public IActionResult Meditations()
        {
            return View();
        }

        public IActionResult PlayMeditation()
        {
            return View();
        }
    }
}