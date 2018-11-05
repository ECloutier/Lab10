using Xunit;
using Moq;
using Prism.Navigation;
using lab8.ViewModels;
using Prism.Services;

namespace lab8.UnitTests.Navigation
{
    public class RegisterNavigationTests
    {
        private const string VALID_USER = "test@test.ca";
        private const string INVALID_USER = "test";

        private const string VALID_PW = "123Test123";
        private const string INVALID_PW = "123Test";

        private RegisterPageViewModel registerPageViewModel;
        private Mock<INavigationService> navigationServiceMock;
        public RegisterNavigationTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            registerPageViewModel = new RegisterPageViewModel(navigationServiceMock.Object);
        }

        [Fact]
        public void RegisterCommand_WhenInvalidUserAndValidPassword_ShouldNotNavigate()
        {
            registerPageViewModel.User.Value = INVALID_USER;
            registerPageViewModel.Password.Value = VALID_PW;

            registerPageViewModel.ValidateUserCommand.Execute();
            registerPageViewModel.ValidatePasswordCommand.Execute();
            bool canExecute = registerPageViewModel.RegisterCommand.CanExecute();

            Assert.False(canExecute);
        }

        [Fact]
        public void RegisterCommand_WhenValidUserAndInvalidPassword_ShouldNotNavigate()
        {
            registerPageViewModel.User.Value = VALID_USER;
            registerPageViewModel.Password.Value = INVALID_PW;

            registerPageViewModel.ValidateUserCommand.Execute();
            registerPageViewModel.ValidatePasswordCommand.Execute();
            bool canExecute = registerPageViewModel.RegisterCommand.CanExecute();

            Assert.False(canExecute);
        }

        [Fact]
        public void RegisterCommand_WhenInvalidUserAndInvalidPassword_ShouldNotNavigate()
        {
            registerPageViewModel.User.Value = INVALID_USER;
            registerPageViewModel.Password.Value = INVALID_PW;

            registerPageViewModel.ValidateUserCommand.Execute();
            registerPageViewModel.ValidatePasswordCommand.Execute();
            bool canExecute = registerPageViewModel.RegisterCommand.CanExecute();

            Assert.False(canExecute);
        }

        [Fact]
        public void RegisterCommand_WhenValidUserAndValidPassword_ShouldNotNavigate()
        {
            registerPageViewModel.User.Value = VALID_USER;
            registerPageViewModel.Password.Value = VALID_PW;

            registerPageViewModel.ValidateUserCommand.Execute();
            registerPageViewModel.ValidatePasswordCommand.Execute();
            bool canExecute = registerPageViewModel.RegisterCommand.CanExecute();

            Assert.True(canExecute);
        }

    }
}
