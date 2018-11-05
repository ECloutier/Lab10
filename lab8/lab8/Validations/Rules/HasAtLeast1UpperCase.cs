using System.Text.RegularExpressions;

namespace lab8.Validations
{
    public class HasAtLeast1UpperCase<T> : IValidationRule<T>
    {
        private Regex UpperCaseLetterRegex = new Regex(@"[A-Z]");
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var password = value as string;
            if (!UpperCaseLetterRegex.IsMatch(password))
            {
                return false;
            }
            return true;
        }
    }
}