using System;

namespace msgraphapi.ExceptionHandling
{
    public class InvalidRequestException : Exception
    {
        private static readonly Func<string, string> GetError = errorMessage =>
            $"The request has invalid parameters. {errorMessage}";

        public InvalidRequestException(string message) : base(GetError(message))
        {
        }

        public InvalidRequestException(string message, Exception inner) : base(GetError(message), inner)
        {
        }
    }
}