using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class VotoDetalleConfiguration
        : IEntityTypeConfiguration<VotoDetalle>
    {
        public void Configure(EntityTypeBuilder<VotoDetalle> builder)
        {
            builder.ToTable("VotoDetalles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.VotoId)
                .IsRequired();

            builder.Property(x => x.PuestoElectivoId)
                .IsRequired();

            builder.HasOne(x => x.Voto)
                .WithMany(x => x.VotoDetalles)
                .HasForeignKey(x => x.VotoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}