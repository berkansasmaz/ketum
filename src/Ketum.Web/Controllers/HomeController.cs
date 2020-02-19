using System;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web.Controllers
{
    public class HomeController : SecureController
    {
        public IActionResult Index()
        {
            Console.WriteLine("Hola!");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
