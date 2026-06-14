using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class AlianzaPoliticaConfiguration
        : IEntityTypeConfiguration<AlianzaPolitica>
    {
        public void Configure(
            EntityTypeBuilder<AlianzaPolitica> builder)
        {
            builder.ToTable("AlianzasPoliticas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Descripcion)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Estado)
                .IsRequired();
        }
    }
}