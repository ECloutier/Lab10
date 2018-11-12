using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Prism.Services;

namespace lab8.Services.SecureStorageEssentials
{
    public class SecureStorageService : ISecureStorageService
    {
        IPageDialogService _pageDialogService;

        public SecureStorageService(IPageDialogService pageDialogService)
        {
            _pageDialogService = pageDialogService;
        }
        public async Task<string> GetEncryptionKeyAsync(string keyId)
        {
            try
            {
                return await SecureStorage.GetAsync(keyId);    
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public async Task SetEncryptionKeyAsync(string keyId, string keyValue)
        {
            try
            {
                await SecureStorage.SetAsync(keyId, keyValue);
            }
            catch (Exception)
            {
                await _pageDialogService.DisplayActionSheetAsync("Erreur", "SetEncryptionKeyAsync Exception thrown", "Ok");
            }
        }
    }
}
