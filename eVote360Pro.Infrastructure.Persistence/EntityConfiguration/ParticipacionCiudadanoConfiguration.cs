using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class ParticipacionCiudadanoConfiguration : IEntityTypeConfiguration<ParticipacionCiudadano>
    {
        public void Configure(EntityTypeBuilder<ParticipacionCiudadano> builder)
        {
            builder.ToTable("ParticipacionCiudadanos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CiudadanoId)
                .IsRequired();

            builder.Property(x => x.EleccionId)
                .IsRequired();

            builder.Property(x => x.FechaVotacion)
                .IsRequired();

            builder.HasOne(x => x.Eleccion)
                .WithMany(x => x.Participaciones)
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Ciudadano)
                .WithMany(x => x.Participaciones)
                .HasForeignKey(x => x.CiudadanoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
