using Microsoft.AspNetCore.Http;

namespace Services.Abstractions
{
    public interface IFileService
    {
        Task<string?> UploadFileAsync(IFormFile file, string folderName);
        void DeleteFile(string fileName);
    }
}
