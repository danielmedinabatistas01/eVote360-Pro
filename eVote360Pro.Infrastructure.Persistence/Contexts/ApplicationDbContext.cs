using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eVote360Pro.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Eleccion> Elecciones { get; set; }
        public DbSet<EleccionPuestoElectivo> EleccionPuestoElectivos { get; set; }
        public DbSet<Voto> Votos { get; set; }
        public DbSet<VotoDetalle> VotoDetalles { get; set; }

        public DbSet<Candidato> Candidatos { get; set; }

        public DbSet<AlianzaPolitica> AlianzasPoliticas { get; set; }

        public DbSet<AsignacionCandidato> AsignacionesCandidatos { get; set; }

        public DbSet<CodigoVerificacion> CodigosVerificacion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}