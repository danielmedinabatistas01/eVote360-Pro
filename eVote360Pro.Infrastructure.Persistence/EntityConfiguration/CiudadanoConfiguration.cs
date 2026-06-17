using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class CiudadanoConfiguration : IEntityTypeConfiguration<Ciudadano>
    {
        public void Configure(EntityTypeBuilder<Ciudadano> builder)
        {
            builder.ToTable("Ciudadanos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NumeroIdentificacion)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.HasIndex(x => x.NumeroIdentificacion)
                   .IsUnique();
        }
    }
}
