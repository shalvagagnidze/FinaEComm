using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Interfaces;

public interface IFileService
{
    Task<List<string>> SaveFileAsync(List<IFormFile> imageFiles, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension);
}
