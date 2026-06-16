using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class PartidoPoliticoConfiguration : IEntityTypeConfiguration<PartidoPolitico>
    {
        public void Configure(EntityTypeBuilder<PartidoPolitico> builder)
        {
            builder.ToTable("PartidosPoliticos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nombre)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(x => x.Siglas)
                   .IsRequired()
                   .HasMaxLength(10);

            builder.HasIndex(x => x.Siglas)
                   .IsUnique();
        }
    }
}
