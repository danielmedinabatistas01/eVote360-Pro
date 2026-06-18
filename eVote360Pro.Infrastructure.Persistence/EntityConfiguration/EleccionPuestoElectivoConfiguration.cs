using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EleccionPuestoElectivoConfiguration
{
    public void Configure(EntityTypeBuilder<EleccionPuestoElectivo> builder)
    {
        builder.ToTable("EleccionPuestoElectivo");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Eleccion)
            .WithMany()
            .HasForeignKey(x => x.EleccionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.PuestoElectivo)
            .WithMany(x => x.Elecciones)
            .HasForeignKey(x => x.PuestoElectivoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}