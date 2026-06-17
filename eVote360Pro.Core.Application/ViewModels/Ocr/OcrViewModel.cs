using Microsoft.AspNetCore.Http;


namespace eVote360Pro.Core.Application.ViewModels.Ocr
{
    public class OcrViewModel
    {
        public string Cedula { get; set; } = string.Empty;

        public IFormFile? ImagenCedula { get; set; }
    }
}
