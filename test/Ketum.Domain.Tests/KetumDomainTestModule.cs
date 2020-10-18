using Ketum.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Ketum
{
    [DependsOn(
        typeof(KetumEntityFrameworkCoreTestModule)
    )]
    public class KetumDomainTestModule : AbpModule
    {
    }
}