using AutoMapper;
using Ketum.Monitors;

namespace Ketum
{
    public class KetumApplicationAutoMapperProfile : Profile
    {
        public KetumApplicationAutoMapperProfile()
        {
            CreateMap<Monitor, MonitorDto>();

            CreateMap<Monitor, MonitorWithDetailsDto>();
        }
    }
}
