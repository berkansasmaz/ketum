using Ketum.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Ketum.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(KetumEntityFrameworkCoreDbMigrationsModule),
        typeof(KetumApplicationContractsModule)
        )]
    public class KetumDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
