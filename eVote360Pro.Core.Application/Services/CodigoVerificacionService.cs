using AutoMapper;
using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class CodigoVerificacionService
        : GenericService<
            CodigoVerificacionDto,
            CodigoVerificacion>,
          ICodigoVerificacionService
    {
        private readonly ICodigoVerificacionRepository
            _codigoRepository;

        public CodigoVerificacionService(
            ICodigoVerificacionRepository codigoRepository,
            IMapper mapper)
            : base(codigoRepository, mapper)
        {
            _codigoRepository = codigoRepository;
        }

        public async Task<string> GenerarCodigoAsync(
            int ciudadanoId,
            int eleccionId)
        {
            string codigo =
                Random.Shared
                    .Next(100000, 999999)
                    .ToString();

            await _codigoRepository.AddAsync(
                new CodigoVerificacion
                {
                    CiudadanoId = ciudadanoId,
                    EleccionId = eleccionId,
                    Codigo = codigo,
                    FechaGeneracion = DateTime.Now,
                    FechaExpiracion =
                        DateTime.Now.AddMinutes(5),
                    Utilizado = false
                });

            return codigo;
        }

        public async Task<bool> ValidarCodigoAsync(
    int ciudadanoId,
    int eleccionId,
    string codigo)
        {
            var entity =
                await _codigoRepository
                    .GetCodigoAsync(
                        ciudadanoId,
                        eleccionId,
                        codigo);

            if (entity == null)
                return false;

            if (entity.Utilizado)
                return false;

            if (entity.FechaExpiracion < DateTime.Now)
                return false;

            return true;
        }

        public async Task MarcarComoUtilizadoAsync(
            int codigoId)
        {
            var codigo =
                await _codigoRepository
                    .GetById(codigoId);

            if (codigo == null)
                throw new Exception(
                    "Código no encontrado.");

            codigo.Utilizado = true;

            await _codigoRepository
                .UpdateAsync(
                    codigoId,
                    codigo);
        }
    }
}