namespace lab8.Validations
{
    public class HasAtLeast10Chars<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var password = value as string;
            if (password.Length < 10)
            {
                return false;
            }
            return true;
        }
    }
}