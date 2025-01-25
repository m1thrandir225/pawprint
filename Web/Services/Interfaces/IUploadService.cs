
namespace Web.Services.Interfaces;

public interface IUploadService {
    public Task<string> UploadFile(IFormFile file);
    public bool RemoveFile(string fileName);

    public Task<string> ReplaceFile(IFormFile file, string currentFileName);
}