using System.Text.RegularExpressions;

namespace lab8.Validations
{
    public class HasAtLeast1Number<T> : IValidationRule<T>
    {
        private Regex NumberRegex = new Regex(@"[0-9]");
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var password = value as string;
            if (!NumberRegex.IsMatch(password))
            {
                return false;
            }
            return true;
        }
    }
}