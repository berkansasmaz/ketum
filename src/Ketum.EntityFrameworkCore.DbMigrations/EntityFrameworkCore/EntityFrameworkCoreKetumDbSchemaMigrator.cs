using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ketum.Data;
using Volo.Abp.DependencyInjection;

namespace Ketum.EntityFrameworkCore
{
    public class EntityFrameworkCoreKetumDbSchemaMigrator
        : IKetumDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreKetumDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the KetumMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<KetumMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}