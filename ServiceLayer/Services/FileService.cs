using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Services;

public class FileService(IWebHostEnvironment environment) : IFileService
{
    public async Task<List<string>> SaveFileAsync(List<IFormFile> imageFiles, string[] allowedFileExtensions)
    {
        if (imageFiles == null || !imageFiles.Any())
        {
            throw new ArgumentNullException(nameof(imageFiles));
        }

        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, "Images");
        // path = "c://projects/ImageManipulation.Ap/uploads" ,not exactly, but something like that

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var fileNames = new List<string>();
        // Check the allowed extenstions
        foreach(var imageFile in imageFiles)
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

            fileNames.Add(fileName);
        }
        
        return fileNames;
    }


    public void DeleteFile(string fileNameWithExtension)
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }
        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, $"Images", fileNameWithExtension);

        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"Invalid file path");
        }
        File.Delete(path);
    }
}
