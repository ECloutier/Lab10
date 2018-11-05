using lab8.Validations;
using Xunit;

namespace lab8.UnitTests.Validations.Rules
{
    public class IsNotNullOrEmptyTests
    {
        private IsNotNullOrEmpty<string> _isNotNullOrEmptyRule;

        public IsNotNullOrEmptyTests()
        {
            _isNotNullOrEmptyRule = new IsNotNullOrEmpty<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        public void Check_EmptyString_ReturnFalse(string emptyString)
        {
            var isValid = _isNotNullOrEmptyRule.Check(emptyString);

            Assert.False(isValid);
        }
    }
}
