using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Ketum.EntityFrameworkCore
{
    [DependsOn(
        typeof(KetumEntityFrameworkCoreModule)
        )]
    public class KetumEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<KetumMigrationsDbContext>();
        }
    }
}
