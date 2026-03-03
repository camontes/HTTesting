using HR_Platform.Application.ServicesInterfaces;
using HR_Platform.Application.ServicesModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace HR_Platform.Application.Services;

public class EncryptService(
    ISecretsManagerAmazonService secretsManagerAmazonService, 
    IConfiguration configuration) : IEncryptService
{
    private readonly ISecretsManagerAmazonService _secretsManagerAmazonService = secretsManagerAmazonService;

    private readonly IConfiguration _configuration = configuration;

    public string EncryptString(string plainText)
    {
        try
        {
            string Region = _configuration["Region"]!;
            string EncryptionSecret = _configuration["EncryptionSecret"]!;

            EncrpytionSecretDTO encrpytionSecretObject = JsonConvert.DeserializeObject<EncrpytionSecretDTO>(_secretsManagerAmazonService.GetSecret(EncryptionSecret, Region))!;

            byte[] cipheredtext;

            string cipheredPlainText = string.Empty;

            using (Aes aes = Aes.Create())
            {
                byte[] key = Convert.FromBase64String(!string.IsNullOrEmpty(encrpytionSecretObject.EncryptionKey) ? encrpytionSecretObject.EncryptionKey : string.Empty);

                byte[] iv = Convert.FromBase64String(!string.IsNullOrEmpty(encrpytionSecretObject.IvKey) ? encrpytionSecretObject.IvKey : string.Empty);

                ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);

                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }

                cipheredtext = memoryStream.ToArray();

                cipheredPlainText = Convert.ToBase64String(cipheredtext);
            }

            return cipheredPlainText;
        }
        catch(Exception)
        {
            return string.Empty;
        }
    }

    public string DecryptString(string cipheredPlainText)
    {
        try
        {
            string Region = _configuration["Region"]!;
            string EncryptionSecret = _configuration["EncryptionSecret"]!;

            EncrpytionSecretDTO encrpytionSecretObject = JsonConvert.DeserializeObject<EncrpytionSecretDTO>(_secretsManagerAmazonService.GetSecret(EncryptionSecret, Region))!;

            string plainText = string.Empty;

            byte[] cipheredtext = Convert.FromBase64String(cipheredPlainText);

            using (Aes aes = Aes.Create())
            {
                byte[] key = Convert.FromBase64String(!string.IsNullOrEmpty(encrpytionSecretObject.EncryptionKey) ? encrpytionSecretObject.EncryptionKey : string.Empty);

                byte[] iv = Convert.FromBase64String(!string.IsNullOrEmpty(encrpytionSecretObject.IvKey) ? encrpytionSecretObject.IvKey : string.Empty);

                aes.Padding = PaddingMode.None;

                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);

                using MemoryStream memoryStream = new(cipheredtext);
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                using StreamReader streamReader = new(cryptoStream);
                string pattern = @"[\u0000-\u001F\u007F]";
                plainText = Regex.Replace(streamReader.ReadToEnd(), pattern, string.Empty).Trim();
            }

            return plainText;
        }
        catch(Exception)
        {
            return string.Empty;
        }
    }
}
