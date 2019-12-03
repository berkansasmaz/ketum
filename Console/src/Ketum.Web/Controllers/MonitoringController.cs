using System;
using System.Threading.Tasks;
using Ketum.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Web.Controllers
{
    public class MonitoringController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			var list = await Db.Monitors.ToListAsync();
            return Success(null, list);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]KTDMonitor value)
        {
			if(string.IsNullOrEmpty(value.Name))
				return Error("Name is required.");
			value.CreatedDate = DateTime.UtcNow;
			Db.Monitors.Add(value); 
			var result = await Db.SaveChangesAsync();
			if(result > 0)
            	return Success("Monitoring saved successfully");
			else
				return Error("Something is wrong with your model.");
        }
    }

    public class MonitoringModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}