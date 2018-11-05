using System.Text.RegularExpressions;

namespace lab8.Validations
{
    public class IsEmailValid<T> : IValidationRule<T>
    {
        private Regex EmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        public string ValidationMessage { get; set; }
        public bool Check(T value)
        {
            var email = value as string;

            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            return EmailRegex.IsMatch(email);
        }
    }
}
