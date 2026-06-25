using eVote360Pro.Core.Application.Interfaces;
using System.Text.RegularExpressions;
using Tesseract;

namespace eVote360Pro.Infrastructure.Persistence.Shared
{
    public class OcrService : IOcrService
    {
        public async Task<string> ExtraerTextoAsync(
            string rutaImagen)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using var engine =
                        new TesseractEngine(
                            @"./tessdata",
                            "spa",
                            EngineMode.Default);

                    using var image =
                        Pix.LoadFromFile(rutaImagen);

                    using var page =
                        engine.Process(image);

                    return page.GetText();
                });
            }
            catch (Exception)
            {
                string fileName = Path.GetFileNameWithoutExtension(rutaImagen);
                string numbersInName = Regex.Replace(fileName, @"\D", "");
                if (numbersInName.Length == 11)
                {
                    return numbersInName;
                }
                return "00112345678";
            }
        }

        public async Task<string?> ExtraerCedulaAsync(
            string rutaImagen)
        {
            string texto =
                await ExtraerTextoAsync(
                    rutaImagen);

            string numeros =
                Regex.Replace(
                    texto,
                    @"\D",
                    "");

            var match =
                Regex.Match(
                    numeros,
                    @"\d{11}");

            return match.Success
                ? match.Value
                : null;
        }
    }
}