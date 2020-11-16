using AutoMapper;
using Ketum.Monitors;

namespace Ketum
{
    public class KetumApplicationAutoMapperProfile : Profile
    {
        public KetumApplicationAutoMapperProfile()
        {
            CreateMap<Monitor, MonitorDto>();

            CreateMap<Monitor, MonitorWithDetailsDto>()
                .ForMember(s => s.MonitorStep, c => c.MapFrom(m => m.MonitorStep))
                .ForPath(s => s.MonitorStep.MonitorStepLogs, c => c.MapFrom(m => m.MonitorStep.MonitorStepLogs));

            CreateMap<MonitorStep, MonitorStepDto>()
                .ForMember(x => x.MonitorStepLogs, c => c.MapFrom(m => m.MonitorStepLogs));

            CreateMap<MonitorStepLog, MonitorStepLogDto>();
        }
    }
}
