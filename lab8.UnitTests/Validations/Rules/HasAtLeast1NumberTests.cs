using lab8.Validations;
using Xunit;

namespace lab8.UnitTests.Validations.Rules
{
    public class HasAtLeast1NumberTests
    {
        private HasAtLeast1Number<string> _hasAtLeast1NumberRule;
        public const string NO_NUMBER = "thereAreNoNumberHere";
        public const string WITH_NUMBER = "thereAre1NumberHere";

        public HasAtLeast1NumberTests()
        {
            _hasAtLeast1NumberRule = new HasAtLeast1Number<string>();
        }

        [Fact]
        public void Check_PasswordWithoutNumber_ReturnFalse()
        {
            var isValid = _hasAtLeast1NumberRule.Check(NO_NUMBER);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_PasswordWithNumber_ReturnTrue()
        {
            var isValid = _hasAtLeast1NumberRule.Check(WITH_NUMBER);

            Assert.True(isValid);
        }
    }
}
