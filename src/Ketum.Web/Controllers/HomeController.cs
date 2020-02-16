using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ketum.Entity;

namespace Ketum.Web.Controllers
{
    public class HomeController : SecureController
    {
        public IActionResult Index()
        {
            System.Console.WriteLine("Hola!");
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}