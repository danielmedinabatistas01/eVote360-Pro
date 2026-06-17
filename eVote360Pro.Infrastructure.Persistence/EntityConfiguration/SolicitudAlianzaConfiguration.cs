using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class AlianzaPoliticaConfiguration
        : IEntityTypeConfiguration<AlianzaPolitica>
    {
        public void Configure(
            EntityTypeBuilder<AlianzaPolitica> builder)
        {
            builder.ToTable(
                "AlianzasPoliticas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Estado)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(
                x => x.FechaSolicitud)
                .IsRequired();

            builder.Property(
                x => x.Vigente)
                .IsRequired();

            builder.HasOne(x => x.PartidoOrigen)
                .WithMany(x => x.AlianzasEnviadas)
                .HasForeignKey(x => x.PartidoOrigenId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PartidoDestino)
                .WithMany(x => x.AlianzasRecibidas)
                .HasForeignKey(x => x.PartidoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}