using System;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web.Controllers
{
    public class HomeController : SecureController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
