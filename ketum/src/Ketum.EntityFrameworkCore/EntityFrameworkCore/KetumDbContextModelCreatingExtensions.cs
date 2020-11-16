using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Ketum.EntityFrameworkCore
{
    public static class KetumDbContextModelCreatingExtensions
    {
        public static void ConfigureKetum(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(KetumConsts.DbTablePrefix + "YourEntities", KetumConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}