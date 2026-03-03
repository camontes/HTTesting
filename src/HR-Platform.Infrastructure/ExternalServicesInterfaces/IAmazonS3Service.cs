using Microsoft.AspNetCore.Http;

namespace HR_Platform.Infrastructure.ExternalServicesInterfaces;

public interface IAmazonS3Service
{
    Task<string> UploadFile(IFormFile fileToUpload, string bucketFolderName);
    Task<bool> DeleteFile(string fileToDelete);

    Task<MemoryStream> GetFile(string fileToDownload);
}
