using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Infrastructure.ExternalServicesInterfaces;
using HR_Platform.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HR_Platform.Infrastructure.ExternalServices;

public class AmazonS3Service(
    ISecretsManagerAmazonService secretsManagerAmazonService,

    IConfiguration configuration
    ) : IAmazonS3Service
{
    private readonly ISecretsManagerAmazonService _secretsManagerAmazonService = secretsManagerAmazonService;

    private readonly IConfiguration _configuration = configuration;

    public async Task<string> UploadFile(IFormFile fileToUpload, string bucketFolderName)
    {
        try
        {
            string currentPath = Directory.GetCurrentDirectory();
            string identifier;

            string folderPath = Path.GetFullPath(currentPath);
            Directory.CreateDirectory(folderPath);
            string path = Path.GetFullPath(Path.Combine(folderPath, fileToUpload.FileName));

            using FileStream fileStream = new(path, FileMode.Create, FileAccess.ReadWrite);
            fileToUpload.CopyTo(fileStream);
            string fileExtension = Path.GetExtension(fileToUpload.FileName);

            identifier = DateTime.Now.Ticks.ToString();

            string fileNameAux = fileToUpload.FileName
                .Replace("\\", "-")
                .Replace("/", "-")
                .Replace(":", "-")
                .Replace("*", "-")
                .Replace("?", "-")
                .Replace("\"", "-")
                .Replace("<", "-")
                .Replace(">", "-")
                .Replace("|", "-")
                .Replace("#", "-")
                .Replace("$", "-");

            string fileName = $"{fileNameAux.Replace(fileExtension, "")}_{identifier}{fileExtension}";

            string Region = _configuration["Region"]!;
            string S3Secret = _configuration["S3Secret"]!;

            S3SecretDTO s3SecretObject = JsonConvert.DeserializeObject<S3SecretDTO>(_secretsManagerAmazonService.GetSecret(S3Secret, Region))!;

            BasicAWSCredentials credentials = new
            (
                s3SecretObject.AccessKey,
                s3SecretObject.SecretKey
            );

            AmazonS3Config config = new()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            using AmazonS3Client client = new
            (
                credentials,
                config
            );

            try
            {
                string? bucketFolderNameString = s3SecretObject.GetType()?.GetProperty(bucketFolderName)?.GetValue(s3SecretObject)?.ToString();

                if (string.IsNullOrEmpty(bucketFolderNameString))
                    return string.Empty;

                TransferUtilityUploadRequest uploadRequest = new()
                {
                    InputStream = fileStream,
                    BucketName = s3SecretObject.BucketName,
                    Key = bucketFolderNameString + "/" + fileName,
                    CannedACL = S3CannedACL.NoACL
                };

                TransferUtility fileTransferUtility = new
                (
                    client
                );

                await fileTransferUtility.UploadAsync(uploadRequest);

                File.Delete(path);

                await Task.Delay(250);

                return "https://" + s3SecretObject.BucketName + ".s3.amazonaws.com/" + bucketFolderNameString + "/" + fileName;
            }
            catch (Exception ex)
            {
                File.Delete(path);

                return string.Empty;
            }
        }
        catch (Exception ex)
        {
            return string.Empty;
        }
    }

    public async Task<bool> DeleteFile(string fileToDelete)
    {
        try
        {
            string Region = _configuration["Region"]!;
            string S3Secret = _configuration["S3Secret"]!;

            S3SecretDTO s3SecretObject = JsonConvert.DeserializeObject<S3SecretDTO>(_secretsManagerAmazonService.GetSecret(S3Secret, Region))!;

            BasicAWSCredentials credentials = new
            (
                s3SecretObject.AccessKey,
                s3SecretObject.SecretKey
            );

            AmazonS3Config config = new()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            using AmazonS3Client client = new
            (
                credentials,
                config
            );

            try
            {
                DeleteObjectRequest deleteObjectRequest = new()
                {
                    Key = fileToDelete,
                    BucketName = s3SecretObject.BucketName,
                };

                DeleteObjectResponse deleteObjectResponse = await client.DeleteObjectAsync(deleteObjectRequest);

                if(deleteObjectResponse != null)
                    return true;

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<MemoryStream> GetFile(string fileToDownload)
    {
        try
        {
            string Region = _configuration["Region"]!;
            string S3Secret = _configuration["S3Secret"]!;

            S3SecretDTO s3SecretObject = JsonConvert.DeserializeObject<S3SecretDTO>(_secretsManagerAmazonService.GetSecret(S3Secret, Region))!;

            BasicAWSCredentials credentials = new
            (
                s3SecretObject.AccessKey,
                s3SecretObject.SecretKey
            );

            AmazonS3Config config = new()
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            using AmazonS3Client client = new
            (
                credentials,
                config
            );

            try
            {
                MemoryStream memoryStream = new();

                GetObjectRequest request = new()
                {
                    BucketName = s3SecretObject.BucketName,
                    Key = fileToDownload
                };

                using (GetObjectResponse response = await client.GetObjectAsync(request))

                using (Stream responseStream = response.ResponseStream)
                {
                    responseStream.CopyTo(memoryStream);
                }

                return memoryStream;
            }
            catch (AmazonS3Exception)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}
