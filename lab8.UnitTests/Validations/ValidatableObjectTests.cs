using lab8.UnitTests.Mocks;
using lab8.Validations;
using Xunit;

namespace lab8.UnitTests.Validations
{
    public class ValidatableObjectTests
    {
        private readonly ValidatableObject<string> _validatableObject;
        private readonly ValidationRuleMock<string> _mockValidationRule;

        public ValidatableObjectTests()
        {
            _mockValidationRule = new ValidationRuleMock<string>();
            _validatableObject = new ValidatableObject<string>();
            _validatableObject.ValidationRules.Add(_mockValidationRule);
        }

        [Fact]
        public void Validate_NoRuleOnError_ContainNoErrorsAndIsValid()
        {
            _mockValidationRule.Validity = true;

            _validatableObject.Validate();

            Assert.Empty(_validatableObject.Errors);
            Assert.True(_validatableObject.IsValid);
        }

        [Fact]
        public void Validate_RuleWithError_ContainErrorAndIsNotValid()
        {
            _mockValidationRule.Validity = false;

            _validatableObject.Validate();

            Assert.Single(_validatableObject.Errors);
            Assert.False(_validatableObject.IsValid);
        }
    }
}
