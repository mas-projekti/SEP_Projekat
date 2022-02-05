using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Exceptions
{
    public class PasswordInvalidException : Exception
    {
        public PasswordInvalidException(string message) : base(message)
        {
        }
    }
}
