using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lab8.Services.Storage
{
    public interface ISecureStorageService
    {
        Task<string> GetEncryptionKeyAsync(string keyId);
        Task SetEncryptionKeyAsync(string keyId, string keyValue);
    }
}
