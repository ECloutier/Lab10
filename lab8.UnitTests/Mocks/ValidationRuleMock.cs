using lab8.Validations;

namespace lab8.UnitTests.Mocks
{
    class ValidationRuleMock<T> : IValidationRule<T>
    {
        public bool Validity { get; set; }

        public string ValidationMessage { get; set; }

        public ValidationRuleMock()
        {
            ValidationMessage = "Error";
        }

        public bool Check(T value)
        {
            return Validity;
        }
    }
}