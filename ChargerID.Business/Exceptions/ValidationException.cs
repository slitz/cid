using System;

namespace ChargerID.Business.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
