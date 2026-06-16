using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class AsignacionCandidatoConfiguration
        : IEntityTypeConfiguration<AsignacionCandidato>
    {
        public void Configure(
            EntityTypeBuilder<AsignacionCandidato> builder)
        {
            builder.ToTable("AsignacionesCandidatos");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Candidato)
                .WithMany(x => x.Asignaciones)
                .HasForeignKey(x => x.CandidatoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PuestoElectivo)
                .WithMany()
                .HasForeignKey(x => x.PuestoElectivoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Eleccion)
                .WithMany()
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}