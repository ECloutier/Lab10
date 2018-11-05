using lab8.Validations;
using Xunit;

namespace lab8.UnitTests.Validations.Rules
{
    public class HasAtLeast1UpperCaseTests
    {
        private HasAtLeast1UpperCase<string> _hasAtLeast1UpperCaseRule;
        public const string NO_UPPER_CASE = "nouppercase1";
        public const string WITH_UPPER_CASE = "WithUppercase1";

        public HasAtLeast1UpperCaseTests()
        {
            _hasAtLeast1UpperCaseRule = new HasAtLeast1UpperCase<string>();
        }

        [Fact]
        public void Check_PasswordWithoutUpperCase_ReturnFalse()
        {
            var isValid = _hasAtLeast1UpperCaseRule.Check(NO_UPPER_CASE);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_PasswordWithUpperCase_ReturnTrue()
        {
            var isValid = _hasAtLeast1UpperCaseRule.Check(WITH_UPPER_CASE);

            Assert.True(isValid);
        }
    }
}
