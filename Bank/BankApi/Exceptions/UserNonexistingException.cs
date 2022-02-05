using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Exceptions
{
    public class UserNonexistingException : Exception
    {
        public UserNonexistingException(string message) : base(message)
        {
        }
    }
}
