using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Entity
{
    public class KTDBContext : IdentityDbContext<KTUser, IdentityRole<Guid>, Guid>
    {
        public KTDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<KTDMonitor> Monitors {get; set;}
        public DbSet<KTDMonitorStep> MonitorSteps { get; set; }
        public DbSet<KTDMonitorStepLog> MonitorStepLogs { get; set; }
    }
}
