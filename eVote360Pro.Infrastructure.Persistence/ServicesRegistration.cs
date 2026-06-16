using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.Services;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using eVote360Pro.Infrastructure.Persistence.Shared;
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
                IVotoRepository,
                VotoRepository>();

            services.AddTransient<
                ICodigoVerificacionRepository,
                CodigoVerificacionRepository>();

            services.AddTransient<
                ICiudadanoRepository,
                CiudadanoRepository>();

            //services.AddTransient<
            //    IOcrService,
            //    OcrService>();

            services.AddTransient<
    IAsignacionDirigenteService,
    AsignacionDirigenteService>();

            services.AddTransient<
                IPartidoPoliticoService,
                PartidoPoliticoService>();

            services.AddTransient<
                IPuestoElectivoService,
                PuestoElectivoService>();

            services.AddTransient<
                ICiudadanoService,
                CiudadanoService>();

        }
    }
}