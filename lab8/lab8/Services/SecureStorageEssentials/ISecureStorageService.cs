using System.Threading.Tasks;

namespace lab8.Services.SecureStorageEssentials
{
    public interface ISecureStorageService
    {
        Task<string> GetEncryptionKeyAsync(string keyId);
        Task SetEncryptionKeyAsync(string keyId, string keyValue);
    }
}
