using Microsoft.AspNetCore.Http;
using Tenant.Application.Abstractions;

namespace Tenant.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public Task<byte[]> GetImageToProductAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Görüntü dosyası boş olamaz.");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray(); 
            }
        }
    }
}
