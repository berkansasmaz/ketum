using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Ketum.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(KetumHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class KetumConsoleApiClientModule : AbpModule
    {
        
    }
}
