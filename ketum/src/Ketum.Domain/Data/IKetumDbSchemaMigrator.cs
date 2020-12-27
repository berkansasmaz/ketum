using System.Threading.Tasks;

namespace Ketum.Data
{
    public interface IKetumDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
