using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ketum.Entity
{
    public class KTDBContext : IdentityDbContext<KTUser>
    {
        public KTDBContext(DbContextOptions options) : base(options)
        {
        }
    }
}
