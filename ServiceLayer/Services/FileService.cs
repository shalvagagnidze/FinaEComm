using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services;

public class FileService : IFileService
{

    private readonly IWebHostEnvironment _environment;
    private readonly IMemoryCache _cache;
    private readonly ILogger<FileService> _logger;

    public FileService(IWebHostEnvironment environment, IMemoryCache cache,ILogger<FileService> logger)
    {
        _environment = environment;
        _cache = cache;
        _logger = logger;
    }

    public async Task<List<string>> SaveFileAsync(List<IFormFile> imageFiles, string[] allowedFileExtensions)
    {
        if (imageFiles == null || !imageFiles.Any())
        {
            throw new ArgumentNullException(nameof(imageFiles));
        }

        var contentPath = _environment.ContentRootPath;
        var path = Path.Combine(contentPath, "Images");
        // path = "c://projects/ImageManipulation.Ap/uploads" ,not exactly, but something like that

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileNames = new List<string>();

        foreach (var imageFile in imageFiles)
        {
            if (imageFile.Length > 2097152)
            {
                throw new ArgumentException("File size should not exceed 2 MB");
            }
            var ext = Path.GetExtension(imageFile.FileName);
            if (!allowedFileExtensions.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed.");
            }

            // generate a unique filename
            var fileName = $"{Guid.NewGuid().ToString()}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);

            //fileNames.Add(fileName);

            fileNames.Add(fileNameWithPath);

            _cache.Set(fileName, fileNameWithPath, TimeSpan.FromMinutes(30));
            _logger.LogInformation($"Cached file path: {fileNameWithPath}");
        }

        return fileNames;

    }


    public void DeleteFile(string fileNameWithExtension)
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }
        var contentPath = _environment.ContentRootPath;
        var path = Path.Combine(contentPath, $"Images", fileNameWithExtension);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Invalid file path");
        }
        File.Delete(path);

        _cache.Remove(path);
    }
}
