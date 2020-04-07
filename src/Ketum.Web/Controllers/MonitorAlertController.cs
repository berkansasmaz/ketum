using Ketum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ketum.Entity;
using Newtonsoft.Json;

namespace Ketum.Web.Controllers
{

    public class MVMMonitorAlertSave
    {
        public Guid Id { get; set; }
        public Guid MonitorId { get; set; }
        public string Title { get; set; }
        public KTDMonitorAlertChannelTypes ChannelType { get; set; }
        public Dictionary<string, object> Settings { get; set; }
    }

    public class MonitorAlertController : ApiController
    {
        [NonAction]
        private object GetMonitorAlertClientModel(KTDMonitorAlert monitorAlert)
        {
            return new
            {
                monitorAlert.Title,
                monitorAlert.ChannelType,
                ChannelTypeText = monitorAlert.ChannelType.ToString(),
                monitorAlert.Settings
            };
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]Guid id)
        {
            if (id == Guid.Empty)
                return Error("Id is required.");

            var monitorAlert = await Db.MonitorAlerts.FirstOrDefaultAsync(x => x.MonitorAlertId == id);
            if (monitorAlert == null)
                return Error("Monitor alert not found.");

            var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == monitorAlert.MonitorId && x.UserId == UserId);
            if (monitor == null)
                return Error("Monitor not found!");

            return Success(null, GetMonitorAlertClientModel(monitorAlert));
        }

        [HttpGet("list/{id?}")]
        public async Task<IActionResult> List([FromRoute]Guid? id)
        {
            var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == id && x.UserId == UserId);
            if (monitor == null)
                return Error("Monitor not found!");

            var alerts = Db.MonitorAlerts.Where(x => x.MonitorId == monitor.MonitorId);
            var list = new List<dynamic>();
            foreach (var alert in alerts)
            {
                list.Add(GetMonitorAlertClientModel(alert));
            }
            return Success(null, list);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]MVMMonitorAlertSave value)
        {
            if (string.IsNullOrWhiteSpace(value.Title))
            {
                return Error("Title is required.");
            }

            if (value.MonitorId == Guid.Empty)
                return Error("Monitor Id is required.");

            var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == value.MonitorId && x.UserId == UserId);
            if (monitor == null)
                return Error("Monitor not found.");

            KTDMonitorAlert data = null;
            if (value.Id != Guid.Empty)
            {
                data = await Db.MonitorAlerts.FirstOrDefaultAsync(x => x.MonitorId == monitor.MonitorId && x.MonitorAlertId == value.Id);
                if (data == null) return Error("Monitor alert not found.");
            }
            else
            {
                if (!await CheckSubscription(UserId, "ALERT_CHANNEL"))
                    return Error("You don't have enough quota to do that.");

                data = new KTDMonitorAlert
                {
                    MonitorAlertId = Guid.NewGuid(),
                    MonitorId = monitor.MonitorId,
                    Title = value.Title,
                    ChannelType = value.ChannelType,
                    Settings = JsonConvert.SerializeObject(value.Settings)
                };
                await Db.AddAsync(data);
            }

            var result = await Db.SaveChangesAsync();
            if (result > 0)
                return Success("Monitor alert saved successfully.", new
                {
                    Id = data.MonitorAlertId
                });
            else
                return Error("Something is wrong with your model.");
        }

        [HttpGet("steps/{id}")]
        public async Task<IActionResult> Steps(Guid id)
        {
            var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == id && x.UserId == UserId);
            if (monitor == null)
                return Error("Monitor not found.");

            var steps = await Db.MonitorSteps.Where(x => x.MonitorId == monitor.MonitorId).ToListAsync();
            var list = steps.Select(x =>
                        new
                        {
                            x.MonitorStepId,
                            x.Interval,
                            x.Status,
                            StatusText = x.Status.ToString(),
                            x.LastCheckDate,
                            x.Type,
                            TypeText = x.Type.ToString()
                        }).ToList();

            return Success(null, list);
        }

        [HttpGet("logs/{id}")]
        public async Task<IActionResult> Logs(Guid id, [FromQuery] int page)
        {
            var monitorAlert = await Db.MonitorAlerts.FirstOrDefaultAsync(x => x.MonitorAlertId == id);
            if (monitorAlert == null)
                return Error("Monitor alert not found.");

            var monitor = await Db.Monitors.FirstOrDefaultAsync(x => x.MonitorId == monitorAlert.MonitorId && x.UserId == UserId);

            if (monitor == null)
                return Error("Monitor not found.");

            var itemCount = await Db.MonitorAlertLogs.CountAsync(x => x.MonitorAlertId == monitorAlert.MonitorAlertId);
            var perPageItem = 10;

            var currentPage = page;

            var logs = await Db.MonitorAlertLogs
                .Where(x => x.MonitorAlertId == monitorAlert.MonitorAlertId)
                .OrderByDescending(x => x.CreatedDate)
                .Skip(currentPage * perPageItem)
                .Take(perPageItem)
                .ToListAsync();

            var pagedResult = new KTReturnPagedData<dynamic>();
            pagedResult.ItemCount = itemCount;
            pagedResult.PageCount = (int)Math.Ceiling(itemCount / (decimal)perPageItem);
            pagedResult.CurrentPage = currentPage;

            pagedResult.Items = new List<dynamic>();

            foreach (var item in logs)
            {
                pagedResult.Items.Add(new
                {
                    item.MonitorAlertLogId,
                    item.Log,
                    item.CreatedDate,
                    item.UpdatedDate,
                    item.Status,
                    StatusText = item.Status.ToString()
                });
            }

            return Success(null, pagedResult);
        }
    }
}
