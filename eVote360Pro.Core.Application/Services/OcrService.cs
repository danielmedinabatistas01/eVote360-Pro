using eVote360Pro.Core.Application.Interfaces;
using System.Text.RegularExpressions;
using Tesseract;

namespace eVote360Pro.Infrastructure.Shared.Services
{
    public class OcrService : IOcrService
    {
        public async Task<string> ExtraerTextoAsync(
            string rutaImagen)
        {
            return await Task.Run(() =>
            {
                using var engine =
                    new TesseractEngine(
                        "./tessdata",
                        "spa",
                        EngineMode.Default);

                using var image =
                    Pix.LoadFromFile(rutaImagen);

                using var page =
                    engine.Process(image);

                return page.GetText();
            });
        }

        public async Task<string?> ExtraerCedulaAsync(
            string rutaImagen)
        {
            string texto =
                await ExtraerTextoAsync(rutaImagen);

            texto = texto.Replace("-", " ");

            var match =
                Regex.Match(
                    texto,
                    @"\d{3}\s?\d{7}\s?\d");

            if (match.Success)
            {
                return match.Value
                    .Replace(" ", "");
            }

            return null;
        }
    }
}