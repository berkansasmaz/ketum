using System;
using System.Threading.Tasks;
using Ketum.Monitors;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Ketum.Controllers.Monitors
{
    [RemoteService(Name = KetumRemoteServiceConsts.RemoteServiceName)]
    [Area("ketum")]
    [Route("api/v1.0/public/ketum/monitor")]
    public class MonitorController : KetumController, IMonitorAppService
    {
        private readonly IMonitorAppService _monitorAppService;

        public MonitorController(IMonitorAppService monitorAppService)
        {
            _monitorAppService = monitorAppService;
        }

        [HttpGet]
        public async Task<PagedResultDto<MonitorDto>> GetListAsync(GetMonitorsRequestInput input)
        {
            var monitors = await _monitorAppService.GetListAsync(input);

            return monitors;
        }

        [HttpGet("{id}")]
        public async Task<MonitorWithDetailsDto> GetAsync(Guid id)
        {
            var monitor = await _monitorAppService.GetAsync(id);

            return monitor;
        }

        [HttpPost]
        public async Task CreateAsync(CreateMonitorDto input)
        {
            await _monitorAppService.CreateAsync(input);
        }

        [HttpPut("{id}")]
        public async Task UpdateAsync(Guid id, UpdateMonitorDto input)
        {
            await _monitorAppService.UpdateAsync(id, input);
        }

        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _monitorAppService.DeleteAsync(id);
        }
    }
}