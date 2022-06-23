using Microsoft.AspNetCore.Http;
using SocialPlatformBlazor.Interfaces;
using SocialPlatformBlazor.Shared;

namespace SocialPlatformBlazor.Services
{
    public class UploadFileService : IUploadFileService
    {
        private ICollection<string> imgExtensions = new HashSet<string>(new string[] {".img", ".png",".jpeg",".jpg" });

        public async Task<string> UploadFileAsync(IFormFile file, string path)
        {
            EnsurePathExists(path);
            var extension = Path.GetExtension(file.FileName);

            if (imgExtensions.Contains(Path.GetExtension(file.FileName)))
            {
                path = path + Guid.NewGuid().ToString() + extension;
            }
            else
            {
                path = path + file.FileName;
            }
            
            if (file.Length > 0)
            {
                using (var stream = File.Create(path))
                {
                    await file.CopyToAsync(stream);
                }

                return path;
            }
            return "";
        }

        private void EnsurePathExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
