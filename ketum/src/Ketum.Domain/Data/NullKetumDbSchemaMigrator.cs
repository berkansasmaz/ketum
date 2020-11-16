using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Ketum.Data
{
    /* This is used if database provider does't define
     * IKetumDbSchemaMigrator implementation.
     */
    public class NullKetumDbSchemaMigrator : IKetumDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}