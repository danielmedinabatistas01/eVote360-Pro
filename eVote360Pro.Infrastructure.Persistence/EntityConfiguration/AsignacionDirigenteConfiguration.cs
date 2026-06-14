using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eVote360Pro.Infrastructure.Persistence.EntityConfiguration
{
    public class AsignacionDirigenteConfiguration : IEntityTypeConfiguration<AsignacionDirigente>
    {
        public void Configure(EntityTypeBuilder<AsignacionDirigente> builder)
        {
            builder.HasOne(x => x.Usuario)
       .WithOne(x => x.AsignacionDirigente)
       .HasForeignKey<AsignacionDirigente>(x => x.UsuarioId);

            builder.HasOne(x => x.PartidoPolitico)
                   .WithOne(x => x.AsignacionDirigente)
                   .HasForeignKey<AsignacionDirigente>(x => x.PartidoPoliticoId);
        }
    }
}
