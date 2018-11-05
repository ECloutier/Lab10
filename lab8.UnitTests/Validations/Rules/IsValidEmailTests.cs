using lab8.Validations;
using Xunit;

namespace lab8.UnitTests.Validations.Rules
{
    public class IsValidEmailTests
    {
        private IsEmailValid<string> _isEmailValidRule;

        public IsValidEmailTests()
        {
            _isEmailValidRule = new IsEmailValid<string>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("noAt.com")]
        [InlineData("@noWordBeforeAt.com")]
        [InlineData("noWordAfterAt@.com")]
        [InlineData("afterAtIsEmpty@")]
        [InlineData("no@domainExtension")]
        [InlineData("no@validDomainExtension.notValid")]
        [InlineData("invalidChar$@email.com")]
        [InlineData("invalidChar!@email.com")]
        [InlineData("invalidChar%@email.com")]
        [InlineData("invalidChar^@email.com")]
        [InlineData("invalidChar&@email.com")]
        [InlineData("invalidChar*@email.com")]
        [InlineData("invalidChar(@email.com")]
        [InlineData("invalidChar)@email.com")]
        [InlineData("invalidChar/@email.com")]
        [InlineData("invalidChar=@email.com")]
        [InlineData("invalidChar{@email.com")]
        [InlineData("invalidChar}@email.com")]
        [InlineData("invalidChar+@email.com")]
        [InlineData("invalidChar`@email.com")]
        [InlineData("invalidChar~@email.com")]
        [InlineData("invalidChar?@email.com")]
        [InlineData("invalidChar>@email.com")]
        [InlineData("invalidChar<@email.com")]
        [InlineData("invalidChar]@email.com")]
        [InlineData("invalidChar[@email.com")]
        [InlineData("invalidChar\\@email.com")]
        [InlineData("invalidChar\"@email.com")]
        public void Check_InvalidEmail_ReturnFalse(string invalidEmail)
        {
            var isValid = _isEmailValidRule.Check(invalidEmail);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("jean@nicolas.com")]
        [InlineData("je@an.com")]
        [InlineData("jean@nicolas.ca")]
        [InlineData("je@an.ca")]
        [InlineData("jean@nicolas.org")]
        [InlineData("je@an.org")]
        [InlineData("jean@nicolas.net")]
        [InlineData("je@an.net")]
        [InlineData("jean@nicolas.io")]
        [InlineData("je@an.io")]
        [InlineData("jean@nicolas.fr")]
        [InlineData("je@an.fr")]
        [InlineData("jean1@nicolas.fr")]
        [InlineData("jean@nicolas2.fr")]
        public void Check_ValidEmail_ReturnTrue(string validEmail)
        {
            var isValid = _isEmailValidRule.Check(validEmail);

            Assert.True(isValid);
        }
    }
}
