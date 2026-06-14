using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class EleccionConfiguration: IEntityTypeConfiguration<Eleccion>
    {
       public void Configure(EntityTypeBuilder<Eleccion> builder)
        {
            builder.ToTable("Elecciones");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(e => e.FechaRealizacion)
                .IsRequired();
            builder.Property(e => e.EstadoEleccion)
                .IsRequired();

            builder.HasMany(e => e.PuestosElectivos)
                .WithOne(pe => pe.Eleccion)

                .HasForeignKey(pe => pe.EleccionId);
            builder.HasMany(e => e.Votos)
                .WithOne(v => v.Eleccion)
                .HasForeignKey(v => v.EleccionId);

        }
    }
}
