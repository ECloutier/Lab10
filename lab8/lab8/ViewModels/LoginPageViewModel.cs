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
using lab8.Services;

namespace lab8.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
    {
        private ValidatableObject<string> _user;
        private ValidatableObject<string> _password;

        private IAuthenticationService _authenticationService;
        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;

        public ValidatableObject<string> User
        {
            get => _user;
        }
        public ValidatableObject<string> Password
        {
            get => _password;
        }

        public LoginPageViewModel(IAuthenticationService authenticationService, INavigationService navigationService, IPageDialogService pageDialogService)
        : base(navigationService)
        {
            _user = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            _authenticationService = authenticationService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;
        }

        public DelegateCommand AuthenticateCommand => new DelegateCommand(Authenticate);
        private async void Authenticate()
        {
            try
            {
                if(_authenticationService.AuthenticateUser(User.Value, Password.Value))
                {
                    await _navigationService.NavigateAsync(nameof(Views.MainPage));
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Authentication failed","", "Ok");
                }

            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync("The following error as occured :", "", "Ok");
            }
        }

    }
}
