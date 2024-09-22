using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Tenant.Application.Abstractions;

namespace Tenant.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;

        private readonly string _imageDirectory;

        public ImageService(IConfiguration configuration, ILogger<ImageService> logger)
        {
            _imageDirectory = configuration["ImageUrl"];
            _logger = logger;

            if (!Directory.Exists(_imageDirectory))
            {
                Directory.CreateDirectory(_imageDirectory);
            }
        }

        public async Task<byte[]> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError($"{nameof(ImageService)} : No file uploaded");
                throw new ArgumentException("No file uploaded.");
            }

            var filePath = Path.Combine(_imageDirectory, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<byte[]> GetImageAsync(string fileName)
        {
            var filePath = Path.Combine(_imageDirectory, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                _logger.LogError($"{nameof(ImageService)} : File not found");
                throw new FileNotFoundException();
            }

            return await File.ReadAllBytesAsync(filePath);
        }

    }
}
