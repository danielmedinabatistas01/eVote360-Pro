using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class VotoConfiguration: IEntityTypeConfiguration<Voto>
    {
        public void Configure(EntityTypeBuilder<Voto> builder)
        {
            builder.ToTable("Votos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.EleccionId)
                .IsRequired();

            builder.Property(x => x.CiudadanoId)
                .IsRequired();

            builder.Property(x => x.FechaVoto)
                .IsRequired();

            builder.HasOne(x => x.Eleccion)
                .WithMany(x => x.Votos)
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
