using lab8.Validations;
using Xunit;
namespace lab8.UnitTests.Validations.Rules
{
    public class HasAtLeast10CharsTests
    {
        private HasAtLeast10Chars<string> _HasAtLeast10CharsRule;
        public const string BAD_PW_LENGTH = "Only9Char";
        public const string GOOD_PW_LENGTH = "10CharGood";
        public HasAtLeast10CharsTests()
        {
            _HasAtLeast10CharsRule = new HasAtLeast10Chars<string>();
        }

        [Fact]
        public void Check_PasswordWithoutEnoughChar_ReturnFalse()
        {
            var isValid = _HasAtLeast10CharsRule.Check(BAD_PW_LENGTH);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_PasswordWithEnoughChar_ReturnTrue()
        {
            var isValid = _HasAtLeast10CharsRule.Check(GOOD_PW_LENGTH);

            Assert.True(isValid);
        }
    }
}
