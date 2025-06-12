using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Toshi.Backend.Infraestructure.Services;

namespace Toshi.Backend.API.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly EncryptionService _encryptor;

        public UploadController(IWebHostEnvironment env, EncryptionService encryptor)
        {
            _env = env;
            _encryptor = encryptor;
        }

        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            if (file == null)
                throw new Exception("No se ha enviado archivo a procesar");

            var validExtensions = new string[] { "jpg", "jpeg", "png" };
            var extension = Path.GetExtension(file.FileName).ToLower();

            if (!validExtensions.Contains(extension.Replace(".", "")))
                throw new Exception("Solo se permiten imágenes con extensión .jpg, .jpeg o .png");

            var separator = Path.PathSeparator;
            var uploadPath = _env.ContentRootPath;
            var paths = new List<string> { "wwwroot", "temp", "images" };

            for (int i = 0; i < paths.Count; i++)
            {
                uploadPath = Path.Combine(uploadPath, paths[i]);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);
            }
            
            string fileName = string.Empty;
            string filePath = string.Empty;

            do
            {
                fileName = $"{Guid.NewGuid().ToString()}{extension}";
                filePath = Path.Combine(uploadPath, fileName);

            } while (!System.IO.File.Exists(filePath));

            using var stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return _encryptor.Encrypt($"{separator}{string.Join(separator, paths)}{separator}{fileName}");
        }
    }
}
