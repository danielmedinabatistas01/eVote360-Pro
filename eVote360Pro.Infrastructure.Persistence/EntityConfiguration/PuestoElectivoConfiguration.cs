using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class PuestoElectivoConfiguration
        : IEntityTypeConfiguration<PuestoElectivo>
    {
        public void Configure(EntityTypeBuilder<PuestoElectivo> builder)
        {
            builder.ToTable("PuestosElectivos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Descripcion)
                .IsRequired();

            builder.Property(x => x.EsActivo)
                .IsRequired();

            builder.HasIndex(x => x.Nombre)
                .IsUnique();
        }
    }
}