using Xunit;
using Moq;
using Prism.Navigation;
using lab8.ViewModels;
using lab8.Services.Authentication;
using lab8.Services.Crypto;
using lab8.Services.Repository;
using lab8.Models.Entities;
using lab8.Services.SecureStorageEssentials;
using System;
using Prism.Services;
using System.Collections.Generic;
using lab8.Helpers;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using System.IO;

namespace lab8.UnitTests.MainPage
{
    public class MainPageViewModelTests
    {
        private MainPageViewModel mainPageViewModel;
        private Mock<INavigationService> navigationServiceMock;
        private Mock<ISecureStorageService> secureStorageServiceMock;
        private Mock<IPageDialogService> pageDialogServiceMock;

        private IRepository<User> dbTest;
        private ICryptoService cryptoService;
        private INavigationParameters parameters;
        private IAuthenticationService authenticationService;
        private ISecureStorageService secureStorageService;

        bool _eventRaised = false;

        public MainPageViewModelTests()
        {
            //Test database setup
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string databasePath = Path.Combine(path, "TestDatabase.db3");
            var databaseSqLiteConnection = new SQLiteConnection(databasePath);
            dbTest = new SqLiteRepository<User>(databaseSqLiteConnection);

            //some mock 
            navigationServiceMock = new Mock<INavigationService>();
            pageDialogServiceMock = new Mock<IPageDialogService>();
            secureStorageServiceMock = new Mock<ISecureStorageService>();
            
            //service i need to work with
            cryptoService = new CryptoService();
            authenticationService = new AuthenticationService(cryptoService, dbTest);
            mainPageViewModel = new MainPageViewModel(navigationServiceMock.Object, authenticationService, dbTest, secureStorageServiceMock.Object, cryptoService, pageDialogServiceMock.Object);

            //navigation parameters
            
            secureStorageService = new SecureStorageService(pageDialogServiceMock.Object);

            //seed my shit
            SeedTestData();
        }

        [Fact]
        public void CreditCardProperty_WhenUserIsAuthAndLogIn_ShouldRaiseProperty()
        {
            mainPageViewModel.PropertyChanged += PropertyChanged;
            parameters = new NavigationParameters();
            parameters.Add("id", authenticationService.UserID);

            mainPageViewModel.OnNavigatingTo(parameters);
            mainPageViewModel.CreditCard = "3450";
            Assert.True(_eventRaised);
        }

        [Fact]
        public void CreditCardProperty_WhenUserIsNotAuthAndLogIn_ShouldNotRaiseProperty()
        {
            mainPageViewModel.PropertyChanged += PropertyChanged;
            parameters = new NavigationParameters();
            parameters.Add("id", 6543);

            mainPageViewModel.OnNavigatingTo(parameters);
            Assert.False(_eventRaised);
        }

        [Fact]
        public void UserNotInDB_ThrowException_ShouldAlert()
        {
            mainPageViewModel.PropertyChanged += PropertyChanged;
            parameters = new NavigationParameters();
            parameters.Add("id", null);

            mainPageViewModel.OnNavigatingTo(parameters);
            pageDialogServiceMock.Verify(x => x.DisplayAlertAsync("User not found in db", "", "Ok"), Times.Once);
        }

        private void SeedTestData()
        {
            if(dbTest.GetAll().Count() != 0)
            {
                foreach(User seededUser in dbTest.GetAll())
                {
                    dbTest.Delete(seededUser);
                }
            }

            string userSalt = cryptoService.GenerateSalt();
            string encryptionKey = cryptoService.GenerateEncryptionKey();     

            var user = new User()
            {
                Login = "123",
                HashedPassword = cryptoService.HashSHA512("456", userSalt),
                PasswordSalt = userSalt,
                CreditCard = cryptoService.Encrypt("420420420420420420", encryptionKey)
            };

            dbTest.Add(user);

            secureStorageService.SetEncryptionKeyAsync(user.Id.ToString(), encryptionKey);
        }

        private void PropertyChanged(object sender, EventArgs e)
        {
            _eventRaised = true;
        }
    }
}
