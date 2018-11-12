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
using System.Collections.Generic;

namespace lab8.UnitTests.Authentication
{
    public class AuthenticationTests
    {
        private LoginPageViewModel loginPageViewModel;

        private IAuthenticationService authenticationService;
        private Mock<INavigationService> navigationServiceMock;
        private Mock<IPageDialogService> pagedialogServiceMock;

        private ICryptoService cryptoService;
        private Mock<IRepository<User>> sqLiteRepositoryMock;

        public const string NOT_AUTHENTICATED_USERNAME = "notInDb";
        public const string AUTHENTICATED_USERNAME = "123";
        public const string NOT_AUTHENTICATED_PASSWORD = "notInDb";
        public const string AUTHENTICATED_PASSWORD = "456";

        public AuthenticationTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            pagedialogServiceMock = new Mock<IPageDialogService>();

            cryptoService = new CryptoService();
            sqLiteRepositoryMock = new Mock<IRepository<User>>();
            authenticationService = new AuthenticationService(cryptoService, sqLiteRepositoryMock.Object);

            loginPageViewModel = new LoginPageViewModel(authenticationService, navigationServiceMock.Object, pagedialogServiceMock.Object);

            SeedTestData();
        }

        private void SeedTestData()
        {

            string userSalt = cryptoService.GenerateSalt();

            var user = new User()
            {
                Login = "123",
                HashedPassword = cryptoService.HashSHA512("456", userSalt),
                PasswordSalt = userSalt,
                CreditCard = cryptoService.Encrypt("5162042483342023", "any")
            };

            List<User> listOfUsers = new List<User>();
            listOfUsers.Add(user);
            sqLiteRepositoryMock.Setup(x => x.GetAll()).Returns(listOfUsers);
        }

        [Fact]
        public void UserNameNotInDatabse_WhenAuthenticateUser_returnIsNotAuthenticated()
        {
            authenticationService.AuthenticateUser(NOT_AUTHENTICATED_USERNAME, AUTHENTICATED_PASSWORD);
            bool isAuth = authenticationService.IsUserAuthenticated;

            Assert.False(isAuth);
        }

        [Fact]
        public void UserNameInDatabassePasswordIsNot_WhenAuthenticateUser_returnIsNotAuthenticated()
        {
            authenticationService.AuthenticateUser(AUTHENTICATED_USERNAME, NOT_AUTHENTICATED_PASSWORD);
            bool isAuth = authenticationService.IsUserAuthenticated;
            Assert.False(isAuth);
        }

        [Fact]
        public void UserNameAndPasswordAreNotInDatabase_WhenAuthenticateUser_returnIsNotAuthenticated()
        {
            authenticationService.AuthenticateUser(NOT_AUTHENTICATED_USERNAME, NOT_AUTHENTICATED_PASSWORD);
            bool isAuth = authenticationService.IsUserAuthenticated;

            Assert.False(isAuth);
        }

        [Fact]
        public void UserNameAndPasswordAreInDatabase_WhenAuthenticateUser_returnIsAuthenticated()
        {
            authenticationService.AuthenticateUser(AUTHENTICATED_USERNAME, AUTHENTICATED_PASSWORD);
            bool isAuth = authenticationService.IsUserAuthenticated;

            Assert.True(isAuth);
        }

    }
}
