using lab8.Validations;
using Xunit;
namespace lab8.UnitTests.Validations.Rules
{
    public class HasAtLeast1LowerCaseTests
    {
        private HasAtLeast1LowerCase<string> _hasAtLeast1LowerCaseRule;
        public const string NO_LOWER_CASE = "NOLOWERCASE1";
        public const string WITH_LOWER_CASE = "THEREiS1LOWERCASE";
        public HasAtLeast1LowerCaseTests()
        {
            _hasAtLeast1LowerCaseRule = new HasAtLeast1LowerCase<string>();
        }
        [Fact]
        public void Check_PasswordWithoutLowerCase_ReturnFalse()
        {
            var isValid = _hasAtLeast1LowerCaseRule.Check(NO_LOWER_CASE);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_PasswordWithLowerCase_ReturnTrue()
        {
            var isValid = _hasAtLeast1LowerCaseRule.Check(WITH_LOWER_CASE);

            Assert.True(isValid);
        }
    }
}
