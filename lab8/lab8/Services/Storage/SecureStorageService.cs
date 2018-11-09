using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace lab8.Services.Storage
{
    public class SecureStorageService : ISecureStorageService
    {
        public async Task<string> GetEncryptionKeyAsync(string keyId)
        {
            try
            {
                return await SecureStorage.GetAsync(keyId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task SetEncryptionKeyAsync(string keyId, string keyValue)
        {
            try
            {
                await SecureStorage.SetAsync(keyId, keyValue);
            }
            catch (Exception ex)
            {
                //return ex.Message;
            }
        }
    }
}
