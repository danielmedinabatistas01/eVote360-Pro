using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace eVote360_Pro.Helpers
{
    public static class IUploadFile
    {
        public static string? Upload(IFormFile file, string folderName, bool IsEditMode = false, string? currentImagePath = "")
        {
            // 1. Si estamos editando y el usuario no subió un archivo nuevo, mantenemos la foto actual
            if (IsEditMode && file == null)
            {
                return currentImagePath ?? string.Empty;
            }

            // 2. Definir rutas dentro de wwwroot
            string basePath = $"uploads/{folderName}";
            string absolutePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", basePath);

            // Crear el directorio si no existe
            if (!Directory.Exists(absolutePath))
            {
                Directory.CreateDirectory(absolutePath);
            }

            // 3. Generar un nombre único para el archivo para evitar colisiones
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string fileName = $"{guid}{fileInfo.Extension}";
            string fullFilePath = Path.Combine(absolutePath, fileName);

            // 4. Guardar el nuevo archivo físico en el servidor
            using (var stream = new FileStream(fullFilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // 5. Si había una foto anterior, la borramos para no llenar el servidor de basura
            if (!string.IsNullOrWhiteSpace(currentImagePath))
            {
                // Limpiamos los slashes para obtener solo el nombre del archivo real
                string oldFileName = Path.GetFileName(currentImagePath);
                string completeOldPath = Path.Combine(absolutePath, oldFileName);

                if (File.Exists(completeOldPath))
                {
                    File.Delete(completeOldPath);
                }
            }

            // Retornamos la ruta web relativa que se guardará en la base de datos (LogoUrl)
            return $"/{basePath}/{fileName}";
        }
    }
}
