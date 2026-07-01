using eVote360Pro.Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Interfaces
{
    public interface IAlianzaPoliticaService
     : IGenericService<AlianzaPoliticaDto>
    {
        Task<List<AlianzaPoliticaDto>>
            GetActivosAsync();

        Task CrearSolicitudAsync(
            int partidoOrigenId,
            int partidoDestinoId);

        Task AceptarSolicitudAsync(
            int alianzaId);

        Task RechazarSolicitudAsync(
            int alianzaId);

        Task EliminarSolicitudAsync(
            int alianzaId);

        Task EliminarAlianzaAsync(
            int alianzaId);

        Task<List<AlianzaPoliticaDto>> ObtenerSolicitudesPendientesAsync(int partidoId);

        Task<List<AlianzaPoliticaDto>> ObtenerSolicitudesRealizadasAsync(int partidoId);

        Task<List<AlianzaPoliticaDto>> ObtenerAlianzasVigentesAsync(int partidoId);
    }
}
