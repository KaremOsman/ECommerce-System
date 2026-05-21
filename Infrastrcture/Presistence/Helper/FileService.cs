using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services.Abstractions;

namespace Persistence.Helper
{
    public class FileService(IWebHostEnvironment _env) : IFileService
    {
        private readonly List<string> allowedExtensions = [".png", ".jpg", ".jpeg"];
        private const int maxSize = 5 * 1024 * 1024; // 5MB
        public async Task<string?> UploadFileAsync(IFormFile file, string folderName)
        {
            // 1.check if the file is null or empty
            if (file == null || file.Length == 0)
                return null;

            // 2. validate the file type and size
            var extension = Path.GetExtension(file.FileName)
                                .ToLowerInvariant()
                                .Trim();

            if (string.IsNullOrEmpty(extension)
                || file.Length > maxSize
                || !allowedExtensions.Contains(extension))
                return null;

            // 3. specify the folder path
            var relativePath = Path.Combine("Images", folderName);
            var folderPath = Path.Combine(_env.WebRootPath, "Data", relativePath);

            // 4. ensure the folder exists
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            // 4. generate a unique file name to avoid conflicts
            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(folderPath, fileName);


            // 6. save the file to the specified path
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine(relativePath, fileName).Replace("\\", "/");
        }

        public void DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return;

            var filePath = Path.Combine(
                _env.WebRootPath,
                "Data",
                fileName.TrimStart('/')
            );

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
