using Prism.Commands;
using Prism.Navigation;
using lab8.Validations;
using Prism.Services;
using lab8.Services.Authentication;
namespace lab8.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
    {
        private ValidatableObject<string> _user;
        private ValidatableObject<string> _password;

        private IAuthenticationService _authenticationService;
        private IPageDialogService _pageDialogService;
        private INavigationService _navigationService;

        private INavigationParameters userParameters = new NavigationParameters();

        public ValidatableObject<string> User
        {
            get => _user;
            set => _user = value;
        }
        public ValidatableObject<string> Password
        {
            get => _password;
            set => _password = value;
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
                _authenticationService.AuthenticateUser(_user.Value, _password.Value);

                if(_authenticationService.IsUserAuthenticated)
                {
                    userParameters.Add("id", _authenticationService.UserID);

                    await _navigationService.NavigateAsync(nameof(Views.MainPage), userParameters);
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Authentication failed","", "Ok");
                }

            }
            catch
            {
                await _pageDialogService.DisplayAlertAsync("Error as occured :", "", "Ok");
            }
        }

    }
}
