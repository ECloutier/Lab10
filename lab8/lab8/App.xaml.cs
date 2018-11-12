using Prism;
using Prism.Ioc;
using lab8.ViewModels;
using lab8.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using lab8.Models.Entities;
using lab8.Helpers;
using System.Linq;
using lab8.Services.Crypto;
using lab8.Services.Authentication;
using lab8.Services.Repository;
using lab8.Services.SecureStorageEssentials;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace lab8
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SeedTestData();
            await NavigationService.NavigateAsync("NavigationPage/RegisterPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var databasePath = DependencyService.Get<IFileHelper>().GetLocalFilePath("MyDatabase.db3");
            var databaseSqLiteConnection = new SQLiteConnection(databasePath);

            containerRegistry.RegisterInstance(databaseSqLiteConnection);
            containerRegistry.RegisterSingleton<IRepository<User>, SqLiteRepository<User>>();
            containerRegistry.RegisterSingleton<IAuthenticationService, AuthenticationService>();
            containerRegistry.RegisterSingleton<ICryptoService, CryptoService>();
            containerRegistry.RegisterSingleton<ISecureStorageService, SecureStorageService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
        }

        private void SeedTestData()
        {
            var userRepository = Container.Resolve<IRepository<User>>();
            if (userRepository.GetAll().Count() != 0)
                return;

            var cryptoService = Container.Resolve<ICryptoService>();
            var secureStorageService = Container.Resolve<ISecureStorageService>();
           
            string userSalt = cryptoService.GenerateSalt();
            string encryptionKey = cryptoService.GenerateEncryptionKey();
            string creditcard = "5162042483342023";

            var user = new User()
            {
                HashedPassword = cryptoService.HashSHA512("456", userSalt),
                Login = "123",
                PasswordSalt = userSalt,
                CreditCard = cryptoService.Encrypt(creditcard, encryptionKey)
            };

            userRepository.Add(user);
            secureStorageService.SetEncryptionKeyAsync(user.Id.ToString(), encryptionKey);
        }
    }
}
