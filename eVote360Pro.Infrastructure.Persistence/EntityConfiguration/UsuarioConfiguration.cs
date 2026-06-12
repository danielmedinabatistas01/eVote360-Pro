using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfigurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.ToTable("Usuarios");

            builder.Property(x => x.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Apellido)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.CorreoElectronico)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Contrasena)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.RolUsuario)
                .IsRequired();

            builder.Property(x => x.Estado)
                .IsRequired();

            builder.HasIndex(x => x.NombreUsuario)
                .IsUnique();

            builder.HasIndex(x => x.CorreoElectronico)
                .IsUnique();
        }
    }
}