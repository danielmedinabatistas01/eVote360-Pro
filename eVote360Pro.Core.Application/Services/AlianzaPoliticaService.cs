using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;

namespace eVote360Pro.Core.Application.Services
{
    public class AlianzaPoliticaService
        : GenericService<AlianzaPoliticaDto, AlianzaPolitica>,
          IAlianzaPoliticaService
    {
        private readonly IAlianzaPoliticaRepository
            _alianzaRepository;

        private readonly IEleccionRepository
            _eleccionRepository;

        private readonly IPartidoPoliticoRepository
            _partidoRepository;

        public AlianzaPoliticaService(
            IAlianzaPoliticaRepository alianzaRepository,
                IEleccionRepository eleccionRepository,
                IPartidoPoliticoRepository partidoRepository,
            IMapper mapper)
            : base(alianzaRepository, mapper)
        {
            _alianzaRepository = alianzaRepository;

            _eleccionRepository = eleccionRepository;

            _partidoRepository = partidoRepository;
        }

        public async Task<List<AlianzaPoliticaDto>>
            GetActivosAsync()
        {
            var alianzas =
                await _alianzaRepository.GetActivosAsync();

            return _mapper.Map<List<AlianzaPoliticaDto>>
                (alianzas);
        }

        public async Task CrearSolicitudAsync(
    int partidoOrigenId,
    int partidoDestinoId)
        {
            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se pueden modificar alianzas políticas mientras exista una elección activa.");
            }

            if (partidoOrigenId ==
                partidoDestinoId)
            {
                throw new Exception(
                    "No puede crear una solicitud de alianza hacia su propio partido político.");
            }

            var partidoOrigen =
                await _partidoRepository
                    .GetById(partidoOrigenId);

            var partidoDestino =
                await _partidoRepository
                    .GetById(partidoDestinoId);

            if (partidoOrigen == null)
            {
                throw new Exception(
                    "Partido origen no encontrado.");
            }

            if (partidoDestino == null)
            {
                throw new Exception(
                    "Partido destino no encontrado.");
            }

            if (!partidoOrigen.EsActivo)
            {
                throw new Exception(
                    "El partido origen está inactivo.");
            }

            if (!partidoDestino.EsActivo)
            {
                throw new Exception(
                    "El partido destino está inactivo.");
            }

            if (await _alianzaRepository
                .ExisteAlianzaAsync(
                    partidoOrigenId,
                    partidoDestinoId))
            {
                throw new Exception(
                    "Ya existe una alianza vigente con este partido político.");
            }

            if (await _alianzaRepository
                .ExisteSolicitudPendienteAsync(
                    partidoOrigenId,
                    partidoDestinoId))
            {
                throw new Exception(
                    "Ya existe una solicitud pendiente.");
            }

            await _alianzaRepository
                .AddAsync(
                    new AlianzaPolitica
                    {
                        PartidoOrigenId =
                            partidoOrigenId,

                        PartidoDestinoId =
                            partidoDestinoId,

                        Estado =
                            "Pendiente",

                        FechaSolicitud =
                            DateTime.Now,

                        Vigente =
                            false
                    });
        }

        public async Task
    AceptarSolicitudAsync(
    int alianzaId)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(alianzaId);

            if (alianza == null)
            {
                throw new Exception(
                    "Solicitud no encontrada.");
            }

            if (alianza.Estado !=
                "Pendiente")
            {
                throw new Exception(
                    "La solicitud ya fue procesada.");
            }

            if (await _eleccionRepository.ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se pueden modificar alianzas políticas mientras exista una elección activa.");
            }

            alianza.Estado =
                "Aceptada";

            alianza.Vigente =
                true;

            alianza.FechaRespuesta =
                DateTime.Now;

            await _alianzaRepository
                .UpdateAsync(
                    alianza.Id,
                    alianza);
        }

        public async Task
        RechazarSolicitudAsync(
    int alianzaId)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(alianzaId);

            if (alianza == null)
            {
                throw new Exception(
                    "Solicitud no encontrada.");
            }

            if (alianza.Estado !=
                "Pendiente")
            {
                throw new Exception(
                    "La solicitud ya fue procesada.");
            }

            if (await _eleccionRepository.ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se pueden modificar alianzas políticas mientras exista una elección activa.");
            }

            alianza.Estado =
                "Rechazada";

            alianza.Vigente =
                false;

            alianza.FechaRespuesta =
                DateTime.Now;

            await _alianzaRepository
                .UpdateAsync(
                    alianza.Id,
                    alianza);
        }

        public async Task
    EliminarSolicitudAsync(
    int alianzaId)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(alianzaId);

            if (alianza == null)
            {
                throw new Exception(
                    "Solicitud no encontrada.");
            }

            if (alianza.Estado ==
                "Aceptada")
            {
                throw new Exception(
                    "No puede eliminar una solicitud aceptada.");
            }

            if (await _eleccionRepository.ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se pueden modificar alianzas políticas mientras exista una elección activa.");
            }

            await _alianzaRepository
                .DeleteAsync(
                    alianzaId);
        }

        public async Task
    EliminarAlianzaAsync(
    int alianzaId)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(alianzaId);

            /*bool tieneCandidatosAliados =
    await _asignacionRepository
        .ExisteAsignacionAliadaAsync(
            alianza.PartidoOrigenId,
            alianza.PartidoDestinoId);

            if (tieneCandidatosAliados)
            {
                throw new Exception(
                    "No se puede eliminar la alianza porque existen candidatos aliados asignados.");
            }*/

            if (alianza == null)
            {
                throw new Exception(
                    "Alianza no encontrada.");
            }

            if (!alianza.Vigente)
            {
                throw new Exception(
                    "La alianza no está vigente.");
            }

            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede eliminar una alianza mientras exista una elección activa.");
            }

            alianza.Vigente =
                false;

            await _alianzaRepository
                .UpdateAsync(
                    alianza.Id,
                    alianza);
        }


        public override async Task AddAsync(
    AlianzaPoliticaDto dto)
        {
            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se pueden crear alianzas políticas mientras exista una elección activa.");
            }

            await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(
    int id,
    AlianzaPoliticaDto dto)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(id);

            if (alianza == null)
            {
                throw new Exception(
                    "Alianza política no encontrada.");
            }

            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede editar una alianza política mientras exista una elección activa.");
            }

            await base.UpdateAsync(
                id,
                dto);
        }

        public override async Task DeleteAsync(
    int id)
        {
            var alianza =
                await _alianzaRepository
                    .GetById(id);

            if (alianza == null)
            {
                throw new Exception(
                    "Alianza política no encontrada.");
            }

            if (await _eleccionRepository
                .ExisteEleccionActivaAsync())
            {
                throw new Exception(
                    "No se puede eliminar una alianza política mientras exista una elección activa.");
            }

            await base.DeleteAsync(id);
        }

        public async Task<List<AlianzaPoliticaDto>>
    ObtenerSolicitudesPendientesAsync(
        int partidoId)
        {
            var data =
                await _alianzaRepository
                    .ObtenerSolicitudesPendientesAsync(partidoId);

            return _mapper.Map<
                List<AlianzaPoliticaDto>>
                (data);
        }

        public async Task<List<AlianzaPoliticaDto>>
            ObtenerSolicitudesRealizadasAsync(
                int partidoId)
        {
            var data =
                await _alianzaRepository
                    .ObtenerSolicitudesRealizadasAsync(partidoId);

            return _mapper.Map<
                List<AlianzaPoliticaDto>>
                (data);
        }

        public async Task<List<AlianzaPoliticaDto>>
            ObtenerAlianzasVigentesAsync(
                int partidoId)
        {
            var data =
                await _alianzaRepository
                    .ObtenerAlianzasVigentesAsync(partidoId);

            return _mapper.Map<
                List<AlianzaPoliticaDto>>
                (data);
        }
    }
}