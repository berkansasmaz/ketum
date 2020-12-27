using Ketum.Monitors;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Ketum.EntityFrameworkCore
{
    public static class KetumDbContextModelCreatingExtensions
    {
        public static void ConfigureKetum(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            builder.Entity<Monitor>(b =>
            {
                b.ToTable(KetumConsts.DbTablePrefix + "Monitors", KetumConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(n => n.Name).HasMaxLength(MonitorConsts.MaxNameLength).IsRequired()
                    .HasColumnName(nameof(Monitor.Name));
                b.Property(n => n.MonitorStatus).HasColumnName(nameof(Monitor.MonitorStatus));

                b.HasIndex(n => new {n.TenantId, n.Name});

                b.HasOne(n => n.MonitorStep).WithOne().HasForeignKey<MonitorStep>(x => x.MonitorId).IsRequired();
            });

            builder.Entity<MonitorStep>(b =>
            {
                b.ToTable(KetumConsts.DbTablePrefix + "MonitorSteps", KetumConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(n => n.Url).HasMaxLength(MonitorStepConsts.MaxUrLength).IsRequired()
                    .HasColumnName(nameof(MonitorStep.Url));
                b.Property(n => n.Interval).HasDefaultValue(KetumConsts.MonitorWorkerPeriod.TotalMinutes)
                    .HasColumnName(nameof(MonitorStep.Interval));
                b.Property(n => n.Type).HasColumnName(nameof(MonitorStep.Type));
                b.Property(n => n.Status).HasColumnName(nameof(MonitorStep.Status));

                b.HasIndex(n => new {n.TenantId, n.Url});

                b.HasMany(n => n.MonitorStepLogs).WithOne().HasForeignKey(x => x.MonitorStepId).IsRequired();
            });

            builder.Entity<MonitorStepLog>(b =>
            {
                b.ToTable(KetumConsts.DbTablePrefix + "MonitorStepLogs", KetumConsts.DbSchema);
                b.ConfigureByConvention();

                b.Property(n => n.StartDate).HasColumnName(nameof(MonitorStepLog.StartDate));
                b.Property(n => n.EndDate).HasColumnName(nameof(MonitorStepLog.EndDate));
                b.Property(n => n.Status).HasColumnName(nameof(MonitorStepLog.Status));
                b.Property(n => n.Log).HasMaxLength(MonitorStepLogConsts.MaxLogLength)
                    .HasColumnName(nameof(MonitorStepLog.Log));
                b.Property(n => n.Interval).HasDefaultValue(KetumConsts.MonitorWorkerPeriod.TotalMinutes)
                    .HasColumnName(nameof(MonitorStep.Interval));

                b.HasIndex(n => new {n.TenantId});
            });
        }
    }
}