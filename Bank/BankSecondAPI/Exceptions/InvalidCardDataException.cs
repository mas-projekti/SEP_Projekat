using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Exceptions
{
    public class InvalidCardDataException : Exception
    {
        public InvalidCardDataException(string message) : base(message)
        {
        }
    }
}
