using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class CodigoVerificacionConfiguration : IEntityTypeConfiguration<CodigoVerificacion>
    {
        public void Configure(EntityTypeBuilder<CodigoVerificacion> builder)
        {
            builder.ToTable("CodigosVerificacion");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(x => x.FechaGeneracion)
                .IsRequired();

            builder.Property(x => x.FechaExpiracion)
                .IsRequired();

            builder.Property(x => x.Utilizado)
                .IsRequired();

            builder.HasOne(x => x.Ciudadano)
                .WithMany()
                .HasForeignKey(x => x.CiudadanoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Eleccion)
                .WithMany()
                .HasForeignKey(x => x.EleccionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
