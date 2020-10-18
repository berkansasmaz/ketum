using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Ketum.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IKetumDbContext : IEfCoreDbContext
    {
    }
}