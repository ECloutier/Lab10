using System.Text.RegularExpressions;

namespace lab8.Validations
{
    public class HasAtLeast1LowerCase<T> : IValidationRule<T>
    {
        private Regex LowerCaseLetterRegex = new Regex(@"[a-z]");
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var password = value as string;
            if (!LowerCaseLetterRegex.IsMatch(password))
            {
                return false;
            }
            return true;
        }
    }
}