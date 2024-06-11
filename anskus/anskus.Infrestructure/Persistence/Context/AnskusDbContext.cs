using anskus.Domain.Models.Authentication;
using anskus.Infrestructure.Persistence.Context.EntitysConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anskus.Infrestructure.Persistence.Context
{
    public partial class AnskusDbContext : IdentityDbContext<ApplicationUser>
    {
        public AnskusDbContext(DbContextOptions<AnskusDbContext> options)
            :base(options)
        {
            

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    }
}
