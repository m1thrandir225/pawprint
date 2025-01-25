
using Web.Services.Interfaces;

public class UploadService : IUploadService
{
    private string createFileName(IFormFile file)
    {
        return $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
    }

    private string createFilePath(string fileName)
    {
        return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);
    }
    private async Task<string> UploadFileBase(IFormFile file, string fileName, string filePath)
    {
      
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        return fileName;
    }

    public async Task<string> UploadFile(IFormFile file)
    {
        var fileName = createFileName(file);
        var filePath = createFilePath(fileName);

        var uploadedFileName = await UploadFileBase(file, fileName, filePath);

        return uploadedFileName;
    }

    public bool RemoveFile(string fileName)
    {
        var filePath = createFilePath(fileName);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }

        return false;
    }

    public async Task<string> ReplaceFile(IFormFile file, string currentFileName)
    {
        var fileDeleted = RemoveFile(currentFileName);

        if(!fileDeleted)
        {
            throw new Exception("File not found");
        }
     
        var fileName = createFileName(file);
        var filePath = createFilePath(fileName);

        var uploadedFileName = await UploadFileBase(file, fileName, filePath);

        return uploadedFileName;


    }
}