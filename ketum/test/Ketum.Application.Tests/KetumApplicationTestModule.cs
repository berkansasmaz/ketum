using Volo.Abp.Modularity;

namespace Ketum
{
    [DependsOn(
        typeof(KetumApplicationModule),
        typeof(KetumDomainTestModule)
        )]
    public class KetumApplicationTestModule : AbpModule
    {

    }
}