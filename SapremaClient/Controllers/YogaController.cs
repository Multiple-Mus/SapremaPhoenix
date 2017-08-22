using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SapremaClient.Controllers
{
    public class YogaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Store()
        {
            return View();
        }
    }
}