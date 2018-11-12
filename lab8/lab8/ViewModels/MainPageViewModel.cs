using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab8.Services.Authentication;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using lab8.Models.Entities;
using lab8.Services.SecureStorageEssentials;
using Prism.Services;

namespace lab8.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigatingAware
    {
        private string _creditCard;

        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        private IRepository<User> _db;
        private ISecureStorageService _secureStorageService;
        private ICryptoService _cryptoService;
        private IPageDialogService _pageDialogService;
        public string CreditCard
        {
            get => _creditCard;
            set
            {
                _creditCard = value;
                RaisePropertyChanged();
            }
        }

        public MainPageViewModel(INavigationService navigationService, IAuthenticationService authentificationService, IRepository<User> db, ISecureStorageService secureStorageService, ICryptoService cryptoService, IPageDialogService pageDialogService)
            : base(navigationService)
        {
            _navigationService = navigationService;
            _authenticationService = authentificationService;
            _secureStorageService = secureStorageService;
            _cryptoService = cryptoService;
            _pageDialogService = pageDialogService;
            _db = db;
            
        }
        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            try
            {
                var userID = parameters.GetValue<int>("id");
                User authUser = _db.GetAll().Where(user => user.Id.ToString() == userID.ToString()).FirstOrDefault();

                string encryptionKey = await _secureStorageService.GetEncryptionKeyAsync(userID.ToString());
                string encryptedCreditCard = authUser.CreditCard;
                string decryptedCreditCard = _cryptoService.Decrypt(encryptedCreditCard, encryptionKey);

                CreditCard = decryptedCreditCard.Substring(Math.Max(0, decryptedCreditCard.Length - 4));
            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync("User not found in db", "", "Ok");
            }
        }
    }
}
