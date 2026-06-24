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
    public class CandidatoConfiguration : IEntityTypeConfiguration<Candidato>
    {
        public void Configure(EntityTypeBuilder<Candidato> builder)
        {
            builder.ToTable("Candidatos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FotoUrl)
                .HasMaxLength(500);

            builder.Property(x => x.Estado)
                .IsRequired();

            builder.HasOne(x => x.PartidoPolitico)
    .WithMany(x => x.Candidatos)
    .HasForeignKey(x => x.PartidoPoliticoId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Asignaciones)
                .WithOne(x => x.Candidato)
                .HasForeignKey(x => x.CandidatoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
