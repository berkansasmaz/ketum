using Volo.Abp.Application.Dtos;

namespace Ketum.Monitors
{
    public class GetMonitorsRequestInput : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }
    }
}