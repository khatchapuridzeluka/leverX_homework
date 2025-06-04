namespace leverX.Domain.Exceptions
{
    public class InsertFailedException : Exception
    {
        public InsertFailedException(string message) : base(message) { } 
    }
}
