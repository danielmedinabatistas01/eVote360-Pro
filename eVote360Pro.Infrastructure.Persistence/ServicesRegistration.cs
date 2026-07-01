using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.Services;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;

using Microsoft.Extensions.DependencyInjection;

namespace eVote360Pro.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayerIoc(
            this IServiceCollection services)
        {
            services.AddTransient<
                ICandidatoRepository,
                CandidatoRepository>();

            services.AddTransient<
                IAlianzaPoliticaRepository,
                AlianzaPoliticaRepository>();

            services.AddTransient<
                IAsignacionCandidatoRepository,
                AsignacionCandidatoRepository>();

            services.AddTransient<
                IAsignacionDirigenteRepository,
                AsignacionDirigenteRepository>();

            services.AddTransient<
                IPartidoPoliticoRepository,
                PartidoPoliticoRepository>();

            services.AddTransient<
                IPuestoElectivoRepository,
                PuestoElectivoRepository>();

            services.AddTransient<
                ICiudadanoRepository,
                CiudadanoRepository>();

            services.AddTransient<
                IEleccionRepository,
                EleccionRepository>();

            services.AddTransient<
                IVotoRepository,
                VotoRepository>();

            services.AddTransient<
                ICodigoVerificacionRepository,
                CodigoVerificacionRepository>();

            services.AddTransient<
                    IUsuarioRepository,
                    UsuarioRepository>();

            services.AddTransient<
                IEleccionPuestoElectivoRepository,
                EleccionPuestoElectivoRepository>();

            services.AddTransient<
                IVotoDetalleRepository,
                VotoDetalleRepository>();

            services.AddTransient<
                IParticipacionCiudadanoRepository,
                ParticipacionCiudadanoRepository>();

            //frank 
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<IEleccionRepository, EleccionRepository>();
            services.AddTransient<IVotoRepository, VotoRepository>();
            services.AddTransient<IEleccionPuestoElectivoRepository, EleccionPuestoElectivoRepository>();
            services.AddTransient<IVotoDetalleRepository, VotoDetalleRepository>();
            services.AddTransient<IAsignacionCandidatoRepository, AsignacionCandidatoRepository>();
            services.AddTransient<ICandidatoRepository, CandidatoRepository>();
            services.AddTransient<ICodigoVerificacionRepository, CodigoVerificacionRepository>();
            services.AddTransient<IPuestoElectivoRepository, PuestoElectivoRepository>();
            services.AddTransient<IAlianzaPoliticaRepository, AlianzaPoliticaRepository>();
            services.AddTransient<ICiudadanoRepository, CiudadanoRepository>();
            services.AddTransient<IPartidoPoliticoRepository, PartidoPoliticoRepository>();
            services.AddTransient<IAsignacionDirigenteRepository, AsignacionDirigenteRepository>();
        }
    }
}