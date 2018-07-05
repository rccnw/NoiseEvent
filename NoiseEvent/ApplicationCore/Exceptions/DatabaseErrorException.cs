using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class DatabaseErrorException : Exception
    {
        public DatabaseErrorException(string message) : base(message)
        {
        }

        public DatabaseErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
