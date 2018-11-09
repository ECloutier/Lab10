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
using lab8.Services.Storage;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace lab8
{
    public partial class App
    {
        public static readonly string KEY_ID = "Cryptohard";
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
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
            var cryptoService = Container.Resolve<ICryptoService>();
            var secureStorageService = Container.Resolve<ISecureStorageService>();

            secureStorageService.SetEncryptionKeyAsync(KEY_ID, cryptoService.GenerateEncryptionKey());

            string userSalt = cryptoService.GenerateSalt();
            string encrpytionKey = "cryptoeasy";

            if (userRepository.GetAll().Count() != 0)
                return;

            userRepository.Add(new User()
            {
                Login = "83",
                HashedPassword = cryptoService.HashSHA512("1420", userSalt),
                PasswordSalt = userSalt,
                CreditCard = cryptoService.Encrypt("9192123451513121", encrpytionKey)
            });
        }

    }
}
