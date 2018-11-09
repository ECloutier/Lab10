using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lab8.Validations;
using System.ComponentModel;
using lab8.Services.Repository;
using lab8.Models.Entities;
using Prism.Services;
using lab8.Services.Authentication;
using lab8.Services.Crypto;
using lab8.Services.Storage;

namespace lab8.ViewModels
{
	public class RegisterPageViewModel : ViewModelBase
    {
        private ValidatableObject<string> _user;
        private ValidatableObject<string> _password;
        private ValidatableObject<string> _creditCard;
        private bool _isValidated;
        private INavigationService _navigationService;
        private IAuthenticationService _authenticationService;
        private ICryptoService _cryptoService;
        private ISecureStorageService _secureStorageService;

        public ValidatableObject<string> User
        {
            get => _user;
        }
        public ValidatableObject<string> Password
        {
            get => _password;
        }

        public ValidatableObject<string> CreditCard
        {
            get => _creditCard;
        }

        public bool IsValidated
        {
            get => _isValidated;
            set
            {
                _isValidated = value;
                RaisePropertyChanged();
            }
        }

        public RegisterPageViewModel(INavigationService navigationService/*, IAuthenticationService authenticationService, ICryptoService cryptoService, ISecureStorageService secureStorageService*/)
        : base(navigationService)
        {
            _user = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _navigationService = navigationService;
            /*_authenticationService = authenticationService;
            _cryptoService = cryptoService;
            _secureStorageService = secureStorageService;*/

            AddValidations();
        }

        public DelegateCommand ValidateUserCommand => new DelegateCommand(ValidateUser);
        private void ValidateUser()
        {
            _user.Validate();
            UpdateButton();
        }

        public DelegateCommand ValidatePasswordCommand => new DelegateCommand(ValidatePassword);
        private void ValidatePassword()
        {
            _password.Validate();
            UpdateButton();
        }

        private void UpdateButton()
        {
            if(_user.IsValid && _password.IsValid)
            {
                IsValidated = true;
            } else {
                IsValidated = false;
            }
        }

        public DelegateCommand RegisterCommand => new DelegateCommand(Register,CanRegister);
        private bool CanRegister()
        {

            if (User.Value == null || Password.Value == null)
            {
                return false;
            }

            return IsValidated;
        }

        private async void Register()
        {
            ValidateUser();
            ValidatePassword();

            if (_user.IsValid && _password.IsValid)
            {
                User user = new User();
                CreateAndAddUserToDatabase();
                await _navigationService.NavigateAsync(nameof(Views.MainPage));
            }
        }

        private void CreateAndAddUserToDatabase()
        {
            string userSalt = _cryptoService.GenerateSalt();
            /*string keyId = ""
            _secureStorageService.SetEncryptionKeyAsync(KEY_ID, cryptoService.GenerateEncryptionKey());

            _authenticationService.Add(new User()
            {
                Login = _user.ToString(),
                HashedPassword = _cryptoService.HashSHA512(_password.ToString(), userSalt),
                PasswordSalt = userSalt,
                CreditCard = _cryptoService.Encrypt(_creditCard.ToString(), encryptionKey)*/
        }

        public DelegateCommand NavigateToLoginCommand => new DelegateCommand(NavigateToLogin);

        private async void NavigateToLogin()
        {
            await _navigationService.NavigateAsync(nameof(Views.LoginPage));
        }

        private void AddValidations()
        {
            //add validations for users
            _user.ValidationRules.Add(new IsNotNullOrEmpty<string>
            {
                ValidationMessage = "A username is required."
            });
            _user.ValidationRules.Add(new IsEmailValid<string>
            {
                ValidationMessage = "Email is invalid."
            });

            //add validations for password
            _password.ValidationRules.Add(new IsNotNullOrEmpty<string>
            {
                ValidationMessage = "A password is required."
            });
            _password.ValidationRules.Add(new HasAtLeast10Chars<string>
            {
                ValidationMessage = "Password must be at least 10 char."
            });
            _password.ValidationRules.Add(new HasAtLeast1LowerCase<string>
            {
                ValidationMessage = "A password required at least ONE lowercase letter."
            });
            _password.ValidationRules.Add(new HasAtLeast1Number<string>
            {
                ValidationMessage = "A password required at least ONE number."
            });
            _password.ValidationRules.Add(new HasAtLeast1UpperCase<string>
            {
                ValidationMessage = "A password required at least ONE uppercase letter."
            });
            //_password.ValidationRules.Add(new IsPasswordValid<string>());
        }

    }
}
