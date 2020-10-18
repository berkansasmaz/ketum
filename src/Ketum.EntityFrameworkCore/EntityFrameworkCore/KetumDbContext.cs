using Ketum.Monitors;
using Microsoft.EntityFrameworkCore;
using Ketum.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;

namespace Ketum.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class KetumDbContext : AbpDbContext<KetumDbContext>, IKetumDbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DbSet<Monitor> Monitors { get; set; }

        public DbSet<MonitorStep> MonitorSteps { get; set; }

        public DbSet<MonitorStepLog> MonitorStepLogs { get; set; }


        public KetumDbContext(DbContextOptions<KetumDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix +
                          "Users"); //Sharing the same table "AbpUsers" with the IdentityUser

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the KetumEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureKetum method */

            builder.ConfigureKetum();
        }
    }
}