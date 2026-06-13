using eVote360Pro.Core.Application.Dtos;
using eVote360Pro.Core.Application.Interfaces;
using eVote360Pro.Core.Domain.Entities;
using eVote360Pro.Core.Domain.Interfaces;
using eVote360Pro.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360Pro.Core.Application.Services
{
    public class CodigoVerificacionService : ICodigoVerificacionService
    {
        private readonly ICodigoVerificacionRepository _repository;

        public CodigoVerificacionService(
            ICodigoVerificacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CodigoVerificacionDto>> GetAllAsync()
        {
            var asignaciones = await _repository.GetAllList();

            return asignaciones.Select(x => new CodigoVerificacionDto
            {
                Id = x.Id,
                Codigo = x.Codigo,
                CiudadanoId = x.CiudadanoId,
                FechaExpiracion = x.FechaExpiracion,
                Utilizado = x.Utilizado



            }).ToList();
        }

        public async Task<CodigoVerificacionDto?> GetByIdAsync(int id)
        {
            var codigo = await _repository.GetById(id);

            if (codigo == null)
                return null;

            return new CodigoVerificacionDto
            {
                Id = codigo.Id,
                Codigo = codigo.Codigo,
                CiudadanoId = codigo.CiudadanoId,
                FechaExpiracion = codigo.FechaExpiracion,
                Utilizado = codigo.Utilizado
            };
        }

        public async Task AddAsync(CodigoVerificacionDto dto)
        {
            await _repository.AddAsync(new CodigoVerificacion
            {
                Id = dto.Id,
                Codigo = dto.Codigo,
                CiudadanoId = dto.CiudadanoId,
                FechaExpiracion = dto.FechaExpiracion,
                Utilizado = dto.Utilizado
            });
        }

        public async Task UpdateAsync(int id, CodigoVerificacionDto dto)
        {
            await _repository.UpdateAsync(id, new CodigoVerificacion
            {
                Id = id,
                Codigo = dto.Codigo,
                CiudadanoId = dto.CiudadanoId,
                FechaExpiracion = dto.FechaExpiracion,
                Utilizado = dto.Utilizado
            });
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<string> GenerarCodigoAsync(
    int ciudadanoId)
        {
            string codigo =
                Random.Shared.Next(100000, 999999)
                .ToString();

            await _repository.AddAsync(
                new CodigoVerificacion
                {
                    CiudadanoId = ciudadanoId,
                    Codigo = codigo,
                    FechaExpiracion =
                        DateTime.Now.AddMinutes(5),
                    Utilizado = false
                });

            return codigo;
        }

        public async Task<bool> ValidarCodigoAsync(
    int ciudadanoId,
    string codigo)
        {
            var entity =
                await _repository.GetCodigoAsync(
                    ciudadanoId,
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
                await _repository.GetById(codigoId);

            if (codigo == null)
                throw new Exception("Código no encontrado.");

            codigo.Utilizado = true;

            await _repository.UpdateAsync(
                codigoId,
                codigo);
        }

    }
}
