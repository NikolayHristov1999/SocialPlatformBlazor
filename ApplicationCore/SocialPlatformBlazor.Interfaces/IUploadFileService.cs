
using Microsoft.AspNetCore.Http;

namespace SocialPlatformBlazor.Interfaces
{
    public interface IUploadFileService
    {
        public Task<string> UploadFileAsync(IFormFile file, string path);
    }
}
