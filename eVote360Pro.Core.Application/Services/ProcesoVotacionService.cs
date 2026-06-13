using eVote360Pro.Core.Application.Interfaces;

namespace eVote360Pro.Core.Application.Services
{
    public class ProcesoVotacionService
        : IProcesoVotacionService
    {
        private readonly IOcrService _ocrService;

        public ProcesoVotacionService(
            IOcrService ocrService)
        {
            _ocrService = ocrService;
        }

        public Task<bool> ValidarCedulaAsync(
            string numeroDocumento)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidarIdentidadOcrAsync(
            string numeroDocumento,
            string rutaImagen)
        {
            string? cedulaExtraida =
                await _ocrService.ExtraerCedulaAsync(rutaImagen);

            return cedulaExtraida == numeroDocumento;
        }

        public Task<string> GenerarCodigoAsync(
            int ciudadanoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarCodigoAsync(
            int ciudadanoId,
            string codigo)
        {
            throw new NotImplementedException();
        }
    }
}