using Xunit;
using Moq;
using Prism.Navigation;
using lab8.ViewModels;
using lab8.Views;
using System.Threading.Tasks;
using Prism.Services;

namespace lab8.UnitTests.Navigation
{
    public class LoginNavigationTests
    {
        private RegisterPageViewModel registerPageViewModel;
        private Mock<INavigationService> navigationServiceMock;

        public LoginNavigationTests()
        {
            navigationServiceMock = new Mock<INavigationService>();
            registerPageViewModel = new RegisterPageViewModel(navigationServiceMock.Object);
        }

        [Fact]
        public void NavigateToLoginCommand_OnClick_ShouldNavigate()
        {
            registerPageViewModel.NavigateToLoginCommand.Execute();
            navigationServiceMock.Verify(x => x.NavigateAsync(nameof(Views.LoginPage)), Times.Once);
        }
    }
}
