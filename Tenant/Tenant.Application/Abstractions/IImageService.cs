using Microsoft.AspNetCore.Http;

namespace Tenant.Application.Abstractions
{
    public interface IImageService
    {
        Task<byte[]> UploadImageAsync(IFormFile file);
        Task<byte[]> GetImageAsync(string fileName);
    }
}
