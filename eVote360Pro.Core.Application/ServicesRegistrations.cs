using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace eVote360Pro.Core.Application
{
    public static class ServicesRegistration
    {
        public static void AddApplicationLayerIoc(
            this IServiceCollection services)
        {
            services.AddAutoMapper(
                Assembly.GetExecutingAssembly());

            services.AddTransient<
                ICandidatoService,
                CandidatoService>();

            services.AddTransient<
                IAlianzaPoliticaService,
                AlianzaPoliticaService>();

            services.AddTransient<
                IAsignacionCandidatoService,
                AsignacionCandidatoService>();

            services.AddTransient<
                IVotoService,
                VotoService>();

            services.AddTransient<
                ICodigoVerificacionService,
                CodigoVerificacionService>();

            services.AddTransient<
                IProcesoVotacionService,
                ProcesoVotacionService>();

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