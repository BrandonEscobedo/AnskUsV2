using anskus.Domain.Models;
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
        public virtual DbSet<Temp__sala_participante_cuestionario> SalaParticipante { get; set; }
        public virtual DbSet<CuestionarioActivo> cuestionarioActivo { get; set; }
        public AnskusDbContext(DbContextOptions<AnskusDbContext> options)
            :base(options)
        {
            

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Temp__sala_participante_cuestionario>()
                .HasKey(x=> new {x.code,x.name_user });
            builder.Entity<CuestionarioActivo>(entity =>
            {
                entity.HasKey(x => new { x.Codigo, x.IdUsuario });

                entity.HasOne(x => x.IdUsuarioNavigation)
                .WithOne(d => d.CuestionarioActivo)
                .HasForeignKey<CuestionarioActivo>(x => x.IdUsuario);              
            });
                
            builder.ApplyConfiguration(new UserConfiguration());
        }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    }
}
