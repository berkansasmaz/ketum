using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ketum.Web.Controllers
{
    public class MonitoringController : ApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new
            {
                Success = true,
                Message = "Hi there!"
            });
        }

        [HttpPost]
        public IActionResult Post([FromBody]MonitoringModel value)
        {
            return Forbid();
            // return Json(new
            // {
            //     Success = true,
            //     Message = "I saved it.",
            //     Data = value
            // });
        }
    }

    public class MonitoringModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}