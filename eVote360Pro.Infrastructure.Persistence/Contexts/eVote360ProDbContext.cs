using eVote360Pro.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Infrastructure.Persistence.Contexts
{
    public class eVote360ProDbContext
    {
        public DbSet<Candidato> Candidatos { get; set; }

        public DbSet<AlianzaPolitica> AlianzasPoliticas { get; set; }

        public DbSet<AsignacionCandidato> AsignacionesCandidatos { get; set; }

        public DbSet<Voto> Votos { get; set; }

        public DbSet<CodigoVerificacion> CodigosVerificacion { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); //Liskov-substitution

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
