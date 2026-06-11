using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class EleccionPuestoElectivoConfiguration: IEntityTypeConfiguration<EleccionPuestoElectivo>
    {
        //Revisar
        public void Configure(EntityTypeBuilder<EleccionPuestoElectivo> builder)
        {
            builder.ToTable("EleccionPuestoElectivo");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EleccionId)
                .IsRequired();
            builder.Property(x => x.PuestoElectivoId)
                .IsRequired();
            builder.HasOne(x => x.Eleccion)
                .WithMany()
                .HasForeignKey(epe => epe.EleccionId)
                .OnDelete(DeleteBehavior.Restrict);
            /* Agregar despues de Perla
            builder.HasOne(epe => epe.PuestoElectivo)
                .WithMany()
                .HasForeignKey(epe => epe.PuestoElectivoId)
                .OnDelete(DeleteBehavior.Cascade);
             */
        }
    }
}
