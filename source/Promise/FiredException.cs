using System;

namespace PromiseSharp
{
    public class FiredException : Exception
    {
        public FiredException(): base("Already fired.")
        {
        }

        public FiredException(string message) : base(message)
        {
        }

        public FiredException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}