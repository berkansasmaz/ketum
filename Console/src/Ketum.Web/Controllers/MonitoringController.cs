using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Ketum.Web.Controllers
{
    [Authorize]
    public class MonitoringController : Controller
    {
        public IActionResult Index(){
            return View();
        }

    }
}