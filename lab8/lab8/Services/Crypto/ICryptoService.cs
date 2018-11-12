using lab8.Services;

namespace lab8.Services.Crypto
{
    public interface ICryptoService
    {
        string Decrypt(string encryptedValue, string encryptionKey);
        string Encrypt(string clearValue, string encryptionKey);
        string GenerateSalt();
        string HashSHA512(string value, string salt);
        string GenerateEncryptionKey();
    }
}
