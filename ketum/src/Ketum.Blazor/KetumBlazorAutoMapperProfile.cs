using AutoMapper;
using Ketum.Monitors;

namespace Ketum.Blazor
{
    public class KetumBlazorAutoMapperProfile : Profile
    {
        public KetumBlazorAutoMapperProfile()
        {
            CreateMap<MonitorDto, UpdateMonitorDto>()
                .ForMember(s => s.Url, c => c.MapFrom(m => m.MonitorStep.Url))
                .ForMember(s => s.Interval, c => c.MapFrom(m => m.MonitorStep.Interval));
        }
    }
}
