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
    public class SolicitudAlianzaConfiguration : IEntityTypeConfiguration<AlianzaPolitica>
    {
        public void Configure(EntityTypeBuilder<AlianzaPolitica> builder)
        {
            builder.ToTable("SolicitudesAlianza");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FechaSolicitud)
                .IsRequired();

            builder.Property(x => x.Estado)
                .IsRequired();

            builder.HasOne(x => x.PartidoSolicitante)
                .WithMany()
                .HasForeignKey(x => x.PartidoSolicitanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.PartidoDestino)
                .WithMany()
                .HasForeignKey(x => x.PartidoDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
