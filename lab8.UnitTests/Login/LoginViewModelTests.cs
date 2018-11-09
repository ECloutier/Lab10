using Xunit;
using Moq;
using Prism.Navigation;
using lab8;
using lab8.ViewModels;
using lab8.Services.Authentication;
using lab8.Models.Entities;
using Prism.Services;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using System.Threading.Tasks;
using lab8.Helpers;
using SQLite;
using lab8.Services;
using System;

namespace lab8.UnitTests
{
    public class LoginViewModelTests
    {
        private LoginPageViewModel loginPageViewModel;

        private Mock<IAuthenticationService> authenticationServiceMock;
        private Mock<INavigationService> navigationServiceMock;
        private Mock<IPageDialogService> pagedialogServiceMock;

        private Mock<ICryptoService> cryptoServiceMock;
        private Mock<IRepository<User>> sqLiteRepositoryMock;

        public const string NOT_AUTHENTICATED_USERNAME = "notInDb";
        public const string AUTHENTICATED_USERNAME = "123";
        public const string NOT_AUTHENTICATED_PASSWORD = "notInDb";
        public const string AUTHENTICATED_PASSWORD = "456";

        public LoginViewModelTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            pagedialogServiceMock = new Mock<IPageDialogService>();

            cryptoServiceMock = new Mock<ICryptoService>();
            sqLiteRepositoryMock = new Mock<IRepository<User>>();

            authenticationServiceMock = new Mock<IAuthenticationService>();

            loginPageViewModel = new LoginPageViewModel(authenticationServiceMock.Object, navigationServiceMock.Object, pagedialogServiceMock.Object);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldNotNavigate()
        {
            loginPageViewModel.User.Value = AUTHENTICATED_USERNAME;
            loginPageViewModel.Password.Value = NOT_AUTHENTICATED_PASSWORD;

            authenticationServiceMock.Setup(a => a.LogIn(
                                   NOT_AUTHENTICATED_USERNAME,
                                   NOT_AUTHENTICATED_PASSWORD
                                   ));

            loginPageViewModel.AuthenticateCommand.Execute();

            navigationServiceMock.Verify(x => x.NavigateAsync(nameof(Views.MainPage)), Times.Never);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsNotValid_ShouldDisplayAlertToUser()
        {
            loginPageViewModel.User.Value = AUTHENTICATED_USERNAME;
            loginPageViewModel.Password.Value = NOT_AUTHENTICATED_PASSWORD;

            authenticationServiceMock.Setup(a => a.LogIn(
                                   NOT_AUTHENTICATED_USERNAME,
                                   NOT_AUTHENTICATED_PASSWORD
                                   ));

            loginPageViewModel.AuthenticateCommand.Execute();

            pagedialogServiceMock.Verify(x => x.DisplayAlertAsync("Authentication failed", "", "Ok"), Times.Once);
        }

        [Fact]
        public void AuthenticateCommand_WhenAuthenticationIsValid_ShouldNavigateToHomePage()
        {
            loginPageViewModel.User.Value = AUTHENTICATED_USERNAME;
            loginPageViewModel.Password.Value = AUTHENTICATED_PASSWORD;

            authenticationServiceMock.Setup(a => a.LogIn(
                                   AUTHENTICATED_USERNAME,
                                   AUTHENTICATED_PASSWORD
                                   ));

            loginPageViewModel.AuthenticateCommand.Execute();

            navigationServiceMock.Verify(x => x.NavigateAsync(nameof(Views.MainPage)), Times.Once);
        }

        [Fact]
        public void AuthenticateCommand_OnException_ShouldDisplayAlertToUser()
        {
            loginPageViewModel.User.Value = NOT_AUTHENTICATED_USERNAME;
            loginPageViewModel.Password.Value = NOT_AUTHENTICATED_PASSWORD;

            authenticationServiceMock.Setup(a => a.LogIn(
                                   NOT_AUTHENTICATED_USERNAME,
                                   NOT_AUTHENTICATED_PASSWORD
                                   )).Throws(new Exception());

            loginPageViewModel.AuthenticateCommand.Execute();

            pagedialogServiceMock.Verify(x => x.DisplayAlertAsync("The following error as occured :", "", "Ok"), Times.Once);
        }
    }
}