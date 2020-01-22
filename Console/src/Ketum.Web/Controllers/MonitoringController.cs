using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ketum.Entity;
using Ketum.Web;
using Newtonsoft.Json;
using System.Linq;

namespace Monova.Web.Controllers
{
    public class MonitoringController : ApiController
    {
   		 [HttpGet("{id?}")]
        public async Task<IActionResult> Get([FromRoute]Guid? id)
        {
            if (id.HasValue)
            {
                if (id.Value == Guid.Empty)
                {
                    return Error("You must send monitor id to get.");
                }

                var monitor = await Db.Monitors.FirstOrDefaultAsync( x => x.MonitorId == id.Value && x.UserId == UserId);
                if (monitor == null)
                    return Error("Monitor not found.", code: 404);

				var url =string.Empty;
				var monitorStepRequest = await Db.MonitorSteps.FirstOrDefaultAsync( x=> x.MonitorId == monitor.MonitorId && x.Type == KTDMonitorStepTypes.Request);
				if (monitorStepRequest != null) //Herhangi bir monitorSteps varsa
				{
					var requestSettings = monitorStepRequest.SettingsAsRequest();
					if (requestSettings != null)
					{
						url = requestSettings.Url;
					}
				}
                return Success(data: new
                {
                    monitor.MonitorId,
                    monitor.CreatedDate,
                    monitor.LastCheckDate,
                    monitor.MonitorStatus,
                    monitor.Name,
                    monitor.TestStatus,
                    monitor.UpTime,
                    monitor.UpdatedDate,
					Url = url
                });
            }

            var list = await Db.Monitors.ToListAsync();
            return Success(null, list);
        }

        [HttpPost] 
        public async Task<IActionResult> Post([FromBody]KTMMonitorSave value)
        {
            if (string.IsNullOrEmpty(value.Name))
            {
                return Error("Name is required.");
            }

			var monitorCheck = await Db.Monitors.AnyAsync(x => x.MonitorId != value.Id && x.Name.Equals(value.Name) && x.UserId == UserId);
			if (monitorCheck)
			{
				return Error("This project name is already in use. Please choose a different name.");
			}
			KTDMonitor data = null;
			if (value.Id != Guid.Empty)
			{
				data = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == value.Id && x.UserId == UserId);
				if (data == null)
				{
					return Error("Monitor not found.");
				}
				data.UpdatedDate = DateTime.UtcNow;
				 data.Name = value.Name; 
	
			}else
			{
				data =  new KTDMonitor
            	{
					MonitorId = Guid.NewGuid(),
					CreatedDate = DateTime.UtcNow,
					Name = value.Name,
					UserId = UserId
         	   };
				Db.Monitors.Add(data);
			}
			var monitorStepData = new KTDSMonitorStepSettingsRequest{
				Url = value.Url
			};

				var step = await Db.MonitorSteps.FirstOrDefaultAsync( x=> x.MonitorId == data.MonitorId && x.Type == KTDMonitorStepTypes.Request);
				if (step != null) //Herhangi bir monitorSteps varsa
				{
					var requestSettings = step.SettingsAsRequest() ?? new KTDSMonitorStepSettingsRequest();
					 requestSettings.Url = value.Url;
					 step.Settings = JsonConvert.SerializeObject(requestSettings);
				}else
				{
					 step =  new KTDMonitorStep{
						MonitorStepId = Guid.NewGuid(),
						Type = KTDMonitorStepTypes.Request,
						MonitorId = data.MonitorId,
						Settings = JsonConvert.SerializeObject(monitorStepData)//Json' a çevirdik 
					};
					Db.MonitorSteps.Add(step);
				}

            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Monitoring saved successfully.", new
                {
                    Id = data.MonitorId
                });
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
