using anskus.Domain.Models;
using anskus.Domain.Models.Authentication;
using anskus.Infrestructure.Persistence.Context.EntitysConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace anskus.Infrestructure.Persistence.Context
{
    public partial class AnskusDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public virtual DbSet<SalaParticipante> SalaParticipante { get; set; }
        public virtual DbSet<CuestionarioActivo> cuestionarioActivo { get; set; }
        public AnskusDbContext(DbContextOptions<AnskusDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<SalaParticipante>(entity =>
            {
                entity.HasKey(x => new { x.code, x.name_user });
                entity.Property(x => x.name_user)
                   .HasMaxLength(60);
                entity.HasOne(x => x.IdCuestionrioNavigation)
                .WithMany(x => x.SalaParticipantes)
                .HasForeignKey(x => x.IdCuestionario)
                .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<CuestionarioActivo>(entity =>
            {
                entity.HasKey(x => new { x.Idcuestionario });
                entity.HasOne(x => x.IdUsuarioNavigation)
                .WithOne(d => d.CuestionarioActivo)
                .HasForeignKey<CuestionarioActivo>(x => x.IdUsuario);
            });
            builder.ApplyConfiguration(new UserConfiguration());
        }
        public virtual DbSet<RefreshToken> RefreshToken { get; set; }

    }
}
