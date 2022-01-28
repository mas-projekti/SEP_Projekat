using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message) : base(message)
        {
        }
    }
}
