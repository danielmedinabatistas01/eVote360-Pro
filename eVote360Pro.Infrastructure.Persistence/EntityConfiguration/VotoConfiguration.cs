using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class VotoConfiguration : IEntityTypeConfiguration<Voto>
{
    public void Configure(EntityTypeBuilder<Voto> builder)
    {
        builder.ToTable("Votos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CiudadanoId)
            .IsRequired();

        builder.Property(x => x.EleccionId)
            .IsRequired();

        builder.Property(x => x.FechaVotacion)
            .IsRequired();

        builder.HasOne(x => x.Ciudadano)
            .WithMany()
            .HasForeignKey(x => x.CiudadanoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Eleccion)
            .WithMany(x => x.Votos)
            .HasForeignKey(x => x.EleccionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.VotoDetalles)
            .WithOne(x => x.Voto)
            .HasForeignKey(x => x.VotoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}