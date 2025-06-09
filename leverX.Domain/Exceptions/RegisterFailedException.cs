namespace leverX.Domain.Exceptions
{
    public class RegisterFailedException : Exception
    {
        public RegisterFailedException(string message) : base(message) { }
    }
}
